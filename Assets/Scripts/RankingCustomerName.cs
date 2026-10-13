using System.Xml.Serialization;
using TMPro;
using UnityEngine;

public class RankingCustomerName : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI troubleCustomerNameText;

    public async void LoadName()
    {
        DBManager.RankingData data =
            await GameManager.DB.RegistrationRanking();

        if (data == null)
        {
            Debug.Log("랭킹 데이터가 없습니다.");
            return;
        }

        troubleCustomerNameText.text = data.troubleCustomerName;
    }
}
