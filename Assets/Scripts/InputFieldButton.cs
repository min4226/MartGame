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
    StageData stageData;
    CustomerSpawn customerSpawn;

    private void Start()
    {
        inputField = GameManager.Instance.InputField;
        inputField.contentType = TMP_InputField.ContentType.IntegerNumber; // 숫자만 쓰는 게 가능하도록
        inputField.characterLimit = 7; // 글자 수 제한
        
    }
    public void OnInputFieldButton()
    {
        
        customerSpawn = GameManager.Instance.CustomerSpawn;
        
        userAnswer = int.Parse(inputField.text);
        answer = GameManager.Instance.NormalCustomer.ItemTotalValue(GameManager.Instance.NormalCustomer.todayItems);

        if (userAnswer == answer)
        {
            GameManager.Instance.CorrectAnswer.SetActive(true);
            inputField.gameObject.SetActive(false);
            GameManager.Instance.RewardModule.ApplyReward();
            customerSpawn.StartCoroutine(GameManager.Instance.CustomerSpawn.NextCustomerRoutine());
        }
        else
        {
            GameManager.Instance.FailAnswer.SetActive(true);
            inputField.gameObject.SetActive(false);
            customerSpawn.StartCoroutine(GameManager.Instance.CustomerSpawn.NextCustomerRoutine());
        }
    }

    
}
