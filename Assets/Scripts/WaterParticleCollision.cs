using System.Collections.Generic;
using UnityEngine;

public class WaterParticleCollision : MonoBehaviour
{
    [SerializeField] private int damage = 30;

    private ParticleSystem particleSystem;
    private List<ParticleSystem.Particle> triggerParticles =
        new List<ParticleSystem.Particle>();

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

            Collider2D[] hits =
                Physics2D.OverlapPointAll(hitPosition);

            foreach (Collider2D hit in hits)
            {
                TroubleCustomerDamage damageTarget =
                    hit.GetComponentInParent<TroubleCustomerDamage>();

                if (damageTarget != null)
                {
                    Debug.Log("물 파티클이 손님에게 닿음");

                    damageTarget.TakeDamage(
                        damage,
                        hitPosition
                    );

                    break;
                }
            }
        }
    }
}