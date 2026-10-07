using UnityEngine;

public class PurchaseConfirmButton : MonoBehaviour
{
    private RewardUI rewardui;

    public void ConfirmPurchase()
    {
        GameObject userCoinFame = GameObject.FindGameObjectWithTag("userCoinFame");
        Debug.Log($"usercoinfame : {userCoinFame}");
        rewardui = userCoinFame.GetComponentInChildren<RewardUI>();
        Debug.Log($"rewardui : {rewardui}");
        
        if (ShopInventory.Instance == null)
            return;

        
        ShopItemData selectedItem = ShopInventory.Instance.GetSelectedItem();

        
        if (selectedItem == null)
            return;

        
        Reward reward = selectedItem.reward;

        
        int currentCoin = int.Parse(rewardui.CoinText.text);
        int currentFame = int.Parse(rewardui.FameText.text);

        
        currentCoin -= reward.coin;
        currentFame -= reward.fame;

        
        rewardui.CoinText.text = currentCoin.ToString();
        rewardui.FameText.text = currentFame.ToString();

        
        GameManager.DB.resultData.coin = currentCoin;
        GameManager.DB.resultData.fame = currentFame;

        // Firebase 저장
        GameManager.DB.SaveRewardData(GameManager.DB.resultData.coin,
            GameManager.DB.resultData.fame);

        // 실제 아이템 구매 처리
        ShopInventory.Instance.BuySelectedItem();

    }
}