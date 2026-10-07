using System;
using UnityEngine;

[Serializable]
public struct UIClaim
{
    public string prefabName;
    public UIType uiType;
    public bool isOpen;
    public bool isOverlay;
    public UIBase Execute()
    {
        UIBase result = UIManager.ClaimGetUI(uiType);

        if (!result)
        {
            if(isOverlay) result = UIManager.ClaimOverlay(uiType, prefabName);
            else result = UIManager.ClaimCreateUI(uiType, prefabName);
        }

        if (!result) return result;

        if (result is IOpenable openTarget)
        { 
            if(isOpen) openTarget.Open();
            else openTarget.Close();
        }

        return result;
    }


}
public class UI_ScreenBase : UIBase , IOpenable
{
    [SerializeField] UIClaim[] requiredUI;

    public bool IsOpen => gameObject.activeSelf; // activeself : 현재 상태를 나타냄 true라면 true를 반환
    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Toggle()
    {

        gameObject.SetActive(!IsOpen);
    }

    public override void Registration(UIManager manager)
    {
        base.Registration(manager);
        if (requiredUI is null) return;

        foreach (UIClaim currentClaim in requiredUI)
        {
            currentClaim.Execute();
        }
    }
}
