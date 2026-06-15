using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class StageManager : ManagerBase
{
    [SerializeField] CustomerSpawn customerSpawn;
    [SerializeField] StageContainer container;
    private int currentIndex;
    StageData currentStage;

    

    public void StartStage(int index)
    {
        Debug.Log(customerSpawn == null);
        if (container == null)
        {
            Debug.LogError("StageContainer not initialized!");
            return;
        }

        currentIndex = index;
        currentStage = container.stageDatas[index];
        customerSpawn.Init(currentStage);
        
        

    }


    protected override IEnumerator OnConnected(GameManager newManager)
    {
        customerSpawn = FindFirstObjectByType<CustomerSpawn>();
        StartStage(currentIndex);

        yield break;
    }

    protected override void OnDisconnected()
    {
       
    }

    public static StageData GetcurrentStage()
    {
        return GameManager.Instance?.Stage.currentStage;
    }
    

    
}
