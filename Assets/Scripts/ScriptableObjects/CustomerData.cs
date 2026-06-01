using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Customer", menuName = "Scriptable Objects/CustomerData")]
public class CustomerData : ScriptableObject
{
    // 손님 유형
    public CustomerType customerType;

    public AgeType ageType;
   
    //실패 시 받는 리워드
    public Reward successReward;
    public Reward failedReward;

    // 손님 스프라이트
    public GameObject ageSprite;

    

}
