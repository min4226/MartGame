using UnityEngine;

public class JudgeModule : StageData
{
    // 성공, 실패

    // 시간 제한 판정
    public TimeResult StageClear(float playingTime, StageData stageData)
    {
        // 제한시간을 넘겼거나 0일 경우 실패를 알려줌
        if (playingTime >= stageData.timeLimit || playingTime <= 0) return TimeResult.Fail;

        return TimeResult.Success;
    }
   
    
    
}
