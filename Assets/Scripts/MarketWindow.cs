using UnityEngine;

public class MarketWindow : UIManager
{
    [SerializeField] GameObject _marketWindow;
    GameObject marketWindow => _marketWindow;
    [SerializeField] GameObject _stageScene;
    GameObject stageScene => _stageScene;

    void Awake()
    {
        _marketWindow = GameObject.FindGameObjectWithTag("MarketWindow");
        _stageScene = GameObject.FindGameObjectWithTag("PlayGame");
        marketWindow.SetActive(false);
    }
    public void Open()
    {
        UIManager.ClaimCloseUI(UIType.Option);
        stageScene.SetActive(false);
        //UIManager.ClaimOpenUI(UIType.MyMarket);
        marketWindow.SetActive(true);
    }
    public void Close()
    {
        marketWindow.SetActive(false);
    }
}
