using UnityEngine;

public class UI_OpenPopup : MonoBehaviour
{
    [SerializeField] UIType wantType;
    [SerializeField] bool wantToggle;
    [SerializeField] bool openOnTop = true;
    public PayThingCount payThingCount;
    public PayCountChange payCountChange;

    public void Open()
    {
        UIBase opened = null;

        
        if (wantToggle) opened = UIManager.ClaimToggleUI(wantType);
        else opened = UIManager.ClaimOpenUI(wantType);

        if (openOnTop && opened)
        { 
            opened.transform.SetAsLastSibling();
            
        }
        
        
        payThingCount = GetComponent<PayThingCount>(); // shopwindow -> gameobejct 이런 식으로 찾기
        Debug.Log($"paythingcount : {payThingCount}");
        payCountChange = GetComponentInChildren<PayCountChange>(); // paywindow -> inputfield 이런 식으로 찾기
        Debug.Log($"paycountchange : {payCountChange}");

        if (payThingCount != null && payCountChange != null)
        {
            payCountChange.Init(payThingCount);
        }
    }

    public void Close()
    {
        UIManager.ClaimCloseUI(wantType);
    }

}
