using UnityEngine;

public class PailWater : MonoBehaviour
{
    [SerializeField] ParticleSystem waterParticle;
    [SerializeField] Transform waterTransform;

    public void PourWater()
    {
        GameObject obj = Instantiate(waterParticle.gameObject);

        obj.transform.position = waterTransform.position;
        obj.transform.rotation = waterTransform.rotation;

        ParticleSystem particle = obj.GetComponent<ParticleSystem>();
        particle.Play();
    }
}