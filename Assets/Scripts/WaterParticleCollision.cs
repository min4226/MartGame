using System.Collections.Generic;
using UnityEngine;

public class WaterParticleCollision : MonoBehaviour
{
    private ParticleSystem particleSystem;
    private List<ParticleSystem.Particle> triggerParticles =
    new List<ParticleSystem.Particle>();
    private readonly Dictionary<TroubleCustomerDamage, float> lastHitTimes
    = new Dictionary<TroubleCustomerDamage, float>();

    [SerializeField] private float waterHitInterval = 0.5f;
    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }
    private void OnParticleTrigger()
    {
        int count = particleSystem.GetTriggerParticles(
            ParticleSystemTriggerEventType.Enter,
            triggerParticles
        );

        for (int i = 0; i < count; i++)
        {
            Vector3 hitPosition = triggerParticles[i].position;

            Collider[] hits =
                Physics.OverlapSphere(hitPosition, 0.1f);

            foreach (Collider hit in hits)
            {
                TroubleCustomerDamage damageTarget =
                    hit.GetComponentInParent<TroubleCustomerDamage>();

                if (damageTarget != null)
                {
                    // 해당 손님이 마지막으로 물에 맞은 시간 확인
                    if (lastHitTimes.TryGetValue(
                        damageTarget,
                        out float lastHitTime))
                    {
                        if (Time.time - lastHitTime < waterHitInterval)
                            continue;
                    }

                    // 물의 마지막 피격 시간 갱신
                    lastHitTimes[damageTarget] = Time.time;

                    Debug.Log("물 파티클이 손님에게 닿음");

                    damageTarget.TakeDamage(
                        30,
                        hitPosition
                    );

                    break;
                }
            }
        }
    }
    public void SetCustomer(GameObject customer)
    {
        Collider col = customer.GetComponentInChildren<Collider>();

        if (col != null)
        {
            particleSystem.trigger.SetCollider(0, col);
        }
    }

}