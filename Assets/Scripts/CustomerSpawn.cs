using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class CustomerSpawn : MonoBehaviour ,IFunctionable
{
    [SerializeField] CustomerData[] customerData; 
    [SerializeField] Transform poolPosition;
    [SerializeField] NormalCustomer normalCustomer;

    // 스테이지마다 생성될 손님들 종류와 개수를 리스트에 넣음

    List<CustomerType> BuildCustomerList(StageData stageData)
    {
        /*if (stageData == null)
        {
            Debug.LogError("stageData null");
            return new List<CustomerType>();
        }*/
        
        List<CustomerType> spawnList = new List<CustomerType>();

        AddCustomers(spawnList, CustomerType.NormalCustomer, stageData.normalCustomerCount);
        AddCustomers(spawnList, CustomerType.ThiefCustomer, stageData.thiefCustomerCount);
        AddCustomers(spawnList, CustomerType.TroubleMakerCustomer, stageData.troublemakerCustomerCount);
        AddCustomers(spawnList, CustomerType.SpecialCustomer, stageData.specialCustomerCount);

        return spawnList;
    }
    void AddCustomers(List<CustomerType> list, CustomerType type, int count)
    {
        for (int i = 0; i < count; i++)
            list.Add(type);
    }

    void Initialize() 
    {
        var list = BuildCustomerList(StageManager.GetcurrentStage());
        StartCoroutine(SpawnCustomer(list));
    } 
    IEnumerator SpawnCustomer(List<CustomerType> list)
    {
        foreach (var customerType in list)
        {
            Spawn(customerType);
            yield return new WaitForSeconds(1);
        }
    }

    void Spawn(CustomerType type)
    {
        CustomerData data = GetCustomerData(type);
        
        Instantiate(data.ageSprite, poolPosition.position, Quaternion.identity);

    }

    CustomerData GetCustomerData(CustomerType type)
    {
        // customerdata 안의 배열에서 customertype이 같은 경우 리턴
        return System.Array.Find(customerData, x => x.customerType == type);
    }

    public void RegistrationFunctions()
    {
        GameManager.OnInitializeObject += Initialize;
    }

    public void UnregistrationFunctions()
    {

    }
}