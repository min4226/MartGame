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
    }

    public void OnToPlayMode()
    {
        FurnitureDrag furnitureDrag =
            FindFirstObjectByType<FurnitureDrag>(
                FindObjectsInactive.Include
            );

        if (marketScene == null)
        {
            marketScene = GameObject.FindGameObjectWithTag("MarketWindow");
        }

        marketScene.SetActive(false);

        UIManager.ClaimCloseUI(UIType.MyMarket);

        placedFurniture = GameObject.Find("PlacedFurniture");

        if (placedFurniture != null)
        {
            placedFurniture.SetActive(false);
        }
        if (furnitureDrag.FurnitureSlot != null)
        {
            furnitureDrag.FurnitureSlot.SetActive(false);
        }
        
        GameManager.Instance.CurrentState = GameState.PlayScene;

        stageScene.SetActive(true);

        UIManager.ClaimOpenScreen(UIType.Stage);

    }
}