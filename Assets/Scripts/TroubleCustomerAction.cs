using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TroubleCustomerAction : MonoBehaviour
{
    Transform actionSpawnPoint;
    Transform dialogueUI;
    Image balloonImage;
    TextMeshProUGUI dialogueText;
    Animator animator;
    TroubleActionData actionData;
    Canvas canvas;
    GameObject actionObject = null;
    private void Awake()
    {
        Transform canvas = transform.Find("Canvas");
        actionData = FindAnyObjectByType<TroubleActionData>();
        
        if (canvas != null)
        {
            Transform processObj = canvas.Find("ProcessObj");

            if (processObj != null)
            {
                processObj.gameObject.SetActive(false);
            }
        }
        GameObject background = GameObject.FindGameObjectWithTag("PlayGame");
        
        if (background != null)
        {
            Transform troubleItemSpawn = background.transform.Find("TroubleItemSpawn");
            
            if (troubleItemSpawn != null)
            {
                actionSpawnPoint = troubleItemSpawn;
            }
            
        }
        
    
}
    public void StartActions(CustomerData data)
    {
        StartCoroutine(ExecuteActions(data));
    }

    private IEnumerator ExecuteActions(CustomerData data)
    {
        if (data.troubleActions == null || data.troubleActions.Count == 0)
            yield break;

        int randomIndex = Random.Range(0, data.troubleActions.Count);

        TroubleActionData action = data.troubleActions[randomIndex];

        yield return StartCoroutine(Execute(action));
    }
    public IEnumerator Execute(TroubleActionData action)
    {
        if (action.actionPrfab != null)
        {
            actionObject = Instantiate(
                action.actionPrfab,
                actionSpawnPoint.position,
                Quaternion.identity
            );
        }

        // 말풍선
        if (action.dialogueTroubleData != null &&
            action.dialogueTroubleData.Count > 0)
        {
            yield return StartCoroutine(ShowDialogues(action.dialogueTroubleData));
        }

        // 대사 끝난 후 3초 대기
        yield return new WaitForSeconds(3f);

        // 퇴치물건 UI
        ShowExpulsionUI();

        yield return new WaitForSeconds(action.actionDuration);
    }

    private IEnumerator ShowDialogues(List<DialogueData> dialogues)
    {
        UI_StageScreen stageScreen = FindFirstObjectByType<UI_StageScreen>();

        dialogueUI = stageScreen.transform.Find("SpeechBubble");
        balloonImage = dialogueUI.GetComponentInChildren<Image>(true);
        dialogueText = dialogueUI.GetComponentInChildren<TextMeshProUGUI>(true);

        // 대사가 없으면 종료
        if (dialogues == null || dialogues.Count == 0)
            yield break;

        // 랜덤으로 하나 선택
        int randomIndex = Random.Range(0, dialogues.Count);
        DialogueData dialogue = dialogues[randomIndex];

        balloonImage.sprite = dialogue.balloonSprite;
        dialogueText.text = dialogue.dialogue;

        dialogueUI.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);

        dialogueUI.gameObject.SetActive(false);
        if (actionObject == null)
            yield return null;
        else
            actionObject.SetActive(false);
        
        yield return new WaitForSeconds(0.5f);
    }
    private void ShowExpulsionUI()
    {
        Transform canvas = transform.Find("Canvas");

        Transform processObj = canvas.Find("ProcessObj");

        processObj.gameObject.SetActive(true);
    }
}

