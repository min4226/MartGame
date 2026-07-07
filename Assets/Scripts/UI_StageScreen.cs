using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_StageScreen : UI_ScreenBase
{
    [SerializeField] TMP_InputField inputField;
    [SerializeField] Button EnterButton;
    [SerializeField] GameObject correct;
    [SerializeField] GameObject fail;
    private void Awake()
    {
        GameManager.Instance.InputField = inputField;
        GameManager.Instance.EnterButton = EnterButton;
        GameManager.Instance.CorrectAnswer = correct;
        GameManager.Instance.FailAnswer = fail;
        inputField.gameObject.SetActive( false );
        EnterButton.gameObject.SetActive( false );
        correct.gameObject.SetActive( false );
        fail.gameObject.SetActive( false );
    }
}
