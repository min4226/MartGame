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
    [SerializeField] private Transform actionSpawnPoint;

    [SerializeField] private GameObject dialogueUI;
    [SerializeField] private Image balloonImage;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [SerializeField] private Animator animator;

    private void Awake()
    {
        GameObject background = GameObject.FindGameObjectWithTag("PlayGame");
        Debug.Log($"background : {background}");
        if (background != null)
        {
            Transform troubleItemSpawn =
                background.transform.Find("TroubleItemSpawn");

            if (troubleItemSpawn != null)
            {
                actionSpawnPoint = troubleItemSpawn;
            }
            else
            {
                Debug.LogError("BackGround 아래에서 TroubleItemSpawn을 찾지 못했습니다.");
            }
        }
        else
        {
            Debug.LogError("PlayGame 태그가 붙은 BackGround를 찾지 못했습니다.");
        }
    }

    public void StartActions(CustomerData data)
    {
        StartCoroutine(ExecuteActions(data));
    }

    private IEnumerator ExecuteActions(CustomerData data)
    {
        foreach (TroubleActionData action in data.troubleActions)
        {
            yield return StartCoroutine(Execute(action));
        }
    }
    public IEnumerator Execute(TroubleActionData action)
    {
        // 1. 애니메이션
        if (action.actionClip != null)
        {
            animator.Play(action.actionClip.name);
        }

        // 2. 프리팹 생성
        if (action.actionPrfab != null)
        {
            Instantiate(
                action.actionPrfab,
                actionSpawnPoint.position,
                Quaternion.identity
            );
        }

        // 3. 말풍선
        if (action.dialogueTroubleData != null &&
            action.dialogueTroubleData.Count > 0)
        {
            yield return StartCoroutine(
                ShowDialogues(action.dialogueTroubleData)
            );
        }

        // 4. 행동 시간
        yield return new WaitForSeconds(action.actionDuration);
    }


    private IEnumerator ShowDialogues(List<DialogueData> dialogues)
    {
        foreach (DialogueData dialogue in dialogues)
        {
            balloonImage.sprite = dialogue.balloonSprite;
            dialogueText.text = dialogue.dialogue;

            dialogueUI.SetActive(true);

            yield return new WaitForSeconds(2f);

            dialogueUI.SetActive(false);

            yield return new WaitForSeconds(0.5f);
        }
    }
}

