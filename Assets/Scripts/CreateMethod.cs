using UnityEngine;
using UnityEngine.UI;


public class CreateMethod : MonoBehaviour
{
    
    [SerializeField] GameObject gameObjectPanel;
    [SerializeField] Toggle toggle;
    StageData currentData;
    bool isPlaying;

    void Awake()
    {
        toggle.isOn = false;
        gameObjectPanel.SetActive(false);
    }

    public void OnToggle()
    {
        
        gameObjectPanel.SetActive(true);
        
    }

    public void OnToggleCheck()
    { 
        toggle.isOn = true;
        gameObjectPanel.SetActive(false);
        GameManager.Instance.Stage.StartStage(0);
    }
}