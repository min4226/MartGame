using UnityEngine;
using UnityEngine.UI;

public class PricePanelController : MonoBehaviour
{
    [SerializeField] private GameObject pricePanel;
    [SerializeField] private Button arrowButton;
    [SerializeField] private GameObject lockImage;

    private bool isLocked = false;

    private void Start()
    {
        if (lockImage != null)
            lockImage.SetActive(false);
    }

    public void OpenPricePanel()
    {
        if (isLocked)
            return;

        // 한 번 열었으므로 잠금
        isLocked = true;

        if (arrowButton != null)
            arrowButton.interactable = false;

        if (lockImage != null)
            lockImage.SetActive(true);
    }

    public void ResetButton()
    {
        isLocked = false;

        if (arrowButton != null)
            arrowButton.interactable = true;

        if (lockImage != null)
            lockImage.SetActive(false);
    }
}