using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Button_OpenScreen : MonoBehaviour
{
    [SerializeField] UIType wantType;
    [SerializeField] bool wantToggle;
    [SerializeField] ScreenChangeType changeType;
    public void Open()
    {
        UIManager.ClaimOpenScreen(wantType, changeType);

    }

}
