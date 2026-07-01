using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_StageScreen : UI_ScreenBase
{
    [SerializeField] TMP_InputField inputField;
    [SerializeField] Button EnterButton;
    private void Awake()
    {
        GameManager.Instance.InputField = inputField;
        GameManager.Instance.EnterButton = EnterButton;
        inputField.gameObject.SetActive( false );
        EnterButton.gameObject.SetActive( false );
    }
}
