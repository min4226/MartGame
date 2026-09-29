using TMPro;
using UnityEngine;

public class RewardUI : MonoBehaviour
{
    [SerializeField] private TMP_Text coinText;
    public TMP_Text CoinText => coinText;
    [SerializeField] private TMP_Text fameText;
    public TMP_Text FameText => fameText;
    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        
        coinText.text = GameManager.DB.resultData.coin.ToString();
        fameText.text = GameManager.DB.resultData.fame.ToString();

        
    }
}