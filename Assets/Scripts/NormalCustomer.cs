using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public delegate void changedTodayItems(List<ItemData> todayItems);

public class NormalCustomer : MonoBehaviour
{
    public changedTodayItems OnChangedTodayItems;

    [SerializeField] NormalCustomerItem[] items;
    [SerializeField] Transform itemPool;
    [SerializeField] StageContainer stageContainer;
    [SerializeField] Trigger trigger;
    [SerializeField] NormalCustomerItem todayItem;

    [SerializeField] Image balloonImage;
    [SerializeField] TextMeshProUGUI dialogueText;

    // 가격 패널
    [SerializeField] private Button priceArrowButton;
    [SerializeField] private GameObject pricePanel;

    private bool isPricePanelLocked = false;

    Transform dialogueUI;
    Canvas canvas;

    public List<ItemData> todayItems = new List<ItemData>();

    GameObject normalItem;

    int speed = 3;
    int currentIndex;

    
    private int createdItemCount = 0;
    private int targetItemCount = 0;

    private bool isItemCreating = false;


    
    private int currentDialogueIndex = 0;

    
    private float dialogueTimer = 0f;

    
    private bool isDialogueFinished = false;

    
    private bool isDialoguePlaying = false;

    
    private CustomerData currentDialogueData;


    

    private void Awake()
    {
        canvas = FindFirstObjectByType<Canvas>();
    }


    
    private void OnEnable()
    {
        if (GameManager.Instance == null)
            return;

        if (GameManager.Instance.CurrentState != GameState.PlayScene)
            return;

        if (GameManager.Instance.currentCustomer == null)
            return;



        if (createdItemCount < targetItemCount && !isItemCreating)
        {
            StartCoroutine(ItemCreate());
        }


        
        if (!isDialogueFinished &&
            currentDialogueData != null &&
            !isDialoguePlaying)
        {
            StartCoroutine(ShowDialogues(currentDialogueData));
        }
    }


   
    private void OnDisable()
    {
        // 기존 진행 상황을 그대로 들고오게 하기

        isItemCreating = false;
        isDialoguePlaying = false;
    }


    public void Init(StageContainer data)
    {
        if (GameManager.Instance.CurrentState != GameState.PlayScene)
            return;

        stageContainer = data;
    }


    public IEnumerator ItemCreate()
    {
        if (GameManager.Instance.currentCustomer == null)
            yield break;

        isItemCreating = true;


        int stageIndex = GameManager.Instance.Stage.CurrentIndex;

        StageData stageData =
            stageContainer.stageDatas[stageIndex];


        targetItemCount =
            stageData.normalCustomerItemCount;


        if (createdItemCount == 0)
        {
            todayItems.Clear();

            PricePanelController priceController =
                FindFirstObjectByType<PricePanelController>();

            if (priceController != null)
                priceController.ResetButton();

            trigger.SetItemCount(targetItemCount);
        }


       
        while (createdItemCount < targetItemCount)
        {
            if (items.Length == 0)
            {
                isItemCreating = false;
                yield break;
            }


            NormalCustomerItem customerItem =
                items[Random.Range(0, items.Length)];


            if (customerItem.item.Length == 0)
                continue;


            ItemData itemData =
                customerItem.item[
                    Random.Range(0, customerItem.item.Length)
                ];


            // 상품 데이터 저장
            todayItems.Add(itemData);


            // 실제 상품 생성
            normalItem = Instantiate(
                itemData.itemPrefab,
                itemPool,
                false
            );


            normalItem.transform.localScale = Vector3.one;




            if (!normalItem.TryGetComponent<MoveRight>(out var move))
            {
                move = normalItem.AddComponent<MoveRight>();
            }


            move.speed = speed;


            
            createdItemCount++;

            while (GameManager.Instance.CurrentState
                   != GameState.PlayScene)
            {
                yield return null;
            }


            yield return StartCoroutine(WaitForPlaySeconds(2f));

        }

        
        OnChangedTodayItems?.Invoke(todayItems);

        isItemCreating = false;
    }


