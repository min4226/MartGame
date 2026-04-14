using NUnit.Framework;
using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;

// 드래그 가능한 기능들 적는 곳
public class UI_MovableScreen : UI_ScreenBase
{

    Vector3 popupPosition = Vector3.zero;
    Vector3 popupShift = new(20.0f, -20.0f);

    [SerializeField] List<UIBase> popupList = new();

    UI_DraggableWindow currentDragTarget = null;

    public override void Registration(UIManager manager)
    {
        base.Registration(manager);
        InputManager.OnCancel += (value) => UIManager.ClaimToggleUI(UIType.SongPlayList);
        InputManager.OnMouseMove -= MouseMove;
        InputManager.OnMouseMove += MouseMove;
        InputManager.OnMouseLeftButton -= MouseLeft;
        InputManager.OnMouseLeftButton += MouseLeft;
        UIManager.OnPopup -= Popup;
        UIManager.OnPopup += Popup;
    }

    

    public override void Unregistration(UIManager manager)
    {
        base.Unregistration(manager);
        InputManager.OnMouseMove -= MouseMove;
        UIManager.OnPopup -= Popup;
    }

    

    protected override GameObject OnSetChild(GameObject newChild)
    {

        UIManager.ClaimSetUI(newChild);
        if (newChild)
        {
            UI_DraggableWindow asDraggable = newChild.GetComponentInChildren<UI_DraggableWindow>();
            if (asDraggable)
            {
                asDraggable.OnDragStart -= SetDragTarget;
                asDraggable.OnDragStart += SetDragTarget;
            }
        }
        return base.OnSetChild(newChild);
    }

    protected override void OnUnsetChild(GameObject oldChild)
    {
        UIManager.ClaimUnsetUI(oldChild);
        if (oldChild)
        {
            UI_DraggableWindow asDraggable = oldChild.GetComponentInChildren<UI_DraggableWindow>();
            if (asDraggable)
            {
                asDraggable.OnDragStart -= SetDragTarget;
            }
        }
        base.OnUnsetChild(oldChild);
    }

    void SetDragTarget(UI_DraggableWindow dragTarget, Vector2 startPosition)
    {
        currentDragTarget = dragTarget;
        if (currentDragTarget)
        {
            currentDragTarget.SetMouseStartPosition(startPosition);
 
        }
    }
    void MouseLeft(bool value, Vector2 screenPosition, Vector3 worldPosition)
    {
        if (!value) currentDragTarget = null;
    }

    void MouseMove(Vector2 screenPosition, Vector3 worldPosition)
    {
        if (currentDragTarget)
        {
            currentDragTarget.SetMouseCurrentPosition(screenPosition);
        }
    }

    void Popup(string title, string context, string confirm)
    {
        GameObject newChild= SetChild(ObjectManager.CreateObject("Popup"));
        if (newChild)
        {
            newChild.transform.localPosition = GetNextPopupPosition();

            if (newChild.TryGetComponent(out UIBase newUI))
            {
                if (!popupList.Contains(newUI)) popupList.Add(newUI);
                
            }
            if (newChild.TryGetComponent(out ISystemMessagePossible target))
            {
                target.SetSystemMessage(title, context, confirm);
            }
            if (newChild.TryGetComponent(out IConfirmable confirmTarget))
            {
                confirmTarget.SetConfirmAction(()=>
                {
                    if(newUI) popupList.Remove(newUI);
                    UnsetChild(newChild);
                    ObjectManager.DestroyObject(newChild);
                
                });

            }

            
        }
        
    }

    public Vector3 GetNextPopupPosition()
    { 
            Vector3 bestScore = Vector3.zero;

        if (popupList.Count == 0) return bestScore;
        
        foreach (UIBase currentPopup in popupList)
            {
                Vector3 currentScore = currentPopup.transform.localPosition;

                if(bestScore.x < currentScore.x) bestScore.x = currentScore.x;
                if(bestScore.y> currentScore.y) bestScore.y = currentScore.y;
            }

        return bestScore + popupShift;
        
    }


    
}
