using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldButton : MonoBehaviour
{
    TMP_InputField inputField;
    int userAnswer;
    int answer;
    Reward reward;

    public void OnInputFieldButton()
    {
        inputField = GameManager.Instance.InputField;
        Debug.Log($"inputfield : {inputField == null}");
        userAnswer = int.Parse(inputField.text);
        answer = GameManager.Instance.NormalCustomer.ItemTotalValue(GameManager.Instance.NormalCustomer.todayItems);

        if (userAnswer == answer)
        {
            Debug.Log("정답!");
            GameManager.Instance.CorrectAnswer.SetActive(true);
            inputField.gameObject.SetActive(false);
            GameManager.Instance.RewardModule.ApplyReward(reward);
        }
        else
        {
            Debug.Log("틀렸습니다!");
            GameManager.Instance.FailAnswer.SetActive(true);
            inputField.gameObject.SetActive(false);
            GameManager.Instance.RewardModule.ApplyReward(reward);
            Debug.Log($"보상이 잘 실행이 되는가 {reward}");
        }
    }
}
