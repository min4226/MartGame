using TMPro;
using UnityEngine;

public class UserPayCount : MonoBehaviour
{
    [SerializeField] PayThingCount paythingCount;
    [SerializeField] GameObject mainShop;

    public void BuyUserPay()
    {
        TextMeshProUGUI countText = mainShop.transform.Find("CountCountText").GetComponent<TextMeshProUGUI>();

        countText.text = paythingCount._inputField.text;

        UIManager.ClaimOpenUI(UIType.MainShopWindow);

        paythingCount.ResetCount();
    }
}