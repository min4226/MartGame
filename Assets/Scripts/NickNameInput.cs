using TMPro;
using UnityEngine;

public class NickNameInput : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nicknameInput;

    private void Start()
    {
        nicknameInput.text = GameManager.DB.resultData.nickname;
        Debug.Log($"nickname : {nicknameInput.text}");
    }
}
