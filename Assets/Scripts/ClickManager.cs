using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class ClickManager : MonoBehaviour
{
    private RectTransform dragItemRect;

    [SerializeField] ParticleSystem waterParticle;
    [SerializeField] Vector3 waterTransform;
    Transform cartCreate;
    public GameObject cartPrefab;
    public ExplusionInstance explusion;
    public float speed = 2.0f;
    private ExpulsionItem currentItem;

    
    private GameObject currentTroubleCustomer;
    public GameObject CurrentTroubleCustomer
    {
        get { return currentTroubleCustomer; }
    }

    private Vector3 currentHitPosition;
  
    private void Awake()
    {
        this.gameObject.SetActive(false);
        dragItemRect = GetComponent<RectTransform>();

        ClickManager[] managers =
            FindObjectsByType<ClickManager>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None
            );
        //cartCreate = GameObject.Find("CartSpawn").transform;

        //Debug.Log($"cartcreate : {cartCreate}");
        

        cartCreate = GameObject.Find("CartSpawn").transform;

        Debug.Log($"cartCreate : {cartCreate}");
    }

    private void MoveToMouse(Vector2 screenPosition, Vector3 worldPosition)
    {
        if (dragItemRect != null)
        {
            this.gameObject.SetActive(true);
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


    private void LeftButton(bool value, Vector2 screenPosition, Vector3 worldPosition)
    {
        if (value) return;
        OnMouseRelease(worldPosition);
    }


    private void OnMouseRelease(Vector3 worldPosition)
    {
        currentItem = explusion.GetSelectedItem();

        if (currentItem == null) return;
        if (currentItem.name == "MartCart")
        {
            CartCollider();
            return;
        }
        currentHitPosition = worldPosition;
        //currentHitPosition.z = 0f;
        currentTroubleCustomer = null;

        Collider[] hits = Physics.OverlapSphere(currentHitPosition, 0.1f);

        foreach (Collider col in hits)
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

        if (currentItem.name == "Pail") return;
        

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
        GameObject obj = Instantiate(waterParticle.gameObject, waterTransform, Quaternion.identity);

        obj.transform.position = waterTransform;
        //obj.transform.rotation = waterTransform.;

        ParticleSystem particle = obj.GetComponent<ParticleSystem>();
        GameObject troubleCustomer = GameObject.FindGameObjectWithTag("TroubleCustomer");
        //particle.trigger.AddCollider(troubleCustomer.GetComponent<Collider2D>());
        Collider collider = troubleCustomer.GetComponent<Collider>();

        particle.trigger.AddCollider(collider);

        
        particle.Play();
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

    public void CartCollider()
    {
        if (cartPrefab == null)
        {
            Debug.LogError("❌ cartPrefab이 비어있음");
            return;
        }

        if (cartCreate == null)
        {
            Debug.LogError("❌ cartCreate가 비어있음");
            return;
        }

        Debug.Log($"🛒 카트 생성 시작");
        Debug.Log($"cartPrefab = {cartPrefab.name}");
        Debug.Log($"cartCreate = {cartCreate.name}");
        Debug.Log($"cartCreate.position = {cartCreate.position}");

        GameObject cart = Instantiate(
            cartPrefab,
            cartCreate.position,
            Quaternion.identity
        );

        cart.SetActive(true);
        Debug.Log($"🛒 카트 생성 완료 : {cart.name}");
        Debug.Log($"카트 위치 : {cart.transform.position}");

        StartCoroutine(MoveCart(cart));
    }

    private IEnumerator MoveCart(GameObject cart)
    {
        Debug.Log("🛒 카트 이동 시작");

        while (cart != null)
        {
            cart.transform.position += Vector3.left * speed * Time.deltaTime;

            yield return null;
        }
    }

}

    

