using Unity.VisualScripting;
using UnityEngine;

public class ThiefManager : MonoBehaviour
{
    [SerializeField] private GameObject thiefPrefab;

    // 도둑이 나오는 위치
    [SerializeField] private Transform spawnPoint;

    // 도둑이 이동할 목적지
    [SerializeField] private Transform targetPoint;

    // 도둑이 나가는 위치
    [SerializeField] private Transform exitPoint;

    private GameObject currentThief;

    public void StartThief()
    {
        if (currentThief != null)
            return;

        GameObject background = GameObject.Find("BackGround");

        if (background == null)
            return;

        spawnPoint = background.transform.Find("ThiefSpawn");
        targetPoint = background.transform.Find("ThiefTarget");
        exitPoint = background.transform.Find("ThiefExit");

        currentThief = Instantiate(
            thiefPrefab,
            spawnPoint.position,
            Quaternion.identity
        );

        Thief thief = currentThief.GetComponent<Thief>();

        if (thief != null)
        {
            thief.Init(targetPoint, exitPoint);
        }
    }

    public void ThiefFinished()
    {
        currentThief = null;
    }
}