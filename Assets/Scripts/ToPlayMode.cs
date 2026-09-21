using UnityEngine;
using UnityEngine.UI;

public class ToPlayMode : MonoBehaviour
{
    GameObject marketScene;
    GameObject stageScene;
    FurnitureDrag furnitureDrag;
    
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
        

        Debug.Log($"furnituredrag : {furnitureDrag}");

        if (marketScene == null)
        {
            marketScene = GameObject.FindGameObjectWithTag("MarketWindow");
        }

        marketScene.SetActive(false);

        UIManager.ClaimCloseUI(UIType.MyMarket);

        if (furnitureDrag.PlacedFurniture != null)
        {
            furnitureDrag.PlacedFurniture.SetActive(false);
        }

        if (furnitureDrag.FurnitureSlot != null)
        {
            furnitureDrag.FurnitureSlot.SetActive(false);
        }

        stageScene.SetActive(true);

        UIManager.ClaimOpenScreen(UIType.Stage);
        


    }
}
