using System;
using TMPro;
using UnityEngine;

public class ChangeStageLevel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI stageText;
    [SerializeField] StageContainer stageData;

    public void ChangeLevel()
    {
        stageText.text = stageData.stageDatas[0].stageName.ToString();
    }
}
