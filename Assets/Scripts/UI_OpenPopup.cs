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
    }

    public void Close()
    {
        UIManager.ClaimCloseUI(wantType);
    }

}
