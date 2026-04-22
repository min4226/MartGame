using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public delegate void MouseButtonEvent(bool value, Vector2 screenPosition, Vector3 worldPosition);
public delegate void MouseMoveEvent(Vector2 screenPosition, Vector3 worldPosition);
public delegate void ExterminItemEvent(bool value);
public delegate void AxisEvent(Vector2 value);
public delegate void ButtonEvent(bool value);

[RequireComponent(typeof(PlayerInput))]

public class InputManager : ManagerBase
{
    public static event MouseButtonEvent OnMouseLeftButton;
    public static event MouseButtonEvent OnMouseRightButton;
    public static event MouseMoveEvent OnMouseMove;
    public static event ExterminItemEvent OnExterminItemLeft;
    public static event ExterminItemEvent OnExterminItemMiddle;
    public static event ExterminItemEvent OnExterminItemRight;
    public static event AxisEvent OnMove;
    public static event ButtonEvent OnCancel;

    PlayerInput targetInput;
    Dictionary<string, InputAction> actionDictionary = new();
    List<RaycastResult> cursorHitList = new();

    Vector2 cursorScreenPosition;
    Vector3 cursorWorldPosition;

    public bool is2D = true;

    

    protected override IEnumerator OnConnected(GameManager newManager)
    {


        // 키입력을 변경하기 위해
        targetInput = GetComponent<PlayerInput>();
        LoadAllActions();
        InitializeAllActions();

        GameManager.OnUpdateManager -= UpdateEvent;
        GameManager.OnUpdateManager += UpdateEvent;
        yield return null;
    }

    protected override void OnDisconnected()
    {
        GameManager.OnUpdateManager -= UpdateEvent;
    }

    public void UpdateEvent(float deltaTime)
    {
        RefreshGameObjectUnderCursor();
    }

    void RefreshGameObjectUnderCursor()
    {
        cursorHitList.Clear();
        if (is2D)
        {
            GameManager.Instance.Camera.GetRaycastResult2D(cursorScreenPosition, cursorHitList);
        }
        else
        {
            GameManager.Instance.Camera.GetRaycastResult3D(cursorScreenPosition, cursorHitList);
        }
    }

    public GameObject GetGameObjectUnderCursor()
    {
        if (cursorHitList.Count == 0) return null;

        return cursorHitList[0].gameObject;
    }

    void LoadAllActions()
    {
        foreach (var currentAction in targetInput.actions)
        {
            actionDictionary.TryAdd(currentAction.name, currentAction);

        }
    }

    void InitializeAllActions()
    {
        if (actionDictionary == null || actionDictionary.Count == 0) return;

        InitializeAction("CursorPositionChanged", CursorPositionChanged);

        InitializeAction("MouseLeftButton", (context) => OnMouseLeftButton?.Invoke(true, cursorScreenPosition, cursorWorldPosition)
                                          , (context) => OnMouseLeftButton?.Invoke(false, cursorScreenPosition, cursorWorldPosition));

        InitializeAction("MouseRightButton", (context) => OnMouseRightButton?.Invoke(true, cursorScreenPosition, cursorWorldPosition)
                                           , (context) => OnMouseRightButton?.Invoke(false, cursorScreenPosition, cursorWorldPosition));

        InitializeAction("Cancel", (context) => OnCancel?.Invoke(true));
        InitializeAction("ExterminItemLeft", (context) => OnExterminItemLeft?.Invoke(true));
        InitializeAction("ExterminItemMiddle", (context) => OnExterminItemMiddle?.Invoke(true));
        InitializeAction("ExterminItemRight", (context) => OnExterminItemRight?.Invoke(true));
        InitializeAction("Move", (context) => OnMove?.Invoke(GetVector2Value(context))
                               , (context) => OnMove?.Invoke(Vector2.zero));

        void InitializeAction(string actionName, Action<InputAction.CallbackContext> actionMethod, Action<InputAction.CallbackContext> cancelMethod = null)
        {
            if (actionDictionary == null) return;
            if (actionDictionary.TryGetValue(actionName, out InputAction currentInput))
            {
                if (actionMethod is not null) currentInput.performed += actionMethod;
                if (cancelMethod is not null) currentInput.canceled += cancelMethod;
            }
        }

        Vector2 GetVector2Value(InputAction.CallbackContext context)
        {
            if (context.valueType != typeof(Vector2)) return Vector2.zero;
            return context.ReadValue<Vector2>();
        }

        void CursorPositionChanged(InputAction.CallbackContext context)
        {

            Vector2 screenPosition = context.ReadValue<Vector2>();
            Vector3 worldPosition;

            if (is2D)
            {
                worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
                worldPosition.z = 0;
            }
            else
            {
                worldPosition = Vector3.zero;
            }

            cursorScreenPosition = screenPosition;
            cursorWorldPosition = worldPosition;
            OnMouseMove?.Invoke(screenPosition, worldPosition);


        }





    }
}
