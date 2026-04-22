using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : ControllerBase
{
    protected override void OnPossess(CharacterBase newCharacter)
    {
        base.OnPossess(newCharacter);
        InputManager.OnMouseLeftButton -= MoveToMousePosition;
        InputManager.OnMouseLeftButton += MoveToMousePosition;
        InputManager.OnMove -= MoveToDirection;
        InputManager.OnMove += MoveToDirection;
    }

    private void MoveToDirection(Vector2 value)
    {
        CommandMoveToDirection(value);
    }

    protected override void OnUnPossess(CharacterBase oldCharacter)
    {
        base.OnUnPossess(oldCharacter);
        InputManager.OnMouseLeftButton -= MoveToMousePosition;
    }

    public void MoveToMousePosition(bool value, Vector2 screenPosition, Vector3 worldPosition)
    {
        if(value) CommandMoveToDestination(worldPosition, 0.0f);
    }
}
