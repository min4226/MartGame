using TMPro;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class WriteTroubleCustomerName : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI troubleCustomerName;
    [SerializeField] private TMP_InputField writeTroubleCustomerName;
    Transform troubleName;
    
    public void OnWriteTroubleCustomerNameButton()
    {
        GameObject canvas = GameObject.Find("Canvas");
        Debug.Log($"canvas : {canvas}");
        TMP_InputField inputField =
            canvas.transform.Find("WriteTroubleCustomerName").GetComponent<TMP_InputField>();
        Debug.Log($"writetroublecustomername : {inputField}");
            return;

        troubleCustomerName.text = inputField.text;

        Debug.Log($"입력한 이름 : {inputField.text}");
    }
}