using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawn : MonoBehaviour
{
    [SerializeField] CustomerData[] customerData;
    [SerializeField] Transform poolPosition;

    StageData stageData;

    List<CustomerType> spawnList;
    int index = 0;
    bool isSpawning = false;

    //GameObject currentCustomer;
    NormalCustomer normalCustomerSpawn;

    public void Init(StageData data)
    {
        stageData = data;

        Debug.Log($"Stage : {stageData.stageName}");
        Debug.Log($"Normal : {stageData.normalCustomerCount}");
        Debug.Log($"Thief : {stageData.thiefCustomerCount}");
        Debug.Log($"Trouble : {stageData.troublemakerCustomerCount}");
        Debug.Log($"Special : {stageData.specialCustomerCount}");

        spawnList = BuildCustomerList(stageData);

        foreach (var type in spawnList)
        {
            Debug.Log($"spawnlist type : {type}");
        }

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
        Debug.Log($"호출 : {type}, count = {count}");

        for (int i = 0; i < count; i++)
        {
            list.Add(type);
            Debug.Log($"추가 : {type}");
        }
    }

    // 다음 손님 생성
    public void SpawnNextCustomer()
    {
        if (isSpawning) return;
        
        if (spawnList == null || index >= spawnList.Count)
        {
            // 여기에서 다음 스테이지로 넘어가는 코드 작성
            Debug.Log("Stage Clear");
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

        GameManager.Instance.currentCustomer =
            Instantiate(data.ageSprite, poolPosition.position, Quaternion.identity);

        StartCoroutine(GameManager.Instance.NormalCustomer.ItemCreate());
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
}