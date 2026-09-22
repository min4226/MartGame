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
            rb.MovePosition(rb.position + Vector3.left * speed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        TroubleCustomerDamage customerDamage =
            collision.gameObject.GetComponentInParent<TroubleCustomerDamage>();

        Rigidbody customerRb =
            collision.gameObject.GetComponentInParent<Rigidbody>();

       
        if (customerRb != null)
        {
            customerRb.AddForce(Vector3.left * 5f, ForceMode.Impulse);
        }

        if (customerDamage != null)
        {
            StartCoroutine(CartDamage(customerDamage));
        }
    }

    private IEnumerator CartDamage(TroubleCustomerDamage customerDamage)
    {
        yield return new WaitForSeconds(3f);

        customerDamage.TakeDamage(100, transform.position); 

        Destroy(gameObject);
    }
}