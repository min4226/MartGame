using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UIType
{ 
    None, Loading,  Movable, SongPlayList, Title, Example,
        _Length
}

public delegate void PopupEvent(string title, string context, string confirm);
public class UIManager : ManagerBase
{

    public static event PopupEvent OnPopup;


    Canvas _mainCanvas;
    public Canvas MainCanvas => _mainCanvas;

    UIBase _movableScreen;

    GraphicRaycaster _raycaster;
    public GraphicRaycaster Raycaster => _raycaster;

    Dictionary<UIType, UIBase> uiDictionary = new();

    Rect _uiBoundary;
    public static Rect UIBoundary => GameManager.Instance?.UI?._uiBoundary ?? Rect.zero ;

    UIType _currentScreenType = UIType.None;
    public static UIType CurrentScreen => GameManager.Instance?.UI?._currentScreenType ?? UIType.None;

    float _uiScale = 1.0f;
    public static float UIScale => GameManager.Instance?.UI?._uiScale ?? 1.0f;

    
    public IEnumerator Initialize(GameManager newManager)
    {
        SetMainCanvas(GetComponentInChildren<Canvas>());
        SetUI(UIType.Loading, GetComponentInChildren<UI_LoadingScreen>());
        //ObjectManager.CreateObject("Popup",_mainCanvas.transform);
        yield return null;
    }
    protected override IEnumerator OnConnected(GameManager newManager)
    {
        _movableScreen =  CreateUI(UIType.Movable, "MovableScreen");
        GameObject screenSwitcher = new GameObject("ScreenSwitcher");

        RectTransform switcherTransform =  screenSwitcher.AddComponent<RectTransform>();

        switcherTransform.SetParent(MainCanvas.transform);
        switcherTransform.SetAsFirstSibling();

        switcherTransform.anchorMin = Vector3.zero;
        switcherTransform.anchorMax = Vector3.one;

        switcherTransform.offsetMin = Vector3.zero;
        switcherTransform.offsetMax = Vector3.zero;

        switcherTransform.localScale = Vector3.one;

        CreateUI(UIType.Title, "TitleScreen", switcherTransform);
        CreateUI(UIType.Example, "ExScreen", switcherTransform);

        foreach (Transform currentTransform in switcherTransform)
        { 
            currentTransform.gameObject.SetActive(false);
        }

        yield return null;
       
       
    }

    protected override void OnDisconnected()
    {
        UnSetAllUI();
    }

    protected void SetMainCanvas(Canvas newCanvas)
    {
        _mainCanvas = newCanvas;
        if (MainCanvas)
        {
            _raycaster = MainCanvas.GetComponent<GraphicRaycaster>();
            if (MainCanvas.transform is RectTransform mainRectTransform)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(mainRectTransform);
                _uiScale =mainRectTransform.lossyScale.x;
                _uiBoundary = mainRectTransform.rect;
                //_uiBoundary.size *= _uiScale;
                
               //_uiBoundary.position *= UIScale /1.0f;
            }
        }
        else
        { 
            _raycaster = null;
        }
    }
    protected UIBase CreateUI(UIType wantType, string wantName, Transform parent)
    {
        GameObject instance = ObjectManager.CreateObject(wantName, parent);
        UIBase result = instance?.GetComponent<UIBase>();
        return SetUI(wantType, result);
    }
    protected UIBase CreateUI(UIType wantType, string wantName)
    { 
        UIBase result = CreateUI(wantType, wantName, _mainCanvas?.transform);

        if (result?.GetComponentInChildren<UI_DraggableWindow>())
        {
            _movableScreen?.SetChild(result.gameObject);
        }
        return result;
    }

    public static UIBase ClaimCreateUI(UIType wantType, string wantName) => GameManager.Instance?.UI?.CreateUI(wantType, wantName);
    
    protected UIBase SetUI(UIType wantType, UIBase wantUI)
    {
        if (wantUI == null) return null;

        if (uiDictionary.TryGetValue(wantType, out UIBase origin)) return origin;
        uiDictionary.Add(wantType,wantUI);
        
        return SetUI(wantUI);
    }
    
    protected void UnsetUI(UIType wantType)
    {
        if (uiDictionary.TryGetValue(wantType, out UIBase found))
        {
            UnsetUI(found);
            uiDictionary.Remove(wantType);
        }
    }
    protected void UnsetUI(UIBase wantUI)
    {
        if (!wantUI) return;

        wantUI.Unregistration(this);
    }
    protected void UnSetAllUI()
    {
        foreach (UIBase ui in uiDictionary.Values)
        {
            UnsetUI(ui);
        }
        uiDictionary.Clear();
    }
    protected UIBase SetUI(UIBase wantUI)
    { 
        wantUI?.Registration(this);
        return wantUI;
        
    }

    public static void ClaimSetUI(UIBase wantUI) => GameManager.Instance?.UI?.UnsetUI(wantUI);
    public static void ClaimSetUI(GameObject wantObject) => ClaimUnsetUI(wantObject?.GetComponent<UIBase>());

    public static void ClaimUnsetUI(UIBase wantUI) => GameManager.Instance?.UI?.UnsetUI(wantUI);
    public static void ClaimUnsetUI(GameObject wantObject) => ClaimUnsetUI(wantObject?.GetComponent<UIBase>());
    public static UIBase ClaimSetUI(UIType wantType, UIBase wantUI) => GameManager.Instance?.UI?.SetUI(wantType,wantUI);


    protected  UIBase GetUI(UIType wantType)
    {

        if (uiDictionary.TryGetValue(wantType, out UIBase result)) return result;
        else return null;
        
    }
    public static UIBase ClaimGetUI(UIType wantType) => GameManager.Instance?.UI?.GetUI(wantType);
    protected  UIBase  OpenUI(UIType wantType)
    {
        UIBase result = GetUI(wantType);
        if(result is IOpenable opener) opener.Open();
        return result;
    }
    public static UIBase ClaimOpenUI(UIType wantType) => GameManager.Instance?.UI?.OpenUI(wantType);
    protected  UIBase CloseUI(UIType wantType)
    {
        UIBase result = GetUI(wantType);
        if (result is IOpenable closer) closer.Close();
        return result;
    }
    public static UIBase ClaimCloseUI(UIType wantType) => GameManager.Instance?.UI?.CloseUI(wantType);
    protected  UIBase ToggleUI(UIType wantType)
    {
        UIBase result = GetUI(wantType);
        if (result is IOpenable toggle) toggle.Toggle();
        return result;
    }
    public static UIBase ClaimToggleUI(UIType wantType) => GameManager.Instance?.UI?.ToggleUI(wantType);

    public static void ClaimPopup(string title, string context, string confirm)
    {
        OnPopup?.Invoke(title, context, confirm);
    }

    protected UIBase OpenScreen(UIType wantType)
    {
        CloseUI(CurrentScreen);
        _currentScreenType = wantType;
        return OpenUI(wantType);
    }

    public static UIBase ClaimOpenScreen(UIType wantType) => GameManager.Instance?.UI?.OpenScreen(wantType);
    public static void ClaimErrorPopup(string context)
    {
        OnPopup?.Invoke("¿À·ù", context, "");
    }
}
