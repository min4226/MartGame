using UnityEngine;




[CreateAssetMenu(fileName = "Stage", menuName = "Scriptable Objects/StageData")]

public class StageData : ScriptableObject 
{
    
    // 시간 제한
    public float timeLimit;

    // 스테이지를 클리어 하기 위해 필요한 돈과 명성
    public int requiredCoin;
    public int requiredFame;

    // 스테이지 이름
    public StageType stageName;

    // 스테이지 별마다 나오는 손님 빈도수
    public int normalCustomerCount; // 일반 손님
    public int troublemakerCustomerCount; // 진상 손님
    public int thiefCustomerCount; // 도둑 손님
    public int specialCustomerCount; // 특별 손님

    
}
