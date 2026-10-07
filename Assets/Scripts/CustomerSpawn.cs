using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CustomerSpawn : MonoBehaviour
{
    [SerializeField] CustomerData[] customerData;
    [SerializeField] Transform poolPosition;
    [SerializeField] GameObject processObj;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject writeName;
    [SerializeField] private TextMeshProUGUI writeText;
    [SerializeField] private Button writeButton;

    [SerializeField] private int thiefSpawnMinCustomer = 2;
    [SerializeField] private int thiefSpawnMaxCustomer = 4;

    private bool thiefSpawned = false;
    GameObject lastTroubleCustomer;

    StageData stageData;

    List<CustomerType> spawnList;
    int index = 0;
    bool isSpawning = false;

    public void Init(StageData data)
    {
        stageData = data;

        processObj.SetActive(false);

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
            CustomerType.TroubleMakerCustomer,
            stageData.troublemakerCustomerCount
        );

        /*AddCustomers(
            list,
            CustomerType.SpecialCustomer,
            stageData.specialCustomerCount
        );*/

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

        if (data == null)
        {
            Debug.LogError($"CustomerData를 찾을 수 없습니다. Type : {type}");
            return;
        }

        if (data.ageSprite == null)
        {
            Debug.LogError($"CustomerData의 ageSprite가 없습니다. Type : {type}");
            return;
        }

        // 손님 생성
        GameObject customer = Instantiate(
            data.ageSprite,
            poolPosition.position,
            Quaternion.identity
        );

        GameManager.Instance.currentCustomer = customer;

        
        // 기존 손님 처리

        switch (type)
        {
            case CustomerType.NormalCustomer:

                GameManager.Instance.NormalCustomer.ResetItemProgress();
                GameManager.Instance.NormalCustomer.SetDialogue(data);

                StartCoroutine(
                    GameManager.Instance.NormalCustomer.ItemCreate()
                );

                break;

            case CustomerType.TroubleMakerCustomer:

                GameObject[] objects = Resources.FindObjectsOfTypeAll<GameObject>();

                foreach (GameObject obj in objects)
                {
                    if (obj.name == "WriteTroubleCustomerName")
                    {
                        writeName = obj;
                        break;
                    }
                }

                foreach (GameObject obj in objects)
                {
                    if (obj.name == "WriteCustomerText")
                    {
                        writeText = obj.GetComponent<TextMeshProUGUI>();
                        break;
                    }
                }

                foreach (GameObject obj in objects)
                {
                    if (obj.name == "WriteTroubleCustomerButton")
                    {
                        writeButton = obj.GetComponent<Button>();
                        break;
                    }
                }

                
                TroubleCustomerAction troubleAction =
                    customer.GetComponent<TroubleCustomerAction>();

                if (troubleAction != null)
                {
                    troubleAction.StartActions(data);
                }
                StartCoroutine(WriteInputField());
                break;

            case CustomerType.SpecialCustomer:

                Debug.Log("특수 손님 등장!");

                break;
        }

        // 도둑 등장 체크
        TrySpawnThief();
    }
    public void SetActiveWriteButton()
    {
        writeName.SetActive(false);
        writeText.gameObject.SetActive(false);
        writeButton.gameObject.SetActive(false);
    }
    public IEnumerator WriteInputField()
    {
        yield return new WaitForSeconds(5f);

        if (GameManager.Instance.Stage.CurrentStage.stageName >= StageType.stage2)
        {
            writeName.SetActive(true);
            writeText.gameObject.SetActive(true);
            writeButton.gameObject.SetActive(true);
            

        }

        /*if (string.IsNullOrWhiteSpace(writeText.text))
            yield break ;

        writeName.SetActive(false);
        writeText.gameObject.SetActive(false);
        writeButton.gameObject.SetActive(false);*/
    }
    CustomerData GetCustomerData(CustomerType type)
    {
        List<CustomerData> matchingData = new List<CustomerData>();

        foreach (CustomerData data in customerData)
        {
            if (data.customerType == type)
            {
                matchingData.Add(data);
            }
        }

        if (matchingData.Count == 0)
            return null;

        return matchingData[Random.Range(0, matchingData.Count)];
    }

    public void StartNextCustomer()
    {
        StartCoroutine(NextCustomerRoutine());
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
            GameManager.Instance.Stage.StageRewardCorrect();

            return;
        }

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

    public void SetCustomerVisible(bool visible)
    {
        if (GameManager.Instance.currentCustomer != null)
        {
            GameManager.Instance.currentCustomer.SetActive(visible);
        }
    }
    void TrySpawnThief()
    {
        if (thiefSpawned)
            return;

        if (stageData.thiefCustomerCount <= 0)
            return;

        // 지금까지 생성된 일반 손님 계열 수
        int spawnedCustomerCount = index;

        if (spawnedCustomerCount < thiefSpawnMinCustomer)
            return;

        if (spawnedCustomerCount > thiefSpawnMaxCustomer)
            return;

        thiefSpawned = true;

        StartCoroutine(ThiefSpawnRoutine());
    }
    IEnumerator ThiefSpawnRoutine()
    {
        float delay = Random.Range(3f, 8f);

        yield return new WaitForSeconds(delay);

        GameManager.Instance.thiefManager.StartThief(stageData);
    }
}