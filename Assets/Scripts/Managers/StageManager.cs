using JetBrains.Annotations;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : ManagerBase
{
    
    [SerializeField] CustomerSpawn customerSpawn;
    StageContainer container;
    int currentIndex;
    StageData currentStage;
    CustomerData customerData;
    NormalCustomer normalCustomer;
    TMP_InputField inputField;

    public int CurrentIndex => currentIndex;
    public StageData CurrentStage => currentStage;
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
        
        //StartStage(currentIndex);

        yield break;
    }

    protected override void OnDisconnected()
    {
       
    }

    public static StageData GetcurrentStage()
    {
        return GameManager.Instance?.Stage.currentStage;
    }

    public void NextStage()
    {
        currentIndex++;
        StartStage(currentIndex);
    }

    
}
