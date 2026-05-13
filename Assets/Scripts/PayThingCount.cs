using TMPro;
using UnityEngine;

public class PayThingCount : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject rightButton;
    public GameObject leftButton;
    public int payCount; 
    void Start()
    {
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
