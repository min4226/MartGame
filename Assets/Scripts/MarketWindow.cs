using UnityEngine;

public class MarketWindow : MonoBehaviour
{
    [SerializeField] GameObject marketWindow;
    [SerializeField] GameObject stageScene;
    
    public void Open()
    {
        Debug.Log("¹® ¿­¸²");
        marketWindow.SetActive(true);
        stageScene.SetActive(false);
    }
    public void Close()
    {
        Debug.Log("¹® ´ÝÈû");
        marketWindow.SetActive(false);
    }
}
