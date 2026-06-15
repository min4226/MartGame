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

    public int normalCustomerItemCount; // 일반 손님이 생성되었을 때 나올 아이템의 개수

    public ItemCreatePattern[] itemCreatePatterns; // 아이템이 나오는 패턴
    public PatternRules[] patternRules; // 패턴 규칙
}
