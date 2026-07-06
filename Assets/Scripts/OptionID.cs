using TMPro;
using UnityEngine;

public class OptionID : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI idInfoText;
    public void SetID(string id)
    { 
        idInfoText.text = id.ToString();
        Debug.Log($"idInfoText : {idInfoText.text == null} ");
        Debug.Log(idInfoText.text);

    }
}
