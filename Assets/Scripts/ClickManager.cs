using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class ClickManager : MonoBehaviour
{
    private RectTransform dragItemRect;

    
    

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

        /*foreach (ClickManager manager in managers)
        {
            Debug.Log(
                $"ClickManager 발견 | " +
                $"GameObject = {manager.gameObject.name} | " +
                $"ID = {manager.GetInstanceID()} | " +
                $"Scene = {manager.gameObject.scene.name}"
            );
        }*/
        
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
        
        if (value)
            return;

        OnMouseRelease(worldPosition);
    }


    private void OnMouseRelease(Vector3 worldPosition)
    {

        
        currentItem = explusion.GetSelectedItem();
        
        if (currentItem == null)
        {
            return;
        }


        
        currentHitPosition = worldPosition;
        currentHitPosition.z = 0f;


        

        currentTroubleCustomer = null;


        

        Collider2D[] hits = Physics2D.OverlapPointAll(currentHitPosition);



        foreach (Collider2D col in hits)
        {
            TroubleCustomerDamage damage = col.GetComponentInParent<TroubleCustomerDamage>();


            if (damage != null)
            {
                currentTroubleCustomer = damage.gameObject;
               
                break;
            }
        }

        for (int i = 0; i < currentItem.OnRelease.GetPersistentEventCount(); i++)
        {
            Object target = currentItem.OnRelease.GetPersistentTarget(i);
            string method = currentItem.OnRelease.GetPersistentMethodName(i);
        }
        currentItem.OnRelease?.Invoke();
        HitItem();
        if (currentItem.ExpulsionItemName == "양동이")
        {
            Collider2D[] itemHits =
                Physics2D.OverlapPointAll(currentHitPosition);

            foreach (Collider2D col in itemHits)
            {
                PailWater pailWater =
                    col.GetComponentInParent<PailWater>();

                if (pailWater != null)
                {
                    pailWater.PourWater();
                    break;
                }
            }
        }
    }




    public void HitItem()
    {
        

        if (currentItem == null)
        {
            return;
        }

        if (currentTroubleCustomer == null)
        {
            return;
        }

        Transform customer = currentTroubleCustomer.transform;

        TroubleCustomerDamage damage = currentTroubleCustomer.GetComponent<TroubleCustomerDamage>();

        if (damage != null)
        {

            damage.TakeDamage(
                currentItem.ExpulsionDamage,
                currentHitPosition
            );

           
        }
        
        Vector3 direction = (customer.position - currentHitPosition).normalized;

        StartCoroutine(Knockback(customer, direction));
    }

    private IEnumerator Knockback(Transform customer, Vector3 direction)
    {
        // 때리기 전 원래 위치
        Vector3 start = customer.position;

        // 맞고 튕겨나갈 위치
        Vector3 target = start + direction * 0.5f;

        float time = 0f;
        float duration = 0.15f;

        // 1. 때린 방향으로 이동
        while (time < duration)
        {
            time += Time.deltaTime;
            customer.position = Vector3.Lerp(start, target, time / duration);
            yield return null;
        }

        // 정확하게 target 위치에 놓기
        customer.position = target;

        // 시간 초기화
        time = 0f;

        // 2. 다시 원래 위치로 돌아오기
        while (time < duration)
        {
            time += Time.deltaTime;
            customer.position = Vector3.Lerp(target, start, time / duration);
            yield return null;
        }

        // 정확하게 원래 위치에 놓기
        customer.position = start;
    }


    
}