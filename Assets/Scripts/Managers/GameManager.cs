using System.Collections;
using TMPro.EditorUtilities;
using UnityEngine;

public delegate void InitializeEvent();
public delegate void UpdateEvent(float deltaTime);
public delegate void DestroyEvent();

public class GameManager : MonoBehaviour
{
    static GameManager _instance;
    public static GameManager Instance => _instance;

    // 각 프로퍼티를 만듦
    UIManager _ui;
    public UIManager UI => _ui;
    DataManager _data;
    public DataManager Data => _data;
    

    public ObjectManager ObjectM => _objectM;
    ObjectManager _objectM;

    SaveManager _save;
    public SaveManager Save => _save;
    SettingManager _setting;
    public SettingManager Setting => _setting;
    LanguageManager _language;
    public LanguageManager Language => _language;
    AudioManager _audio;
    public AudioManager Audio => _audio;
    CameraManager _camera;
    public CameraManager Camera => _camera;
    InputManager _input;
    public InputManager Input => _input;

    
    IEnumerator initializing;

    public static event InitializeEvent OnInitializeManager;
    public static event InitializeEvent OnInitializeController;
    public static event InitializeEvent OnInitializeCharacter;
    public static event InitializeEvent OnInitializeObject;
    public static event UpdateEvent OnUpdateManager;
    public static event UpdateEvent OnUpdateController;
    public static event UpdateEvent OnUpdateCharacter;
    public static event UpdateEvent OnUpdateObject;

    public static DestroyEvent OnDestroyManager;
    public static DestroyEvent OnDestroyController;
    public static DestroyEvent OnDestroyCharacter;
    public static DestroyEvent OnDestroyObject;

    bool isLoading = true;
    bool isPlaying = true;
    void Awake()
    {
        if (Instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }

        // 변수에 저장을 해서 사용하는 것이 더 좋다. 이름으로 넣는 게 아니라
        initializing = InitializeManagers();

        StartCoroutine(initializing);
        
    }
    void OnDestroy()
    {
        if(initializing != null) StopCoroutine(initializing);
        DeleteManagers();
    }

    IEnumerator InitializeManagers()
    {
        int totalLoad = 0;
        totalLoad += CreateManager(ref _ui).LoadingCount;
        totalLoad += CreateManager(ref _data).LoadingCount;
        totalLoad += CreateManager(ref _objectM).LoadingCount;
        totalLoad += CreateManager(ref _save).LoadingCount;
        totalLoad += CreateManager(ref _language).LoadingCount;
        totalLoad += CreateManager(ref _setting).LoadingCount;
        totalLoad += CreateManager(ref _audio).LoadingCount;
        totalLoad += CreateManager(ref _camera).LoadingCount;
        totalLoad += CreateManager(ref _input).LoadingCount;

        yield return  UI.Initialize(this);
        UIBase loadingUI = UIManager.ClaimOpenScreen(UIType.Loading);
        IProgress<int> loadingProgress = loadingUI as IProgress<int>;

        loadingProgress?.Set(0, totalLoad);
        yield return _data.Connect(this);
        yield return _objectM.Connect(this);
        loadingProgress?.AddCurrent(1);
        yield return UI.Connect(this);
        loadingProgress?.AddCurrent(1);
        yield return _save.Connect(this);
        loadingProgress?.AddCurrent(1);
        yield return _language.Connect(this);
        loadingProgress?.AddCurrent(1);
        yield return _setting.Connect(this);
        loadingProgress?.AddCurrent(1);
        yield return _audio.Connect(this);
        loadingProgress?.AddCurrent(1);
        yield return _camera.Connect(this);
        loadingProgress?.AddCurrent(1);
        yield return _input.Connect(this);
        loadingProgress?.AddCurrent(1);
        yield return new WaitForSeconds(1.0f);
        UIManager.ClaimOpenScreen(UIType.Title);
        isLoading = false;

    }

    void DeleteManagers()
    {
        Input?.Disconnect();
        ObjectM?.Disconnect();
        Audio?.Disconnect();
        Language?.Disconnect();
        Setting?.Disconnect();
        Save?.Disconnect();
        Camera?.Disconnect();
        UI?.Disconnect();
        Data?.Disconnect();
    }

    ManagerType CreateManager<ManagerType>(ref ManagerType targetVariable)  where ManagerType :  ManagerBase
    {
        if (targetVariable == null)
        {
            targetVariable = this.TryAddComponent<ManagerType>();
            
        }
        return targetVariable;
    }
    public static void Pause()
    {
        Instance.isPlaying = false;
    }

    public static void Unpause()
    {
        Instance.isPlaying = true;
    }

    
    public void currentInitialize(ref InitializeEvent initializeEvent)
    {
        if (initializeEvent != null)
        {
            InitializeEvent CurrentName = initializeEvent;
            initializeEvent = null;
            CurrentName?.Invoke();

        }
    }

    public void currentDestroy(ref DestroyEvent destroyEvent)
    {
        if (destroyEvent != null)
        {
            DestroyEvent CurrentName = destroyEvent;
            destroyEvent = null;
            CurrentName?.Invoke();

        }
    }
    void Update()
    {
        if (isLoading) return;

        currentInitialize(ref OnInitializeManager);
        currentInitialize(ref OnInitializeCharacter);
        currentInitialize(ref OnInitializeController);
        currentInitialize(ref OnInitializeObject);
        
        if (isPlaying)
        { 
            float deltaTime = Time.deltaTime;
            OnUpdateManager?.Invoke(deltaTime);
            OnUpdateController?.Invoke(deltaTime);
            OnUpdateCharacter?.Invoke(deltaTime);
            OnUpdateObject?.Invoke(deltaTime);
            
        }

        currentDestroy(ref OnDestroyObject);
        currentDestroy(ref OnDestroyController);
        currentDestroy(ref OnDestroyCharacter);
        currentDestroy(ref OnDestroyManager);
        OnDestroyObject?.Invoke();
        OnDestroyController?.Invoke();
        OnDestroyCharacter?.Invoke();
        OnDestroyManager?.Invoke();

}
}
