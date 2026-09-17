using UnityEngine;

public class MouseFollower : MonoBehaviour, IFunctionable
{
    
    void Start()
    {
        RegistrationFunctions();
    }

    void OnDestroy()
    {
        UnregistrationFunctions();
    }
    public void RegistrationFunctions()
    {
        
        
        InputManager.OnExterminItemLeft += (value) => UIManager.ClaimPopup("ù ��° ��ġ ����", "�� ���� ���� �� �� ���ʿ� �ִ� ��ġ ����", "1");
    }
    public void UnregistrationFunctions()
    {
        InputManager.OnMouseLeftButton -= CreateToMouse;
        InputManager.OnMouseRightButton -= DestroyOnMouse;
    }

    void MoveToMouse(Vector2 screenPosition, Vector3 worldPosition)
    { 
        transform.position = worldPosition;
    }



    void CreateToMouse(bool value,  Vector2 screenPosition, Vector3 worldPosition)
    {
        GameObject inst = ObjectManager.CreateObject("Character1",worldPosition);
        
    }


    void DestroyOnMouse(bool value, Vector2 screenPosition, Vector3 worldPosition)
    {
        ObjectManager.DestroyObject(GameManager.Instance.Input.GetGameObjectUnderCursor());
    }
}
