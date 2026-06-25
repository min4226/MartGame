using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ExScreen : UI_ScreenBase
{
    private void Start()
    {
        TMP_InputField input = GetComponentInChildren<TMP_InputField>(true);
        Debug.Log($"gameobject name : {gameObject.name}");
        Debug.Log(input);
        Debug.Log($"inputfield »ý¼ºµÊ : {input == null}");
    }

}
