using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RechangeButton : MonoBehaviour
{
    public TMP_InputField reChange;
    TMP_InputField optionNick;

    public void RechangeNick()
    {
        GameObject IdInfo = GameObject.Find("idInfo");
        optionNick = IdInfo.GetComponentInChildren<TMP_InputField>();
        string nick = reChange.text;
        GameManager.DB.NickNameChange(nick);
        optionNick.text = nick; // 얘 null 뜸

    }
}
