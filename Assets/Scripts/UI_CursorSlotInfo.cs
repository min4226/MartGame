using System;
using UnityEngine;

public class UI_CursorSlotInfo : UI_ItemSlotInfo
{
    public override void Registration(UIManager manager)
    {
        base.Registration(manager);
        ConnectSlot(Inventory.cursorSlot);
        InputManager.OnMouseMove -= MoveToMouse;
        InputManager.OnMouseMove += MoveToMouse;
        InputManager.OnMouseLeftButton -= LeftButton;
        InputManager.OnMouseLeftButton += LeftButton;

    }

    private void LeftButton(bool value, Vector2 screenPosition, Vector3 worldPosition)
    {
        if (!value) return;
        GameObject currentHover = InputManager.CursorSelectObject;
        if (!currentHover) return;
        
        if (currentHover.TryGetComponent(out UI_ItemSlotInfo currentSlotInfo))
        {
            ConnectedSlot?.LeftClick(currentSlotInfo.ConnectedSlot);
            
        }
        
    }

    private void MoveToMouse(Vector2 screenPosition, Vector3 worldPosition)
    {
        transform.position = screenPosition;
    }

    public override void Unregistration(UIManager manager)
    {
        base.Unregistration(manager);
        DisconnectSlot();
        InputManager.OnMouseMove -= MoveToMouse;
        InputManager.OnMouseLeftButton -= LeftButton;

    }
}
