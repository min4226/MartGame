using UnityEngine;
using UnityEngine.UI;

public class PricePanelController : MonoBehaviour
{
    [SerializeField] private GameObject pricePanel;
    [SerializeField] private Button arrowButton;
    
    private bool isLocked = false;

    public void OpenPricePanel()
    {
        // return은 조건이 true일 때만 실행이 됨 현재 isLocked는 false이기
        // 때문에 return을 실행하지 않음
        if (isLocked)
            return;

        // 한 번 열었으므로 잠금
        isLocked = true;

        if (arrowButton != null)
            arrowButton.interactable = false;
            // interactable : ui를 사용자가 누를 수 있는지
            // isLocked가 true이기 때문에 ui를 사용자가 누르지
            // 못하게 막음
    }

    public void ResetButton()
    {
        isLocked = false;

        if (arrowButton != null)
            arrowButton.interactable = true;

    }
}