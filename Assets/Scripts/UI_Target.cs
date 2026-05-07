using UnityEngine;

public class UI_Target : OpenbleUIBase
{
    [SerializeField] Vector2 shiftedPosition;
    [SerializeField] TMPro.TextMeshProUGUI nameText;

    public override void Registration(UIManager manager)
    {
        base.Registration(manager);
        InputManager.OnMouseHover -= HoverInfoChange;
        InputManager.OnMouseHover += HoverInfoChange;

        InputManager.OnMouseMove -= MoveToMouse;
        InputManager.OnMouseMove += MoveToMouse;
    }

    void MoveToMouse(Vector2 screenPosition, Vector3 worldPosition)
    {
        transform.position = screenPosition + shiftedPosition;
    }

    public override void Unregistration(UIManager manager)
    {
        base.Unregistration(manager);
        InputManager.OnMouseHover -= HoverInfoChange;
    }

    void HoverInfoChange(GameObject newTarget, GameObject oldTarget)
    {

        if (newTarget && newTarget.name == "MyFameCount") Open();
        else Close();
    }
}
