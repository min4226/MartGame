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
        if (GameManager.Instance.CurrentState != GameState.PlayScene)
            return;
        UIManager.ClaimOpenScreen(wantType, changeType);
        
        createMethod.OnToggle();

    }

    public void GameQuitOpen()
    {
        if (GameManager.Instance.CurrentState != GameState.PlayScene)
            return;
        UIManager.ClaimOpenScreen(wantType, changeType);
    }

    public void RestartPlayButton()
    {
        Debug.Log($"ClaimOpenScreen »£√‚ : {wantType}, {changeType}");
        if (GameManager.Instance.CurrentState != GameState.PlayScene)
            return;
        UIManager.ClaimOpenScreen(wantType, changeType);
        createMethod.gameObject.SetActive(false);
        Debug.Log($"createmethod : {createMethod.name}");

    }

}
