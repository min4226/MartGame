using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueData
{
    public Sprite balloonSprite;
    public string dialogue;
}


[CreateAssetMenu(fileName = "Customer", menuName = "Scriptable Objects/CustomerData")]
public class CustomerData : ScriptableObject
{
    
    public CustomerType customerType;

    public AgeType ageType;
   
    
    public Reward successReward;
    public Reward failedReward;

    
    public GameObject ageSprite;

    public int troubleCustomerHealth;

    // 말풍선
    public List<DialogueData> dialogues;
    // 진상 행동들
    public List<TroubleActionData> troubleActions;
}
