using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RewardModule : MonoBehaviour
{
    int _fame;
    public int Fame => _fame;

    int _coin;
    public int Coin => _coin;
    GameObject coinPanel;
    GameObject famePanel;

    // FireBase에 코인, 경험치 추가
    public void SetRewardData(int coin, int fame)
    { 
        _coin = coin;
        _fame = fame;

        Debug.Log($"Firebase 불러온 보상 데이터 - coin: {_coin}, fame: {_fame}");
    }

    public void ApplyReward()
    {
        _fame += GameManager.Instance.CustomerData.successReward.fame;
        _coin += GameManager.Instance.CustomerData.successReward.coin;

        _fame = Mathf.Max(0, _fame);
        _coin = Mathf.Max(0, _coin);

        coinPanel = GameObject.Find("MyCoinCount");
        famePanel = GameObject.Find("MyFameCount");

        coinPanel.GetComponentInChildren<TMP_Text>().text = _coin.ToString();
        famePanel.GetComponentInChildren<TMP_Text>().text = _fame.ToString();

        // Firebase에 저장
        GameManager.DB.SaveRewardData(_coin, _fame);

        Debug.Log($"_fame, _coin : {_fame}, {_coin}");
        Debug.Log($"_fame, _coin : {_fame}, {_coin}");
    }

}
