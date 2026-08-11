using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ClickManager : MonoBehaviour
{
    private RectTransform dragItemRect;
    // 매니저에 넣지 말것.
    private void Awake()
    {
        dragItemRect = GetComponent<RectTransform>();
    }
    private void MoveToMouse(Vector2 screenPosition, Vector3 worldPosition)
    {
        
        dragItemRect.position = screenPosition; 
    }

    private void OnEnable()
    {
        InputManager.OnMouseMove -= MoveToMouse;
        InputManager.OnMouseMove += MoveToMouse;
        InputManager.OnMouseLeftButton -= LeftButton;
        InputManager.OnMouseLeftButton += LeftButton;
    }

    private void OnDisable()
    {
        InputManager.OnMouseMove -= MoveToMouse;
    }


    private void LeftButton(bool value, Vector2 screenPosition, Vector3 worldPosition)
    {
        if (!value) return;

        GameObject currentHover = InputManager.CursorSelectObject;
        if (!currentHover) return;

        
    }
}

