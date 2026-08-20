using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class ClickManager : MonoBehaviour
{
    private RectTransform dragItemRect;

    [SerializeField] ParticleSystem waterParticle;
    [SerializeField] Transform waterTransform;


    public ExplusionInstance explusion;

    

    private ExpulsionItem currentItem;

    
    private GameObject currentTroubleCustomer;
    public GameObject CurrentTroubleCustomer
    {
        get { return currentTroubleCustomer; }
    }

    private Vector3 currentHitPosition;


    
        
    private void Awake()
    {
        dragItemRect = GetComponent<RectTransform>();

        ClickManager[] managers =
            FindObjectsByType<ClickManager>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None
            );

       
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
                Debug.Log(
      "할당한 ClickManager ID : " + GetInstanceID() +
      " / Customer : " + currentTroubleCustomer.name
  );
                break;
            }
        }

        for (int i = 0; i < currentItem.OnRelease.GetPersistentEventCount(); i++)
        {
            Object target = currentItem.OnRelease.GetPersistentTarget(i);
            string method = currentItem.OnRelease.GetPersistentMethodName(i);
        }
        Debug.Log(
    "OnRelease 직전 ClickManager ID : " + GetInstanceID() +
    " / Customer : " + currentTroubleCustomer
);
        currentItem.OnRelease?.Invoke();

        
        HitItem();
        
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

    public void PourWater()
    {
        Debug.Log("Pour 시작");

        GameObject obj = Instantiate(waterParticle.gameObject);

        Debug.Log("생성 성공 : " + obj.name);

        obj.transform.position = waterTransform.position;
        obj.transform.rotation = waterTransform.rotation;

        ParticleSystem particle = obj.GetComponent<ParticleSystem>();
        particle.Play();
    }
    private void OnParticleCollision(GameObject other)
    {
        TroubleCustomerDamage damage =
            other.GetComponentInParent<TroubleCustomerDamage>();

        if (damage != null)
        {
            damage.TakeDamage(currentItem.ExpulsionDamage, currentHitPosition);
        }
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