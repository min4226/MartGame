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
        UIManager.ClaimCloseUI(UIType.Option);
        stageScene.SetActive(false);
        UIManager.ClaimCloseUI(UIType.Stage);
        marketWindow.SetActive(true);
    }
    
}
