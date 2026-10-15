using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class FurnitureSaveData
{
    public string furnitureId;
    public Vector2 anchoredPosition;
}

[Serializable]
public class FurnitureLayoutData
{
    public List<FurnitureSaveData> furniture = new();
}

public class FurnitureLayoutSave : MonoBehaviour
{
    private const string SaveKey = "MyMarketFurnitureLayout";

    [SerializeField] private ShopData shopData;
    [SerializeField] private GameObject furnitureSlotPrefab;
    [SerializeField] private RectTransform placedFurnitureParent;

    private void Start()
    {
        LoadLayout();
    }

    public void SaveLayout()
    {
        FurnitureLayoutData data = new FurnitureLayoutData();

        foreach (Transform child in placedFurnitureParent)
        {
            PlacedFurnitureUI placed =
                child.GetComponent<PlacedFurnitureUI>();

            if (placed == null)
                continue;

            RectTransform rect = child.GetComponent<RectTransform>();

            if (rect == null)
                continue;

            data.furniture.Add(new FurnitureSaveData
            {
                furnitureId = placed.FurnitureId,
                anchoredPosition = rect.anchoredPosition
            });
        }

        string json = JsonUtility.ToJson(data);

        PlayerPrefs.SetString(SaveKey, json);
        PlayerPrefs.Save();

        Debug.Log("가게 배치 저장 완료!");
    }

    public void LoadLayout()
    {
        // 기존에 불러온 가구만 제거
        for (int i = placedFurnitureParent.childCount - 1; i >= 0; i--)
        {
            Transform child = placedFurnitureParent.GetChild(i);

            if (child.GetComponent<PlacedFurnitureUI>() != null)
            {
                Destroy(child.gameObject);
            }
        }

        if (!PlayerPrefs.HasKey(SaveKey))
        {
            Debug.Log("저장된 가구 배치가 없습니다.");
            return;
        }

        string json = PlayerPrefs.GetString(SaveKey);
        FurnitureLayoutData data =
            JsonUtility.FromJson<FurnitureLayoutData>(json);

        if (data == null || data.furniture == null)
            return;

        foreach (FurnitureSaveData saved in data.furniture)
        {
            ShopItemData item = FindFurniture(saved.furnitureId);

            if (item == null)
            {
                Debug.LogWarning(
                    $"가구 데이터를 찾을 수 없습니다: {saved.furnitureId}");
                continue;
            }

            GameObject obj = Instantiate(
                furnitureSlotPrefab,
                placedFurnitureParent
            );

            obj.SetActive(true);

            Image image = obj.GetComponent<Image>();

            if (image != null)
            {
                image.sprite = item.shopItemSprite;
            }

            RectTransform rect = obj.GetComponent<RectTransform>();
            rect.anchoredPosition = saved.anchoredPosition;

            PlacedFurnitureUI placed =
                obj.GetComponent<PlacedFurnitureUI>();

            if (placed == null)
            {
                placed = obj.AddComponent<PlacedFurnitureUI>();
            }

            placed.SetFurnitureId(item.furnitureId);

            // 배치된 가구에는 드래그용 스크립트가 필요하지 않음
            FurnitureDrag drag = obj.GetComponent<FurnitureDrag>();

            if (drag != null)
            {
                Destroy(drag);
            }
        }

        Debug.Log("가게 배치 불러오기 완료!");
    }

    private ShopItemData FindFurniture(string furnitureId)
    {
        foreach (ShopItemData item in shopData.items)
        {
            if (item != null && item.furnitureId == furnitureId)
            {
                return item;
            }
        }

        return null;
    }

    public void DeleteSavedLayout()
    {
        PlayerPrefs.DeleteKey(SaveKey);
        PlayerPrefs.Save();

        Debug.Log("저장된 가구 배치를 삭제했습니다.");
    }
}