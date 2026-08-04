using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RechangeButton : MonoBehaviour
{
    public TMP_InputField reChange;
    TextMeshProUGUI optionNick;

    public void RechangeNick()
    {
        GameObject IdInfo = GameObject.Find("idInfo");
        Debug.Log($"idinfo : {IdInfo}");
        TextMeshProUGUI nickname = IdInfo.transform.Find("nicknameText").GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log($"nicknametransform : {nickname}");
        //optionNick = IdInfo.GetComponentInChildren<TextMeshProUGUI>();
        //Debug.Log($"optionnick : {optionNick}");

        string nick = reChange.text;
        GameManager.DB.NickNameChange(nick);
        Debug.Log($"nick : {nick}");
        nickname.text = nick;

    }
}
