using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_StageScreen : UI_ScreenBase
{
    [SerializeField] TMP_InputField inputField;
    [SerializeField] Button EnterButton;
    [SerializeField] GameObject correct;
    [SerializeField] GameObject fail;
    [SerializeField] GameObject stageClearPanel;
    [SerializeField] GameObject stageClearPanelFail;
    private void Awake()
    {
        GameManager.Instance.InputField = inputField;
        GameManager.Instance.EnterButton = EnterButton;
        GameManager.Instance.CorrectAnswer = correct;
        GameManager.Instance.FailAnswer = fail;
        GameManager.Instance.StageClearResultPanel = stageClearPanel;
        GameManager.Instance.StageClearResultPanelFail = stageClearPanelFail;

        inputField.gameObject.SetActive( false );
        stageClearPanel.gameObject.SetActive( false );
        stageClearPanelFail.gameObject.SetActive( false );
        EnterButton.gameObject.SetActive( false );
        correct.gameObject.SetActive( false );
        fail.gameObject.SetActive( false );
    }
}
