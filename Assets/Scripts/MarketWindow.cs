using UnityEngine;

public class MarketWindow : UIManager
{
    [SerializeField] GameObject marketWindow;
    [SerializeField] GameObject stageScene;

    void Awake()
    {
        marketWindow = GameObject.FindGameObjectWithTag("MarketWindow");
        stageScene = GameObject.FindGameObjectWithTag("PlayGame");
        marketWindow.SetActive(false);
    }
    public void Open()
    {
        //Debug.Log(marketWindow == null);
        UIManager.ClaimCloseUI(UIType.Option);
        stageScene.SetActive(false);
        //UIManager.ClaimOpenUI(UIType.MyMarket);
        marketWindow.SetActive(true);
    }
    /*public void Close()
    {
        Debug.Log("¹® ´ÝÈû");
        marketWindow.SetActive(false);
    }*/
}
