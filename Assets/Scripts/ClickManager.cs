using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

// 진상 손님일 경우 물건을 맞았을 때 takedamage(currentitem.damage)를 어떻게 줄 건지 적으면 됨
public class ClickManager : MonoBehaviour
{
    private RectTransform dragItemRect;

    public ParticleSystem water;
    private ExpulsionItem currentItem;
    public ExplusionInstance explusion;

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
        InputManager.OnMouseLeftButton -= LeftButton;
    }

    private void LeftButton(bool value, Vector2 screenPosition, Vector3 worldPosition)
    {
        if (!value) return;

        GameObject currentHover = InputManager.CursorSelectObject;

        if (!currentHover)
        {
            Debug.Log($"currenthover : {currentHover}");
            return;
        }

        Debug.Log("마우스를 뗌");

        OnMouseRelease(worldPosition);
    }

    private void OnMouseRelease(Vector3 worldPosition)
    {
        currentItem = explusion.GetSelectedItem();

        Debug.Log($"currentItem : {currentItem}");

        if (currentItem == null)
        {
            Debug.Log("currentItem NULL");
            return;
        }

        // 마우스 위치에 있는 월드 오브젝트 확인
        Vector3 checkPosition = worldPosition;
        checkPosition.z = 0f;

        Collider2D hit = Physics2D.OverlapPoint(checkPosition);

        if (hit != null)
        {
            Debug.Log("마우스 위치에 있는 오브젝트 : " + hit.name);

            if (hit.CompareTag("TroubleCustomer"))
            {
                Debug.Log("진상 손님 맞음");
                HitItem();

            }
        }

        Debug.Log("OnRelease 호출 직전");

        currentItem.OnRelease?.Invoke();

        Debug.Log("OnRelease 호출 완료");
    }

    public void PourWater()
    {
        Debug.Log("파티클 생성");
        water.Play();
    }

    public void HitItem()
    {
        Debug.Log("물건으로 때리기");
    }
}