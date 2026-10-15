using TMPro;
using UnityEditor;
using UnityEngine;

public class RankingUpName : MonoBehaviour
{
    Transform canvas;
    GameObject processObj;
    
    public void OnRankingUpName()
    {
        
        GameObject[] objects = FindObjectsByType<GameObject>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (GameObject obj in objects)
        {
            if (obj.name == "ProcessObj")
            {
                processObj = obj;
                break;
            }
        }
        Debug.Log($"processobj : {processObj}");

        TextMeshProUGUI troubleCustomerName =
            FindFirstObjectByType<TextMeshProUGUI>(FindObjectsInactive.Include);

        GameManager.DB.SaveRankingData(troubleCustomerName.text);
        processObj.gameObject.SetActive(true);
        
        GameObject troubleCustomerNamePanelPopup = GameObject.FindGameObjectWithTag("troubleCustomerNamePanelPopup");
        if (troubleCustomerNamePanelPopup != null)
        {
            IOpenable openable = troubleCustomerNamePanelPopup.GetComponent<IOpenable>();

            if (openable != null)
            {
                openable.Close();
            }
        }
        Debug.Log($"troubleCustomerNamePanelPopup : {troubleCustomerNamePanelPopup}");
    }
}