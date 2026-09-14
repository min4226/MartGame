using System.Collections;
using UnityEngine;

public class TroubleCustomerDamage : MonoBehaviour
{

    [SerializeField]CustomerData customerData;
    [SerializeField] private GameObject cart;
    ClickManager clickManager;
    private int currentHP;
    private Vector3 originalPosition;

    private void Awake()
    {
        currentHP = customerData.troubleCustomerHealth;   
        originalPosition = transform.position;
        clickManager = GameObject.FindFirstObjectByType<ClickManager>();
    }

    public void TakeDamage(int damage, Vector3 hitPosition, bool isCartAttack = false)
    {
        currentHP -= damage;

        if (currentHP < 0)
            currentHP = 0;

        StartCoroutine(HitReaction(hitPosition, isCartAttack));
    }

    private IEnumerator HitReaction(Vector3 hitPosition, bool isCartAttack)
    {
        Vector3 direction = (transform.position - hitPosition).normalized;

        // 맞았을 때 잠깐 밀려남
        transform.position += direction * 0.3f;

        yield return new WaitForSeconds(0.1f);

        // 카트 공격이 아닐 때만 원래 위치로 복귀
        if (!isCartAttack)
        {
            transform.position = originalPosition;
        }

        if (currentHP <= 0)
        {
            if (cart != null)
                Destroy(cart);

            gameObject.SetActive(false);

            GameManager.Instance.CustomerSpawn.StartNextCustomer();
        }
    }
}