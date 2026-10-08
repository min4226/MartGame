using TMPro;
using UnityEngine;

public class RankingSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI userName;
    [SerializeField] private TextMeshProUGUI customerName;
    [SerializeField] private TextMeshProUGUI thumbsCount;

    public void Init(DBManager.RankingData data) 
    {
        userName.text = data.nickname;
        customerName.text = data.troubleCustomerName;
        thumbsCount.text = data.heart.ToString();

        gameObject.SetActive(true);
    }
}