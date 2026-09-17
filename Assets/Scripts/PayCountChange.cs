using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PayCountChange : MonoBehaviour 
{
    public TextMeshProUGUI payWindowCount;
    public int shopPayCount;

    private void UpdateUI()
    {
        payWindowCount.text = shopPayCount.ToString();
        
    }

    public void Init(int target)
    {
        Debug.Log("paycountchange �� init �Լ�");
        shopPayCount = target;
        UpdateUI();
    }

    

    
}
