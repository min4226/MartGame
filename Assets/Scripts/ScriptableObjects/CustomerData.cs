using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Customer", menuName = "Scriptable Objects/CustomerData")]
public class CustomerData : ScriptableObject
{
    
    public CustomerType customerType;

    public AgeType ageType;
   
    
    public Reward successReward;
    public Reward failedReward;

    
    public GameObject ageSprite;

    

}
