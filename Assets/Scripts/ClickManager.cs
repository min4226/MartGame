using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class ClickManager : MonoBehaviour
{
    private RectTransform dragItemRect;

    
    public ParticleSystem water;

    
    public ExplusionInstance explusion;

    
    private ExpulsionItem currentItem;

    
    private GameObject currentTroubleCustomer;

    
    private Vector3 currentHitPosition;


    
        
        private void Awake()
    {
        dragItemRect = GetComponent<RectTransform>();

        ClickManager[] managers =
            FindObjectsByType<ClickManager>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None
            );

        Debug.Log($"===== ClickManager 개수 : {managers.Length} =====");

        foreach (ClickManager manager in managers)
        {
            Debug.Log(
                $"ClickManager 발견 | " +
                $"GameObject = {manager.gameObject.name} | " +
                $"ID = {manager.GetInstanceID()} | " +
                $"Scene = {manager.gameObject.scene.name}"
            );
        }
    }



    private void MoveToMouse(Vector2 screenPosition, Vector3 worldPosition)
    {
        if (dragItemRect != null)
        {
            dragItemRect.position = screenPosition;
        }
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


    private void LeftButton(
    bool value,
    Vector2 screenPosition,
    Vector3 worldPosition)
    {
        Debug.Log(
            $"[LeftButton] value = {value} / " +
            $"frame = {Time.frameCount}"
        );

        // true = 마우스 누름
        // false = 마우스 뗌
        if (value)
            return;

        Debug.Log("마우스를 뗌");

        OnMouseRelease(worldPosition);
    }


    private void OnMouseRelease(Vector3 worldPosition)
    {

        Debug.Log($"===== OnMouseRelease 실행 ===== {Time.frameCount}");
        currentItem = explusion.GetSelectedItem();
        Debug.Log($"현재 실행 오브젝트 : {gameObject.name}");
        Debug.Log($"현재 ID : {GetInstanceID()}");
        Debug.Log($"Scene : {gameObject.scene.name}");
        if (currentItem == null)
        {
            Debug.Log("currentItem이 없음");
            return;
        }


        
        currentHitPosition = worldPosition;
        currentHitPosition.z = 0f;


        

        currentTroubleCustomer = null;


        

        Collider2D[] hits = Physics2D.OverlapPointAll(currentHitPosition);

        Debug.Log($"겹친 Collider 개수 : {hits.Length}");


        foreach (Collider2D col in hits)
        {
            Debug.Log($"확인 중 : {col.name}");


            TroubleCustomerDamage damage =
                col.GetComponentInParent<TroubleCustomerDamage>();


            if (damage != null)
            {
                currentTroubleCustomer = damage.gameObject;
                
                Debug.Log(
                    $"진상 손님 맞음 : " +
                    $"{currentTroubleCustomer.name}"
                );

                break;
            }
        }




        Debug.Log($"현재 ClickManager ID = {GetInstanceID()}");

        for (int i = 0; i < currentItem.OnRelease.GetPersistentEventCount(); i++)
        {
            Object target = currentItem.OnRelease.GetPersistentTarget(i);
            string method = currentItem.OnRelease.GetPersistentMethodName(i);

            Debug.Log(
                $"OnRelease[{i}] " +
                $"Target = {target} / " +
                $"TargetID = {target?.GetInstanceID()} / " +
                $"Method = {method}"
            );
        }
        currentItem.OnRelease?.Invoke();
        Debug.Log("onrelease 실행 후");
        HitItem();
        Debug.Log("[OnMouseRelease] OnRelease 실행 완료");
    }




    public void HitItem()
    {
        

        if (currentItem == null)
        {
            Debug.Log("❌ currentItem NULL");
            return;
        }

        if (currentTroubleCustomer == null)
        {
            Debug.Log("❌ currentTroubleCustomer NULL");
            return;
        }

        Debug.Log($"아이템 데미지 : {currentItem.ExpulsionDamage}");

        Transform customer = currentTroubleCustomer.transform;

        TroubleCustomerDamage damage =
            currentTroubleCustomer.GetComponent<TroubleCustomerDamage>();

        Debug.Log($"TroubleCustomerDamage : {damage}");

        if (damage != null)
        {
            Debug.Log("🔥 TakeDamage 호출 직전!");

            damage.TakeDamage(
                currentItem.ExpulsionDamage,
                currentHitPosition
            );

            Debug.Log("🔥 TakeDamage 호출 완료!");
        }
        else
        {
            Debug.Log("❌ TroubleCustomerDamage 컴포넌트를 못 찾음");
        }

        Vector3 direction = (customer.position - currentHitPosition).normalized;

        StartCoroutine(Knockback(customer, direction));
    }

    private IEnumerator Knockback(Transform customer, Vector3 direction)
    {
        Vector3 start = customer.position;
        Vector3 target = start + direction * 0.5f;

        float time = 0f;
        float duration = 0.15f;

        while (time < duration)
        {
            time += Time.deltaTime;
            customer.position = Vector3.Lerp(start, target, time / duration);
            yield return null;
        }

        customer.position = target;
    }

    
    public void PourWater()
    {
        Debug.Log("💧 물 파티클 실행");

        if (water != null)
        {
            water.Play();
        }
        else
        {
            Debug.Log("❌ water가 연결되지 않음");
        }
    }
}