    private IEnumerator WaitForPlaySeconds(float seconds)
    {
        float timer = 0f;


        while (timer < seconds)
        {
            if (GameManager.Instance.CurrentState
                == GameState.PlayScene)
            {
                timer += Time.deltaTime;
            }

            yield return null;
        }
    }


    public void OpenPricePanel()
    {
        if (isPricePanelLocked)
        {
            return;
        }


        isPricePanelLocked = true;

        pricePanel.SetActive(true);

        priceArrowButton.interactable = false;
    }


    public int ItemTotalValue(List<ItemData> todayItems)
    {
        int total = 0;


        foreach (ItemData currentItem in todayItems)
        {
            total += currentItem.itemBasePrice;
        }


        return total;
    }


    public void SetDialogue(CustomerData data)
    {
        if (data == null) return;
       
        currentDialogueData = data;

        currentDialogueIndex = 0;
        dialogueTimer = 0f;
        isDialogueFinished = false;

        UI_StageScreen stageScreen = canvas.GetComponentInChildren<UI_StageScreen>(true);

        dialogueUI = stageScreen.transform.Find("SpeechBubble");

        if (dialogueUI == null)
        {
            Debug.LogError("SpeechBubble을 찾을 수 없습니다.");
            return;
        }


        balloonImage =
            dialogueUI.GetComponentInChildren<Image>(true);


        dialogueText =
            dialogueUI.GetComponentInChildren<TextMeshProUGUI>(true);


        // 이미 실행 중이면 또 실행하지 않음
        if (!isDialoguePlaying)
        {
            StartCoroutine(ShowDialogues(data));
        }
    }


    private IEnumerator ShowDialogues(CustomerData data)
    {
        if (data == null)
            yield break;


        if (data.dialogues == null ||
            data.dialogues.Count == 0)
        {
            isDialogueFinished = true;
            isDialoguePlaying = false;
            yield break;
        }


        isDialoguePlaying = true;


        while (currentDialogueIndex < data.dialogues.Count)
        {
           
            while (GameManager.Instance.CurrentState
                   != GameState.PlayScene)
            {
                yield return null;
            }


            DialogueData dialogue =
                data.dialogues[currentDialogueIndex];


            
            dialogueUI.gameObject.SetActive(true);

            balloonImage.sprite =
                dialogue.balloonSprite;

            dialogueText.text =
                dialogue.dialogue;



            while (dialogueTimer < 2f)
            {
                if (GameManager.Instance.CurrentState
                    == GameState.PlayScene)
                {
                    dialogueTimer += Time.deltaTime;
                }

                yield return null;
            }


            // 현재 대사 완료
            dialogueTimer = 0f;

            currentDialogueIndex++;



            dialogueUI.gameObject.SetActive(false);


            

            float nextDialogueTimer = 0f;


            while (nextDialogueTimer < 0.5f)
            {
                if (GameManager.Instance.CurrentState
                    == GameState.PlayScene)
                {
                    nextDialogueTimer += Time.deltaTime;
                }

                yield return null;
            }


            // 다음 대사가 있으면 다시 표시
            if (currentDialogueIndex < data.dialogues.Count)
            {
                dialogueUI.gameObject.SetActive(true);
            }
        }


        dialogueUI.gameObject.SetActive(false);

        isDialogueFinished = true;
        isDialoguePlaying = false;
    }


    public void ResetItemProgress()
    {
        createdItemCount = 0;
        targetItemCount = 0;

        isItemCreating = false;

        todayItems.Clear();
    }


    public void ResetDialogueProgress()
    {
        currentDialogueIndex = 0;

        dialogueTimer = 0f;

        isDialogueFinished = false;

        isDialoguePlaying = false;
    }


    public void ResetCustomerProgress()
    {
        
        ResetItemProgress();

        
        ResetDialogueProgress();

        
        isPricePanelLocked = false;
    }
}