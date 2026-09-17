using UnityEngine;

public class JudgeModule : StageData
{
    // ����, ����

    // �ð� ���� ����
    public TimeResult StageClear(float playingTime, StageData stageData)
    {
        // ���ѽð��� �Ѱ�ų� 0�� ��� ���и� �˷���
        if (playingTime >= stageData.timeLimit || playingTime <= 0) return TimeResult.Fail;

        return TimeResult.Success;
    }
   
    
    
}
