using System;
using TMPro;
using UnityEngine;

public class ChangeStageLevel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI stageText;
    [SerializeField] StageContainer stageData;

    void Start()
    {
        GameManager.Instance.Stage.OnStageChanged += ChangeLevel;    
    }
    public void ChangeLevel()
    {
        stageText.text = stageData.stageDatas[GameManager.Instance.Stage.CurrentIndex].stageName.ToString();
    }
}
