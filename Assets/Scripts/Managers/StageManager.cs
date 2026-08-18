using JetBrains.Annotations;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : ManagerBase
{
    
    [SerializeField] CustomerSpawn customerSpawn;
    [SerializeField] ChangeStageLevel changeStageLevel;
    GameObject stageClearPanel;
    GameObject stageClearPanelFail;
    StageContainer container;
    int currentIndex;
    StageData currentStage;
    CustomerData customerData;
    NormalCustomer normalCustomer;
    TMP_InputField inputField;
    int clearedCustomerCount; // 스테이지 내 처리한 손님 수
    public int CurrentIndex => currentIndex;
    public StageData CurrentStage => currentStage;

    public event Action OnStageChanged;

    public void StartStage(int index)
    {
        currentIndex = index;
        currentStage = container.stageDatas[index];

        clearedCustomerCount = 0;

        customerSpawn.Init(currentStage);
        normalCustomer.Init(container);
    }


    protected override IEnumerator OnConnected(GameManager newManager)
    {
        container = GameManager.Instance.StageContainer;
        customerData = GameManager.Instance.CustomerData;
        normalCustomer = GameManager.Instance.NormalCustomer;
        customerSpawn = FindFirstObjectByType<CustomerSpawn>();
        changeStageLevel = FindFirstObjectByType<ChangeStageLevel>();
        stageClearPanel = GameManager.Instance.StageClearResultPanel;
        stageClearPanelFail = GameManager.Instance.StageClearResultPanelFail;
        yield break;
    }

    protected override void OnDisconnected()
    {
       
    }

    public static StageData GetcurrentStage()
    {
        return GameManager.Instance?.Stage.currentStage;
    }

    
    public void StageRewardCorrect()
    {
        Debug.Log($"requiredcoin : {currentStage.requiredCoin}");
        Debug.Log($"successreward.coin : {GameManager.Instance.CustomerData.successReward.coin}");

        if (GameManager.Instance.RewardModule.Coin >= currentStage.requiredCoin
            && GameManager.Instance.RewardModule.Fame >= currentStage.requiredFame)
        {
            Debug.Log($"stageclearpanel : {stageClearPanel == null}");
            stageClearPanel.SetActive(true);
            
        }
        else
        {
            stageClearPanelFail.SetActive(true);
        }
    }

    public void NextStage()
    {
        currentIndex++;
        StartStage(currentIndex);
        OnStageChanged?.Invoke();
    }

    public void CustomerCleared()
    {
        clearedCustomerCount++;

        CheckStageClear();
    }
    public void CheckStageClear()
    {
        int totalCustomerCount =
            currentStage.normalCustomerCount
            + currentStage.troublemakerCustomerCount
            + currentStage.thiefCustomerCount
            + currentStage.specialCustomerCount;

        bool allCustomerCleared = clearedCustomerCount >= totalCustomerCount;

        bool coinEnough =
            GameManager.Instance.RewardModule.Coin >= currentStage.requiredCoin;

        bool expEnough =
            GameManager.Instance.RewardModule.Fame >= currentStage.requiredFame;

        // 모든 손님 처리 + 코인 + 경험치 모두 만족
        if (allCustomerCleared && coinEnough && expEnough)
        {
            stageClearPanel.SetActive(true);
        }
        // 모든 손님을 처리했는데 조건을 못 채움
        else if (allCustomerCleared)
        {
            stageClearPanelFail.SetActive(true);
        }
    }
}
