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


    private void Awake()
    {
        canvas = FindFirstObjectByType<Canvas>();
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

        todayItems.Clear();


        // ---------------------------------
        // 가격표 버튼 초기화
        // ---------------------------------

        PricePanelController priceController =
            FindFirstObjectByType<PricePanelController>();

        if (priceController != null)
            priceController.ResetButton();


        // ---------------------------------
        // 현재 스테이지 확인
        // ---------------------------------

        int stageIndex =
            GameManager.Instance.Stage.CurrentIndex;

        StageData stageData =
            stageContainer.stageDatas[stageIndex];


        // ---------------------------------
        // 스테이지 7부터 도둑 등장
        // ---------------------------------

        if (stageIndex >= 6 && stageData.thiefCustomerCount > 0)
        {
            ThiefManager thiefManager =
                FindFirstObjectByType<ThiefManager>();

            if (thiefManager != null)
            {
                thiefManager.StartThief();
            }
        }


        yield return new WaitForSeconds(2f);


        // ---------------------------------
        // 일반 손님 상품 개수
        // ---------------------------------

        int count =
            stageData.normalCustomerItemCount;

        trigger.SetItemCount(count);


        // ---------------------------------
        // 오늘 손님이 구매할 상품 랜덤 생성
        // ---------------------------------

        for (int i = 0; i < count; i++)
        {
            if (items.Length == 0)
                yield break;


            NormalCustomerItem customerItem =
                items[Random.Range(0, items.Length)];


            if (customerItem.item.Length == 0)
                continue;


            ItemData itemData =
                customerItem.item[
                    Random.Range(0, customerItem.item.Length)
                ];


            todayItems.Add(itemData);
        }


        // 상품 목록 전달
        OnChangedTodayItems?.Invoke(todayItems);


        // ---------------------------------
        // 상품을 하나씩 생성
        // ---------------------------------

        foreach (ItemData itemData in todayItems)
        {
            normalItem = Instantiate(
                itemData.itemPrefab,
                itemPool,
                false
            );


            normalItem.transform.localScale =
                Vector3.one;


            if (!normalItem.TryGetComponent<MoveRight>(
                out var move))
            {
                move =
                    normalItem.AddComponent<MoveRight>();
            }


            move.speed = speed;


            yield return new WaitForSeconds(2f);
        }
    }


    // ---------------------------------
    // 가격표 열기
    // ---------------------------------

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


    // ---------------------------------
    // 상품 총 가격
    // ---------------------------------

    public int ItemTotalValue(List<ItemData> todayItems)
    {
        int total = 0;


        foreach (ItemData currentItem in todayItems)
        {
            total += currentItem.itemBasePrice;
        }


        return total;
    }


    // ---------------------------------
    // 손님 대사
    // ---------------------------------

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
}