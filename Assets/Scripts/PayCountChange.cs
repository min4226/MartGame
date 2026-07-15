using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PayCountChange : MonoBehaviour 
{
    public TextMeshProUGUI payWindowCount;
    [SerializeField]PayThingCount payThingCount;

    private void UpdateUI()
    {
        payWindowCount.text = payThingCount.payCount.ToString();
        
    }

    public void Init(PayThingCount target)
    {
        Debug.Log("paycountchange ÂÊ init ÇÔ¼ö");
        payThingCount = target;
        UpdateUI();
    }

    

    
}
