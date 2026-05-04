using TMPro;
using UnityEngine;

public class PayThingCount : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Sprite rightButton;
    public Sprite leftButton;
    public static int payCount; // 같은 스크립트 내여도 변수는 서로 다르게 값을 가지고 있기 때문에 static 사용
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
