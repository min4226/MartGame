using UnityEngine;

public class UI_OpenPopup : MonoBehaviour
{
    [SerializeField] UIType wantType;
    [SerializeField] bool wantToggle;
    [SerializeField] bool openOnTop = true;
    

    public void Open()
    {
        Debug.Log("오픈 시작");
        Debug.Log($"wantToggle : {wantToggle}");
        UIBase opened = null;

        if (wantToggle)
        {
            Debug.Log("토글열기");
            opened = UIManager.ClaimToggleUI(wantType);
        }
        else
        {
            Debug.Log("ClaimOpenUI 호출");
            opened = UIManager.ClaimOpenUI(wantType);
            Debug.Log($"wanttype : {wantType}");
            Debug.Log($"opened = {opened}");
        }

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
