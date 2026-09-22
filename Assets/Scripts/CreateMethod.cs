using UnityEngine;
using UnityEngine.UI;

// 게임 방법 판넬
public class CreateMethod : MonoBehaviour
{
    [SerializeField] GameObject gameObjectPanel;
    [SerializeField] Toggle toggle;

    void Awake()
    {
        gameObjectPanel.SetActive(false);
    }

    public void OnToggle()
    {
        gameObjectPanel.SetActive(true);
    }

    public void CloseToggle()
    {
        gameObjectPanel.SetActive(false);
    }

    public void OnToggleCheck()
    {
        gameObjectPanel.SetActive(false);

        GameManager.Instance.Stage.StartStage(0);
    }
}