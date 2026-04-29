using UnityEngine;

[System.Serializable]
public class StageData 
{
    // 시간 제한
    public float timeLimit;

    // 스테이지를 클리어 하기 위해 필요한 돈과 명성
    public int requiredMoney;
    public int requiredFame;

    // 손님 등장
    public float normalGuestRate;
    public float troublemakerRate;
    public float thiefRate;
    public float specialGuestRate;

    // 진상 난이도
    public float troublemakerSpeed;
    public float troublemakerStealChance;

    // 이름 진상
    public bool allowNamedTroublemaker;
    public float namedSpawnCooldown;

    // 이벤트
    public float discountChance;
    public bool enableSpecialItems;

    // 꾸미기
    public int requiredMaterialCount;
    public string rewardDecorationId;

    // 기타
    public int stageIndex;
    public string stageName;

    // 스테이지 클리어 시 받는 보상에 대한 변수
    public int fameReward;
    public int coinReward;

    // 실패했을 시 원래 갖고 있는 값에서 감소할 경우
    public int fameDecrease;
    public int coinDecrease;

    // 성공했을 시 원래 갖고 있는 값에서 증가할 경우
    public int fameIncrease;
    public int coinIncrease;
}
