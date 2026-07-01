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
    public void StartStage(int index)
    {
        currentIndex = index;
        currentStage = container.stageDatas[index];
        customerSpawn.Init(currentStage);
        normalCustomer.Init(container);
        Debug.Log($"일반 손님 한 명당 아이템 개수 : {currentStage.normalCustomerItemCount}");
        Debug.Log($"현재 스테이지 : {currentStage.stageName}");
        Debug.Log($"손님 수 : {currentStage.normalCustomerCount}");
    }


    protected override IEnumerator OnConnected(GameManager newManager)
    {
        container = GameManager.Instance.StageContainer;
        customerData = GameManager.Instance.CustomerData;
        normalCustomer = GameManager.Instance.NormalCustomer;
        customerSpawn = FindFirstObjectByType<CustomerSpawn>();
        
        /*inputField = FindFirstObjectByType<TMP_InputField>();
        Debug.Log(inputField == null);*/
        


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
    

    
}
