using UnityEngine;
using UnityEngine.UI;

public class FurnitureDrag : MonoBehaviour
{
    [SerializeField] private GameObject furnitureSlot;
    public GameObject FurnitureSlot => furnitureSlot;
    [SerializeField] private Transform placedFurnitureParent;

    private RectTransform furnitureSlotRect;
    private Image furnitureSlotImage;

    private ShopItemData shopItemData;

    // 현재 어떤 인벤토리 슬롯에서 가져왔는지 저장
    private MyItemSlot currentSlot;
    GameObject placedFurniture;
    public GameObject PlacedFurniture => placedFurniture;
    private void OnDisable()
    {
        InputManager.OnMouseUPEvent -= EndDrag;
    }


    public void StartDrag(ShopItemData data, MyItemSlot slot)
    {
        if (data == null)
            return;

        if (slot == null)
            return;

        if (furnitureSlot == null)
        {
            Debug.LogError("FurnitureSlot이 연결되지 않음");
            return;
        }

        if (placedFurnitureParent == null)
        {
            Debug.LogError("PlacedFurniture가 연결되지 않음");
            return;
        }

        furnitureSlotRect =
            furnitureSlot.GetComponent<RectTransform>();

        furnitureSlotImage =
            furnitureSlot.GetComponent<Image>();

        if (furnitureSlotImage == null)
        {
            Debug.LogError("FurnitureSlot에 Image가 없음");
            return;
        }

        // 선택한 가구 저장
        shopItemData = data;

        // 어느 인벤토리 슬롯에서 선택했는지 저장
        currentSlot = slot;

        // 가구 이미지 표시
        furnitureSlotImage.sprite = data.shopItemSprite;

        // FurnitureSlot 활성화
        furnitureSlot.SetActive(true);

        // 현재 마우스 위치로 이동
        furnitureSlotRect.position = Input.mousePosition;

        // 기존 이벤트 중복 방지
        InputManager.OnMouseUPEvent -= EndDrag;
        InputManager.OnMouseUPEvent += EndDrag;
    }


    private void Update()
    {
        if (shopItemData == null)
            return;

        if (furnitureSlotRect == null)
            return;

        // 마우스를 따라다님
        furnitureSlotRect.position = Input.mousePosition;
    }


    private void EndDrag(bool value)
    {
        Debug.Log($"현재 UIType : {UIManager.CurrentScreen}");

        if (shopItemData == null)
            return;

        if (UIManager.CurrentScreen == UIType.MyMarket)
        {
            placedFurniture = Instantiate(furnitureSlot, placedFurnitureParent);

            RectTransform placedRect = placedFurniture.GetComponent<RectTransform>();

            placedRect.position = furnitureSlotRect.position;

            FurnitureDrag drag = placedFurniture.GetComponent<FurnitureDrag>();

            if (drag != null)
            {
                Destroy(drag);
            }
        }

        if (currentSlot != null)
        {
            currentSlot.UseItem();
        }

        shopItemData = null;
        currentSlot = null;

        InputManager.OnMouseUPEvent -= EndDrag;

        Debug.Log("가구 배치 완료!");
    }
}