using System.Collections;
using UnityEngine;

public class TroubleCustomerDamage : MonoBehaviour
{

    [SerializeField]CustomerData customerData;

    private int currentHP;
    private Vector3 originalPosition;

    private void Awake()
    {
        currentHP = customerData.troubleCustomerHealth;   
        originalPosition = transform.position;
        
    }

    public void TakeDamage(int damage, Vector3 hitPosition)
    {
        Debug.Log($"피격 전 HP: {currentHP}");
        Debug.Log($"받은 데미지: {damage}");

        currentHP -= damage;

        if (currentHP < 0)
            currentHP = 0;

        Debug.Log($"피격 후 HP: {currentHP}");

        StartCoroutine(HitReaction(hitPosition));

        if (currentHP <= 0)
        {
            Debug.Log("!!! HP가 0 이하가 됨 !!!");

            StartCoroutine(
                GameManager.Instance.CustomerSpawn.NextCustomerRoutine()
            );
        }
    }

    private IEnumerator HitReaction(Vector3 hitPosition)
    {
        
        Vector3 direction = (transform.position - hitPosition).normalized;

        transform.position = originalPosition + direction * 0.3f;

        yield return new WaitForSeconds(0.1f);

        
        transform.position = originalPosition;
    }
}