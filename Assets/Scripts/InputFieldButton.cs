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
    
    public void OnInputFieldButton()
    {
        inputField = GameManager.Instance.InputField;
        customerSpawn = GameManager.Instance.CustomerSpawn;
        

        Debug.Log($"customerSpawn : {customerSpawn == null}");

        userAnswer = int.Parse(inputField.text);
        answer = GameManager.Instance.NormalCustomer.ItemTotalValue(GameManager.Instance.NormalCustomer.todayItems);

        if (userAnswer == answer)
        {
            GameManager.Instance.CorrectAnswer.SetActive(true);
            inputField.gameObject.SetActive(false);
            GameManager.Instance.RewardModule.ApplyReward();

            //customerSpawn.SpawnNextCustomer();
            customerSpawn.StartCoroutine(GameManager.Instance.CustomerSpawn.NextCustomerRoutine());
        }
        else
        {
            GameManager.Instance.FailAnswer.SetActive(true);
            inputField.gameObject.SetActive(false);

            customerSpawn.StartCoroutine(GameManager.Instance.CustomerSpawn.NextCustomerRoutine());
            //customerSpawn.SpawnNextCustomer();
        }
    }
}
