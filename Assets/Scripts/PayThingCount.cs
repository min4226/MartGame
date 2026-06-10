using TMPro;
using UnityEngine;

public class PayThingCount : MonoBehaviour
{
    public GameObject rightButton;
    public GameObject leftButton;
    public TMP_InputField inputField;
   
    public int payCount;
    


    void Start()
    {
        
        inputField.text = payCount.ToString();
    }

    

    public void PressLeftButton()
    {
        if (payCount > 0)
        { 
            payCount--;
            inputField.text = payCount.ToString();
            
        } 
    }

    public void PressRightButton()
    {
        payCount++;
        inputField.text = payCount.ToString();
    }




}
