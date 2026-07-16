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
    
    public int CurrentIndex => currentIndex;
    public StageData CurrentStage => currentStage;

    public event Action OnStageChanged;
    
    public void StartStage(int index)
    {
        currentIndex = index;
        currentStage = container.stageDatas[index];

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

    // 스테이지 클리어에 필요한 리워드와 유저가 받은 리워드가 같다면 실행하는 함수
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

    
}
