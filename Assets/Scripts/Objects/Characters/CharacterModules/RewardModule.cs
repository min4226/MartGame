using UnityEngine;

public class RewardModule : MonoBehaviour
{
    // 퇴치한 후 보상을 받을 경우
    // 보상 얻음
    // 보상 증가 -> 제한 시간 내 처리 완료 했을 때
    // 보상 감소 -> 제한 시간 내 처리 못 했을 경우
    int _fame;
    public int Fame => _fame;

    int _coin;
    public int Coin => _coin;

    public int ReceiveReward(int Fame, int Coin)
    {
        
    }

    public  IncreaseReward(int Fame, int Coin);
    public  DecreaseReward(int Fame, int Coin);
}
