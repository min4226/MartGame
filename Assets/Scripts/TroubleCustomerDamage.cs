using System.Collections;
using UnityEngine;

public class TroubleCustomerDamage : MonoBehaviour
{
    CustomerData customerData;

    private int currentHP;
    private Vector3 originalPosition;

    private void Awake()
    {
        currentHP = GameManager.Instance.CustomerData.troubleCustomerHealth;   
        originalPosition = transform.position;
    }

    public void TakeDamage(int damage, Vector3 hitPosition)
    {
        Debug.Log("데미지 주기");

        
        currentHP -= damage;
        Debug.Log($"진상 데미지 : {currentHP}");
        
        StartCoroutine(HitReaction(hitPosition));

        
        if (currentHP <= 0)
        {
            currentHP = 0;

            
            Debug.Log("진상 퇴치 완료!");
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