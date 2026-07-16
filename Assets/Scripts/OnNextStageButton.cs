using UnityEngine;

public class OnNextStageButton : MonoBehaviour
{
    GameObject stageClearPanel;
    private void Awake()
    {
        stageClearPanel = GameManager.Instance.StageClearResultPanel;
    }
    public void OnStageNextButton()
    {
        Debug.Log("다음스테이지 실행");
        stageClearPanel.SetActive(false);
        GameManager.Instance.Stage.NextStage();
    }
}
