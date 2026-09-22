using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Trigger : MonoBehaviour
{
    int activeItemCount;
    private TMP_InputField inputField;
    private Button EnterButton;

    private IEnumerator ShowInputField()
    {
        yield return new WaitForSeconds(1.5f);
        inputField.gameObject.SetActive(true);
        EnterButton.gameObject.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            inputField = GameManager.Instance.InputField;
            EnterButton = GameManager.Instance.EnterButton;

            if (other.gameObject.name.Contains("MartCart"))
                return;

            activeItemCount--;
            inputField.onValidateInput += ValidateNumber;

            if (activeItemCount <= 0)
            {
                StartCoroutine(ShowInputField());
            }

            Destroy(other.gameObject);
        }
    }
    public void SetItemCount(int count)
    {
        activeItemCount = count;
        
    }

    // inputfield에 숫자만 입력하도록 설정
    private char ValidateNumber(string text, int charIndex, char addedChar)
    {
        if (char.IsDigit(addedChar)) // isDigit : 숫자인지 판별
            return addedChar;

        return '\0';
    }
}
