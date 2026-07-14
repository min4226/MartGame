using UnityEngine;
using UnityEngine.UI;

public class ToPlayMode : MonoBehaviour
{
    GameObject marketScene;
    GameObject stageScene;
    void Awake()
    {
        marketScene = GameObject.FindGameObjectWithTag("MarketWindow");
        stageScene = GameObject.FindGameObjectWithTag("PlayGame");
    }
    public void OnToPlayMode()
    {
        if (marketScene == null)
        {
            marketScene = GameObject.FindGameObjectWithTag("MarketWindow");
        }
        marketScene.SetActive(false);
        UIManager.ClaimCloseUI(UIType.MyMarket);
        stageScene.SetActive(true);
        UIManager.ClaimOpenUI(UIType.Stage);
    }
}
