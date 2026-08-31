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

        // 스테이지에 나올 손님 목록 생성
        spawnList = BuildCustomerList(stageData);

        // 처음부터 다시 시작
        index = 0;

        if (GameManager.Instance.CurrentState != GameState.PlayScene)
            return;

        SpawnNextCustomer();
    }


    // -----------------------------------------
    // 스테이지 손님 목록 만들기
    // -----------------------------------------
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

        return list;
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

        // 더 이상 생성할 손님이 없음
        if (spawnList == null || index >= spawnList.Count)
            return;

        StartCoroutine(SpawnRoutine(spawnList[index]));
    }


    IEnumerator SpawnRoutine(CustomerType type)
    {
        isSpawning = true;

        yield return new WaitForSeconds(1f);

        Spawn(type);

        // 현재 손님을 생성했으므로 다음 번호로 이동
        index++;

        isSpawning = false;
    }



    void Spawn(CustomerType type)
    {
        CustomerData data = GetCustomerData(type);

        // TroubleCustomer인 경우
        if (type == CustomerType.TroubleMakerCustomer)
        {
            CustomerData[] troubleDatas = System.Array.FindAll(
                customerData,
                x => x.customerType == CustomerType.TroubleMakerCustomer
            );

            // TroubleCustomer가 여러 명 등록되어 있는 경우
            if (troubleDatas.Length > 1)
            {
                List<CustomerData> availableDatas = new List<CustomerData>();

                foreach (CustomerData troubleData in troubleDatas)
                {
                    // 바로 직전에 나온 캐릭터 제외
                    if (troubleData.ageSprite != lastTroubleCustomer)
                    {
                        availableDatas.Add(troubleData);
                    }
                }

                // 혹시 전부 제외되어도 안전하게 처리
                if (availableDatas.Count > 0)
                {
                    data = availableDatas[
                        Random.Range(0, availableDatas.Count)
                    ];
                }
                else
                {
                    // 선택 가능한 캐릭터가 없으면 그냥 랜덤
                    data = troubleDatas[
                        Random.Range(0, troubleDatas.Length)
                    ];
                }
            }

            lastTroubleCustomer = data.ageSprite;
        }

        Debug.Log($"🧑 생성할 손님 : {data.ageSprite.name}");

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


    // -----------------------------------------
    // 현재 손님 처리 완료
    // -----------------------------------------
    public void OnCustomerEnd()
    {
        if (GameManager.Instance.currentCustomer != null)
        {
            Destroy(GameManager.Instance.currentCustomer);
            GameManager.Instance.currentCustomer = null;
        }


        // 모든 손님 처리가 끝났는지 확인
        if (index >= spawnList.Count)
        {
            Debug.Log("모든 손님 처리 완료");

            GameManager.Instance.Stage.StageRewardCorrect();

            return;
        }


        // 아직 남은 손님이 있음
        SpawnNextCustomer();
    }


    // -----------------------------------------
    // 다음 손님으로 넘어가는 코루틴
    // -----------------------------------------
    public IEnumerator NextCustomerRoutine()
    {
        Debug.Log("🚨🚨🚨 NextCustomerRoutine 실행됨!");
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
}