using UnityEngine;
using static UnityEngine.ParticleSystem;

public class PailWater : MonoBehaviour
{

    [SerializeField] ParticleSystem waterParticle;
    [SerializeField] Transform waterTransform;
    ParticleSystem waterParticleCopy;
    private void Awake()
    {
        
    }

    public void PourWater()
    {
        Debug.Log("물 뿌리기 실행");
        Debug.Log("Water 함수 들어옴");
        Debug.Log($"water : {waterParticleCopy}");
        Debug.Log($"particle : {waterParticle}");
        waterParticleCopy = waterParticle;
        waterParticleCopy.transform.position = waterParticle.transform.position;
        waterParticleCopy.Play();
        Debug.Log($"isPlaying = {waterParticleCopy.isPlaying}");
        Debug.Log($"particleCount = {waterParticleCopy.particleCount}"); // 파티클이 0개
    }

}