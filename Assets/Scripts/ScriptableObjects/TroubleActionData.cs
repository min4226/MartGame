using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TroubleActionData", menuName = "Scriptable Objects/TroubleActionData")]
public class TroubleActionData : ScriptableObject
{
    public string actionName;
    public AnimationClip actionClip;
    public float actionDuration;
    public GameObject actionPrfab;
    public List<DialogueData> dialogueTroubleData;
    
}
