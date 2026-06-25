using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Button_OpenScreen : MonoBehaviour
{
    [SerializeField] UIType wantType;
    [SerializeField] bool wantToggle;
    [SerializeField] ScreenChangeType changeType;
    CreateMethod createMethod;

    private void Awake()
    {
        createMethod = GameManager.Instance.CreateMethod;
    }

    public void Open()
    {
        UIManager.ClaimOpenScreen(wantType, changeType);
        createMethod.OnToggle();

    }

}
