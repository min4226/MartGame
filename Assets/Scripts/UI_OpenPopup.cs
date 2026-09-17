using UnityEngine;

public class UI_OpenPopup : MonoBehaviour
{
    [SerializeField] UIType wantType;
    [SerializeField] bool wantToggle;
    [SerializeField] bool openOnTop = true;


    public void Open()
    {
        Debug.Log("UI 열기 버튼 클릭됨");

        UIBase opened = null;

        if (wantToggle)
        {
            opened = UIManager.ClaimToggleUI(wantType);
        }
        else
        {
            opened = UIManager.ClaimOpenUI(wantType);
        }

        Debug.Log("열린 UI: " + opened);

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
