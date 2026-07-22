using TMPro;
using UnityEngine.SceneManagement;
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
    GameObject coinPanel;
    GameObject famePanel;
    
    
    //스테이지2에서 리워드가 안 올라감
    public void ApplyReward()
    {
        /*StageData stage = GameManager.Instance.Stage.CurrentStage;
        Debug.Log($"currentstage : {GameManager.Instance.Stage.CurrentStage}");
        Debug.Log($"stagedata : {stage == null}");*/
        // reward = new Reward();
        _fame += GameManager.Instance.CustomerData.successReward.fame;
        _coin += GameManager.Instance.CustomerData.successReward.coin;

        _fame = Mathf.Max(0, _fame);
        _coin = Mathf.Max(0, _coin);

        coinPanel = GameObject.Find("MyCoinCount");
        famePanel = GameObject.Find("MyFameCount");

        coinPanel.GetComponentInChildren<TMP_Text>().text = _coin.ToString();
        famePanel.GetComponentInChildren<TMP_Text>().text = _fame.ToString();
        

        Debug.Log($"_fame, _coin : {_fame}, {_coin}");
    }

    // 실패했을 때 따로 보상을 감소시키지 않음

    // 스테이지를 성공 했을 때 받는 보상 정도
    /*public TimeResult ReceiveReward(TimeResult result, StageData stageData)
    {
        if (result == TimeResult.Success)
        {
            _fame += stageData.fameReward;
            _coin += stageData.coinReward;
        }
        return result;
    }

    // 진상을 처리했을 때 받는 보상
    public void IncreaseReward(StageData stageData)
    {
        _fame += stageData.fameIncrease;
        _coin += stageData.coinIncrease;
    }
    // 진상 처리를 못 했을 시
    public void DecreaseReward(StageData stageData)
    {
        _fame -= stageData.fameDecrease;
        _coin -= stageData.coinDecrease;

        _fame = Mathf.Max(0, _fame);
        _coin = Mathf.Max(0, _coin);
    }*/
}
