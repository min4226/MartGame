using TMPro;
using UnityEngine;

public class RewardUI : MonoBehaviour
{
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private TMP_Text fameText;

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (GameManager.DB == null)
        {
            Debug.LogWarning("DBManager가 없습니다.");
            return;
        }

        if (GameManager.DB.resultData == null)
        {
            Debug.LogWarning("Firebase 데이터를 아직 불러오지 못했습니다.");
            return;
        }

        coinText.text = GameManager.DB.resultData.coin.ToString();
        fameText.text = GameManager.DB.resultData.fame.ToString();

        Debug.Log($"RewardUI 갱신 - Coin: {GameManager.DB.resultData.coin}, Fame: {GameManager.DB.resultData.fame}");
    }
}