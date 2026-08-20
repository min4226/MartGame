using JetBrains.Annotations;
using System;
using System.Net.Mime;
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
        //inputField.contentType = test;
            // TMP_InputField.ContentType.IntegerNumber; // 숫자만 쓰는 게 가능하도록
        inputField.characterLimit = 7; // 글자 수 제한

        inputField.onValidateInput = test;
    }
    
    // addedchar에서 넣었을 때 숫자, 단어를 적었을 땐 가능하도록 아니면 빈칸이 나오도록
    private char test(string text, int charIndex, char addedChar)
    {
        if (!char.IsLetterOrDigit(addedChar))
        {
            return ' ';
        }
        return addedChar;
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
