using UnityEngine;
using UnityEngine.UI;

public class ToPlayMode : MonoBehaviour
{
    GameObject marketScene;
    GameObject stageScene;
    FurnitureDrag furnitureDrag;
    GameObject placedFurniture;

    void Awake()
    {
        marketScene = GameObject.FindGameObjectWithTag("MarketWindow");
        stageScene = GameObject.FindGameObjectWithTag("PlayGame");

        Debug.Log($"[ToPlayMode Awake] marketScene = {marketScene}");
        Debug.Log($"[ToPlayMode Awake] stageScene = {stageScene}");
    }

    public void OnToPlayMode()
    {
        Debug.Log("========== [ToPlayMode 시작] ==========");
        Debug.Log($"[1] OnToPlayMode 시작 / CurrentState = {GameManager.Instance.CurrentState}");

        FurnitureDrag furnitureDrag =
            FindFirstObjectByType<FurnitureDrag>(
                FindObjectsInactive.Include
            );

        Debug.Log($"[2] FurnitureDrag = {furnitureDrag}");

        if (marketScene == null)
        {
            marketScene = GameObject.FindGameObjectWithTag("MarketWindow");
            Debug.Log($"[3] marketScene 다시 찾음 = {marketScene}");
        }

        Debug.Log($"[4] MarketWindow 끄기 전 / CurrentState = {GameManager.Instance.CurrentState}");

        marketScene.SetActive(false);

        Debug.Log($"[5] MarketWindow 끈 후 / CurrentState = {GameManager.Instance.CurrentState}");

        UIManager.ClaimCloseUI(UIType.MyMarket);

        placedFurniture = GameObject.Find("PlacedFurniture");

        if (placedFurniture != null)
        {
            Debug.Log($"[6] PlacedFurniture 발견 = {placedFurniture}");
            placedFurniture.SetActive(false);
        }
        else
        {
            Debug.Log("[6] PlacedFurniture 없음");
        }

        if (furnitureDrag.FurnitureSlot != null)
        {
            Debug.Log("[7] FurnitureSlot 끄기");
            furnitureDrag.FurnitureSlot.SetActive(false);
        }
        else
        {
            Debug.Log("[7] FurnitureSlot 없음");
        }

        Debug.Log($"[8] Background 켜기 전 / CurrentState = {GameManager.Instance.CurrentState}");

        // 일단 상태 변경은 여기서 해보자
        GameManager.Instance.CurrentState = GameState.PlayScene;

        Debug.Log($"[9] PlayScene으로 변경 후 / CurrentState = {GameManager.Instance.CurrentState}");

        stageScene.SetActive(true);

        Debug.Log($"[10] Background 켠 후 / CurrentState = {GameManager.Instance.CurrentState}");

        UIManager.ClaimOpenScreen(UIType.Stage);

        Debug.Log($"[11] Stage UI 연 후 / CurrentState = {GameManager.Instance.CurrentState}");
        Debug.Log("========== [ToPlayMode 끝] ==========");
    }
}