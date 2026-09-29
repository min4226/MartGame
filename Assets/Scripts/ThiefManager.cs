using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class ThiefManager : MonoBehaviour
{
    [SerializeField] private GameObject thiefPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform[] targetPoints;
    [SerializeField] private Transform exitPoint;

    private float thiefTimer;
    private bool isThiefActive;
    private GameObject currentThief;
    GameObject catchText;
    
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

        Collider2D hit = Physics2D.OverlapPoint(worldPosition);

        if (hit == null)
            return;

        if (hit.gameObject != currentThief)
            return;

        Debug.Log("[ThiefManager] 도둑 클릭 성공!");

        CatchThief();

        catchText = null;

        GameObject[] objects = FindObjectsByType<GameObject>(
            FindObjectsInactive.Include,
            FindObjectsSortMode.None
        );

        foreach (GameObject obj in objects)
        {
            if (obj.CompareTag("CatchText"))
            {
                catchText = obj;
                break;
            }
        }

        if (catchText != null)
        {
            StartCoroutine(ShowCatchText()); 
        }
    }
    public void StartThief(StageData stageData)
    {
        Debug.Log("StartThief 실행");

        if (currentThief != null)
            return;

        GameObject background = GameObject.Find("BackGround");

        
        spawnPoint = background.transform.Find("ThiefSpawn");

        Transform target1 = background.transform.Find("ThiefTarget");
        Transform target2 = background.transform.Find("ThiefTarget (1)");
        Transform target3 = background.transform.Find("ThiefTarget (2)");

        exitPoint = background.transform.Find("ThiefExit");

        
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
            Destroy(currentThief);
            currentThief = null;
            return;
        }

        thief.Init(targetPoints, exitPoint);

        // 현재 스테이지의 도둑 제한시간
        thiefTimer = stageData.thiefTimeLimit;
        isThiefActive = true;

    }
    public void ThiefFinished()
    {
        currentThief = null;
    }
    public void CatchThief()
    {
        RewardUI rewardUI = GameObject.Find("GameObject").GetComponent<RewardUI>();
        Debug.Log($"rewardui : {rewardUI}");
        isThiefActive = false;
        thiefTimer = 0f;

        Destroy(currentThief);
        currentThief = null;

        Debug.Log("도둑을 잡았습니다!");
        // 리워드 넣을 공간
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
        // 리워드 넣을 공간
    }
    private IEnumerator ShowCatchText()
    {
        catchText.SetActive(true);

        yield return new WaitForSeconds(2f);

        catchText.SetActive(false);
    }
}