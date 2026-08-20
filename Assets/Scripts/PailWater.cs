using UnityEngine;

public class PailWater : MonoBehaviour
{
    [SerializeField] ParticleSystem waterParticle;
    [SerializeField] Transform waterTransform;

    public void PourWater()
    {
        Debug.Log("Pour 시작");

        GameObject obj = Instantiate(waterParticle.gameObject);

        Debug.Log("생성 성공 : " + obj.name);

        obj.transform.position = waterTransform.position;
        obj.transform.rotation = waterTransform.rotation;

        ParticleSystem particle = obj.GetComponent<ParticleSystem>();
        particle.Play();
    }
}