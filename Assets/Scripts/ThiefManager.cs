using UnityEngine;

public class ThiefManager : MonoBehaviour
{
    [SerializeField] private GameObject thiefPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform[] targetPoints;
    [SerializeField] private Transform exitPoint;

    private float thiefTimer;
    private bool isThiefActive;
    private GameObject currentThief;

    private void Update()
    {
        if (!isThiefActive)
            return;

        thiefTimer -= Time.deltaTime;

        if (thiefTimer <= 0f)
        {
            thiefTimer = 0f;
            ThiefFailed();
        }
    }
    private void OnEnable()
    {
        InputManager.OnMouseLeftButton += OnMouseLeftButton;
    }

    private void OnDisable()
    {
        InputManager.OnMouseLeftButton -= OnMouseLeftButton;
    }

    private void OnMouseLeftButton(
        bool value,
        Vector2 screenPosition,
        Vector3 worldPosition)
    {
        if (!value)
            return;

        if (currentThief == null)
            return;

        if (InputManager.CursorSelectObject == currentThief)
        {
            CatchThief();
        }
    }
    public void StartThief(StageData stageData)
    {
        Debug.Log("StartThief 실행");

        if (currentThief != null)
            return;

        GameObject background = GameObject.Find("BackGround");

        if (background == null)
        {
            Debug.LogError("BackGround를 찾을 수 없습니다.");
            return;
        }

        spawnPoint = background.transform.Find("ThiefSpawn");

        Transform target1 = background.transform.Find("ThiefTarget");
        Transform target2 = background.transform.Find("ThiefTarget (1)");
        Transform target3 = background.transform.Find("ThiefTarget (2)");

        exitPoint = background.transform.Find("ThiefExit");

        if (spawnPoint == null)
        {
            Debug.LogError("ThiefSpawn을 찾을 수 없습니다.");
            return;
        }

        if (target1 == null || target2 == null || target3 == null)
        {
            Debug.LogError("ThiefTarget를 찾을 수 없습니다.");
            return;
        }

        if (exitPoint == null)
        {
            Debug.LogError("ThiefExit을 찾을 수 없습니다.");
            return;
        }

        if (thiefPrefab == null)
        {
            Debug.LogError("ThiefPrefab이 ThiefManager에 들어있지 않습니다.");
            return;
        }

        if (stageData == null)
        {
            Debug.LogError("StageData가 null입니다.");
            return;
        }

        targetPoints = new Transform[]
        {
        target1,
        target2,
        target3
        };

        currentThief = Instantiate(
            thiefPrefab,
            spawnPoint.position,
            Quaternion.identity
        );

        Thief thief = currentThief.GetComponent<Thief>();

        if (thief == null)
        {
            Debug.LogError("ThiefPrefab에 Thief 컴포넌트가 없습니다.");
            Destroy(currentThief);
            currentThief = null;
            return;
        }

        thief.Init(targetPoints, exitPoint);

        // 현재 스테이지의 도둑 제한시간
        thiefTimer = stageData.thiefTimeLimit;
        isThiefActive = true;

        Debug.Log($"도둑 등장! 제한시간 : {thiefTimer}");
    }
    public void ThiefFinished()
    {
        currentThief = null;
    }
    public void CatchThief()
    {
        if (currentThief == null)
            return;

        isThiefActive = false;
        thiefTimer = 0f;

        Destroy(currentThief);
        currentThief = null;

        Debug.Log("도둑을 잡았습니다!");
    }
    private void ThiefFailed()
    {
        isThiefActive = false;

        if (currentThief != null)
        {
            Destroy(currentThief);
            currentThief = null;
        }

        Debug.Log("도둑을 놓쳤습니다!");
    }  
}