using UnityEngine;

public class MouseFollower : MonoBehaviour, IFunctionable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        
        
        InputManager.OnExterminItemLeft += (value) => UIManager.ClaimPopup("첫 번째 퇴치 물건", "세 개의 물건 중 맨 왼쪽에 있는 퇴치 물건", "1");
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
