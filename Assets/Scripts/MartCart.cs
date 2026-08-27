using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

public class MartCart : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private int damage = 30;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        StartCoroutine(MoveCart());
    }

    private IEnumerator MoveCart()
    {
        while (true)
        {
            rb.MovePosition(
     rb.position + Vector3.left * speed * Time.deltaTime);
 

             yield return new WaitForFixedUpdate();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        TroubleCustomerDamage customerDamage =
       collision.gameObject.GetComponentInParent<TroubleCustomerDamage>();
        
        

        Debug.Log("🛒 카트가 진상에게 충돌!");

        StartCoroutine(CartDamage(customerDamage));
    }

    private IEnumerator CartDamage(TroubleCustomerDamage customerDamage)
    {
        yield return new WaitForSeconds(15f);

        customerDamage.TakeDamage(100, transform.position);

        Destroy(this);
    }
}