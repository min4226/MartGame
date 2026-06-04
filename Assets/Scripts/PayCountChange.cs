using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PayCountChange : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI payWindowCount;
    [SerializeField] PayThingCount payThingCount;

    // Update is called once per frame
    void Update()
    {
        
        payWindowCount.text = payThingCount.payCount.ToString();

        
        Debug.Log("payThingCount instance: " + payThingCount.gameObject.name);
        Debug.Log("paywindowcount instance" + payWindowCount.gameObject.name);
        Debug.Log("payCount: " + payThingCount.payCount);
    }

}
