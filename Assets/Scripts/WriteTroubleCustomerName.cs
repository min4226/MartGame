using TMPro;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class WriteTroubleCustomerName : MonoBehaviour
{ 
    Transform troubleName;
    public void OnWriteTroubleCustomerNameButton()
    {
        TextMeshProUGUI troubleCustomerName = FindFirstObjectByType<TextMeshProUGUI>(FindObjectsInactive.Include);
        Debug.Log($"troublecustomername : {troubleCustomerName}");
        TMP_InputField writeCustomerNameInputfield = GameObject.Find("WriteTroubleCustomerName").GetComponent<TMP_InputField>();
        Debug.Log($"writetroublecustomername : {writeCustomerNameInputfield}");
        troubleCustomerName.gameObject.SetActive(true);
         
        troubleCustomerName.text = writeCustomerNameInputfield.text;
        GameManager.DB.SaveTroubleCustomerName(troubleCustomerName.text);
    }
}