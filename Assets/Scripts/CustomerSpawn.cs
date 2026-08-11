using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawn : MonoBehaviour
{
    [SerializeField] CustomerData[] customerData;
    [SerializeField] Transform poolPosition;
    [SerializeField] GameObject processObj;
    StageData stageData;

    List<CustomerType> spawnList;
    int index = 0;
    bool isSpawning = false;

    //GameObject currentCustomer;
    NormalCustomer normalCustomerSpawn;

    public void Init(StageData data)
    {
        stageData = data;
        processObj.SetActive(false);
        Debug.Log($"Stage : {stageData.stageName}");
        Debug.Log($"Normal : {stageData.normalCustomerCount}");
        Debug.Log($"Thief : {stageData.thiefCustomerCount}");
        Debug.Log($"Trouble : {stageData.troublemakerCustomerCount}");
        Debug.Log($"Special : {stageData.specialCustomerCount}");

        spawnList = BuildCustomerList(stageData);

        

        index = 0;

        if (GameManager.Instance.CurrentState != GameState.PlayScene)
            return;

        SpawnNextCustomer();
    }

    // 손님 타입들을 리스트에 저장
    List<CustomerType> BuildCustomerList(StageData stageData)
    {
       
        List<CustomerType> list = new List<CustomerType>();
       
        AddCustomers(list, CustomerType.NormalCustomer, stageData.normalCustomerCount);
        AddCustomers(list, CustomerType.ThiefCustomer, stageData.thiefCustomerCount);
        AddCustomers(list, CustomerType.TroubleMakerCustomer, stageData.troublemakerCustomerCount);
        AddCustomers(list, CustomerType.SpecialCustomer, stageData.specialCustomerCount);

        return list;
    }

    void AddCustomers(List<CustomerType> list, CustomerType type, int count)
    {

        for (int i = 0; i < count; i++)
        {
            list.Add(type);
        }
    }

    // 다음 손님 생성
    public void SpawnNextCustomer()
    {
        if (isSpawning) return;
        
        if (spawnList == null || index >= spawnList.Count)
        {
            
            GameManager.Instance.Stage.StageRewardCorrect();


            return;
        }

        StartCoroutine(SpawnRoutine(spawnList[index]));
    }

    
    
    IEnumerator SpawnRoutine(CustomerType type)
    {

        isSpawning = true;

        yield return new WaitForSeconds(1f); // 손님 등장 시간

        Spawn(type);
        
        index++;

        isSpawning = false;
    }


    void Spawn(CustomerType type)
    {
        CustomerData data = GetCustomerData(type);
        GameObject customer = Instantiate(data.ageSprite, poolPosition.position, Quaternion.identity);
        GameManager.Instance.currentCustomer = customer;
        switch (type)
        {
            case CustomerType.NormalCustomer: // 일반 손님일 경우
                
                StartCoroutine(GameManager.Instance.NormalCustomer.ItemCreate());
                return;
            case CustomerType.TroubleMakerCustomer: // 진상 손님일 경우
                
                StartCoroutine(ProcessObjCreate(customer));
                return;
        }
         
        
    }

    CustomerData GetCustomerData(CustomerType type)
    {
        return System.Array.Find(customerData, x => x.customerType == type);
    }


    public void OnCustomerEnd()
    {
        if (GameManager.Instance.currentCustomer != null)
            Destroy(GameManager.Instance.currentCustomer);

        SpawnNextCustomer();
    }

    public IEnumerator NextCustomerRoutine()
    { 
        yield return new WaitForSeconds(1f);
        GameManager.Instance.CorrectAnswer.SetActive(false);
        GameManager.Instance.FailAnswer.SetActive(false);
        GameManager.Instance.InputField.text = "";
        GameManager.Instance.InputField.gameObject.SetActive(false);
        GameManager.Instance.EnterButton.gameObject.SetActive(false);
        OnCustomerEnd();
    }

    public IEnumerator ProcessObjCreate(GameObject customer)
    {
        
        yield return new WaitForSeconds(1f);
        GameObject troubleCustomerClone = GameObject.Find("Person 3(Clone)");
        Transform troubleCustomerCanvas = troubleCustomerClone.transform.Find("Canvas");
        Transform processObj = troubleCustomerCanvas.transform.Find("ProcessObj");
        

        if (processObj != null)
        {
 
            processObj.gameObject.SetActive(true);
            /*Vector3 screenPos = Camera.main.WorldToScreenPoint(customer.transform.position);
            processObj.position = screenPos;*/
            
        }
        
    }
}