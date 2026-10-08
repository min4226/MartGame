using TMPro;
using UnityEngine;

public class RankingUpName : MonoBehaviour
{
    public void OnRankingUpName()
    {
        TextMeshProUGUI troubleCustomerName =
            FindFirstObjectByType<TextMeshProUGUI>(FindObjectsInactive.Include);

        GameManager.DB.SaveRankingData(troubleCustomerName.text);
    }
}