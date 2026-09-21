using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using static UnityEngine.Rendering.DebugUI;

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

    // 가격 패널 관련
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

        if (createdItemCount >= targetItemCount)
            return;

        if (isItemCreating)
            return;

        StartCoroutine(ItemCreate());
    }

    public void Init(StageContainer data)
    {
        Debug.Log("NormalCustomer Init 호출");
        Debug.Log("현재 상태: " + GameManager.Instance.CurrentState);

        if (GameManager.Instance.CurrentState != GameState.PlayScene)
        {
            Debug.Log("PlayScene이 아니라서 Init 중단");
            return;
        }

        stageContainer = data;

        Debug.Log("stageContainer 설정 완료");
    }


    public IEnumerator ItemCreate()
    {
        Debug.Log("===== ItemCreate 시작 =====");

        if (GameManager.Instance.currentCustomer == null)
            yield break;

        isItemCreating = true;

        int stageIndex = GameManager.Instance.Stage.CurrentIndex;

        StageData stageData =
            stageContainer.stageDatas[stageIndex];

        targetItemCount = stageData.normalCustomerItemCount;

        // 새 손님일 때만 초기화
        if (createdItemCount == 0)
        {
            todayItems.Clear();

            PricePanelController priceController =
                FindFirstObjectByType<PricePanelController>();

            if (priceController != null)
                priceController.ResetButton();

            trigger.SetItemCount(targetItemCount);
        }

        // 이미 생성한 상품이 있다면 남은 상품만 생성
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

            todayItems.Add(itemData);

            normalItem = Instantiate(
                itemData.itemPrefab,
                itemPool,
                false
            );

            normalItem.transform.localScale = Vector3.one;

            if (!normalItem.TryGetComponent<MoveRight>(
                out var move))
            {
                move = normalItem.AddComponent<MoveRight>();
            }

            move.speed = speed;

            createdItemCount++;

            Debug.Log(
                $"상품 생성 {createdItemCount}/{targetItemCount}"
            );

            while (GameManager.Instance.CurrentState != GameState.PlayScene)
            {
                yield return null;
            }

            yield return StartCoroutine(WaitForPlaySeconds(2f));
        }

        OnChangedTodayItems?.Invoke(todayItems);

        isItemCreating = false;

        Debug.Log("===== ItemCreate 완료 =====");
    }
    private IEnumerator WaitForPlaySeconds(float seconds)
    {
        float timer = 0f;

        while (timer < seconds)
        {
            if (GameManager.Instance.CurrentState == GameState.PlayScene)
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
            Debug.Log("이미 가격표를 확인했습니다.");
            return;
        }


        isPricePanelLocked = true;


        pricePanel.SetActive(true);


        priceArrowButton.interactable = false;


        Debug.Log("가격표 열림 / 버튼 잠금");
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
        UI_StageScreen stageScreen =
            canvas.GetComponentInChildren<UI_StageScreen>(true);


        dialogueUI =
            stageScreen.transform.Find("SpeechBubble");


        balloonImage =
            dialogueUI.GetComponentInChildren<Image>(true);


        dialogueText =
            dialogueUI.GetComponentInChildren<TextMeshProUGUI>(true);


        int randomIndex =
            Random.Range(0, data.dialogues.Count);


        DialogueData dialogue =
            data.dialogues[randomIndex];


        balloonImage.sprite =
            dialogue.balloonSprite;


        dialogueText.text =
            dialogue.dialogue;


        StartCoroutine(
            ShowDialogues(data)
        );
    }


    private IEnumerator ShowDialogues(CustomerData data)
    {
        dialogueUI.gameObject.SetActive(true);


        foreach (DialogueData dialogue in data.dialogues)
        {
            balloonImage.sprite =
                dialogue.balloonSprite;


            dialogueText.text =
                dialogue.dialogue;


            yield return new WaitForSeconds(2f);


            dialogueUI.gameObject.SetActive(false);


            yield return new WaitForSeconds(0.5f);


            dialogueUI.gameObject.SetActive(true);
        }


        dialogueUI.gameObject.SetActive(false);
    }
    public void ResetItemProgress()
    {
        createdItemCount = 0;
        targetItemCount = 0;
        isItemCreating = false;

        todayItems.Clear();
    }
}