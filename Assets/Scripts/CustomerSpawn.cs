using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawn : MonoBehaviour
{
    [SerializeField] CustomerData[] customerData;
    [SerializeField] Transform poolPosition;
    [SerializeField] GameObject processObj;
    GameObject lastTroubleCustomer;

    StageData stageData;

    List<CustomerType> spawnList;
    int index = 0;
    bool isSpawning = false;

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


    
    List<CustomerType> BuildCustomerList(StageData stageData)
    {
        List<CustomerType> list = new List<CustomerType>();

        AddCustomers(
            list,
            CustomerType.NormalCustomer,
            stageData.normalCustomerCount
        );

        AddCustomers(
            list,
            CustomerType.ThiefCustomer,
            stageData.thiefCustomerCount
        );

        AddCustomers(
            list,
            CustomerType.TroubleMakerCustomer,
            stageData.troublemakerCustomerCount
        );

        AddCustomers(
            list,
            CustomerType.SpecialCustomer,
            stageData.specialCustomerCount
        );
        Shuffle(list);
        return list;
    }
    void Shuffle(List<CustomerType> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);

            CustomerType temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    void AddCustomers(
        List<CustomerType> list,
        CustomerType type,
        int count)
    {
        for (int i = 0; i < count; i++)
        {
            list.Add(type);
        }
    }



    public void SpawnNextCustomer()
    {
        if (isSpawning)
            return;

        
        if (spawnList == null || index >= spawnList.Count)
            return;

        StartCoroutine(SpawnRoutine(spawnList[index]));
    }


    IEnumerator SpawnRoutine(CustomerType type)
    {
        isSpawning = true;

        yield return new WaitForSeconds(1f);

        Spawn(type);

        
        index++;

        isSpawning = false;
    }



    void Spawn(CustomerType type)
    {
        CustomerData data = GetCustomerData(type);

        
        if (type == CustomerType.TroubleMakerCustomer)
        {
            CustomerData[] troubleDatas = System.Array.FindAll(
                customerData,
                x => x.customerType == CustomerType.TroubleMakerCustomer
            );

            
            if (troubleDatas.Length > 1)
            {
                List<CustomerData> availableDatas = new List<CustomerData>();

                foreach (CustomerData troubleData in troubleDatas)
                {
                    
                    if (troubleData.ageSprite != lastTroubleCustomer)
                    {
                        availableDatas.Add(troubleData);
                    }
                }

                
                if (availableDatas.Count > 0)
                {
                    data = availableDatas[
                        Random.Range(0, availableDatas.Count)
                    ];
                }
                else
                {
                    
                    data = troubleDatas[
                        Random.Range(0, troubleDatas.Length)
                    ];
                }
            }

            lastTroubleCustomer = data.ageSprite;
        }

        Debug.Log($" 생성할 손님 : {data.ageSprite.name}");

        GameObject customer = Instantiate(
            data.ageSprite,
            poolPosition.position,
            Quaternion.identity
        );

        GameManager.Instance.currentCustomer = customer;

        switch (type)
        {
            case CustomerType.NormalCustomer:

                StartCoroutine(
                    GameManager.Instance.NormalCustomer.ItemCreate()
                );

                return;

            case CustomerType.TroubleMakerCustomer:

                StartCoroutine(
                    ProcessObjCreate(customer)
                );

                return;
        }
    }


    CustomerData GetCustomerData(CustomerType type)
    {
        return System.Array.Find(
            customerData,
            x => x.customerType == type
        );
    }


    
    public void OnCustomerEnd()
    {
        if (GameManager.Instance.currentCustomer != null)
        {
            Destroy(GameManager.Instance.currentCustomer);
            GameManager.Instance.currentCustomer = null;
        }


        
        if (index >= spawnList.Count)
        {
            Debug.Log("모든 손님 처리 완료");

            GameManager.Instance.Stage.StageRewardCorrect();

            return;
        }


        SpawnNextCustomer();
    }


    
    public IEnumerator NextCustomerRoutine()
    {
        Debug.Log("NextCustomerRoutine 실행됨!");
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

        if (customer == null)
        {
            Debug.LogError("TroubleCustomer가 생성되지 않았습니다.");
            yield break;
        }


        Transform troubleCustomerCanvas =
            customer.transform.Find("Canvas");

        Transform processObj =
            troubleCustomerCanvas.transform.Find("ProcessObj");

        if (processObj != null)
        {
            processObj.gameObject.SetActive(true);
        }
    }
    public void SetCustomerVisible(bool visible)
    {
        if (GameManager.Instance.currentCustomer != null)
        {
            GameManager.Instance.currentCustomer.SetActive(visible);
        }
    }
}