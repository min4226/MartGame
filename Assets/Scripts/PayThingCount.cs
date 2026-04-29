using TMPro;
using UnityEngine;

public class PayThingCount : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Sprite rightButton;
    public Sprite leftButton;
    int payCount;
    void Start()
    {
        payCount = 0;
        text.text = payCount.ToString();
    }

    
    public void PressLeftButton()
    {
        if (payCount > 0)
        { 
            payCount--;
            text.text = payCount.ToString();
        } 
    }

    public void PressRightButton()
    {
        payCount++;
        text.text = payCount.ToString();
    }
}
