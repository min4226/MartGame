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

    GameObject currentCustomer;

    
    public void Init(StageData data)
    {
        stageData = data;

        spawnList = BuildCustomerList(stageData);
        index = 0;

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
            list.Add(type);
    }

    // 다음 손님 생성
    void SpawnNextCustomer()
    {
        if (isSpawning) return;

        if (spawnList == null || index >= spawnList.Count)
        {
            Debug.Log("Stage Clear");
            return;
        }

        StartCoroutine(SpawnRoutine(spawnList[index]));
    }

    
    IEnumerator SpawnRoutine(CustomerType type)
    {
        isSpawning = true;

        yield return new WaitForSeconds(1f); // 등장 연출 시간

        Spawn(type);

        index++;

        isSpawning = false;
    }

    
    void Spawn(CustomerType type)
    {
        CustomerData data = GetCustomerData(type);
        currentCustomer = Instantiate(data.ageSprite, poolPosition.position, Quaternion.identity);
    }

    CustomerData GetCustomerData(CustomerType type)
    {
        return System.Array.Find(customerData, x => x.customerType == type);
    }

    
    public void OnCustomerEnd()
    {
        if (currentCustomer != null)
            Destroy(currentCustomer);

        SpawnNextCustomer();
    }
}