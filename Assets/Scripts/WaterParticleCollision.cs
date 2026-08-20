using UnityEngine;

public class WaterParticleCollision : MonoBehaviour
{
    private ExpulsionItem currentItem;
    private Vector3 currentHitPosition;
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("파티클과 손님 충돌");
        TroubleCustomerDamage damage =
            other.GetComponentInParent<TroubleCustomerDamage>();

        if (damage != null)
        {
            Debug.Log("손님과 충돌 중");
            damage.TakeDamage(currentItem.ExpulsionDamage, currentHitPosition);
        }
    }
}
