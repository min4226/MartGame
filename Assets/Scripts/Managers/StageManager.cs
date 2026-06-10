using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class StageManager : ManagerBase
{
    CustomerSpawn customerSpawn;
    public StageContainer container;
    private int currentIndex;
    StageData currentStage;

    

    public void StartStage(int index)
    {

        if (container == null)
        {
            Debug.LogError("StageContainer not initialized!");
            return;
        }

        currentIndex = index;
        currentStage = container.stageDatas[index];
    }


    protected override IEnumerator OnConnected(GameManager newManager)
    {
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
