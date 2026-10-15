
using System.Collections.Generic;
using UnityEngine;

public class RankingListUI : MonoBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private RankingSlot slotPrefab;

    public async void LoadRanking()
    {
        List<DBManager.RankingData> rankings =
            await GameManager.DB.ReadAllRankingData();

        if (content == null || slotPrefab == null)
        {
            Debug.LogError("랭킹 Content 또는 Slot Prefab이 연결되지 않았습니다.");
            return;
        }

        // 기존 슬롯 삭제
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        // 랭킹 데이터마다 슬롯 생성
        foreach (DBManager.RankingData data in rankings)
        {
            RankingSlot slot = Instantiate(slotPrefab, content);
            
            slot.Init(data);
        }
    }
}
