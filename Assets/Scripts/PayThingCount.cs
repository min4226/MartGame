using TMPro;
using UnityEngine;

public class PayThingCount : MonoBehaviour
{
    public GameObject rightButton;
    public GameObject leftButton;

    public TMP_InputField _inputField;
    public int payCount;
    
    private void Start()
    {
        payCount = 0;
        UpdateInputField();
    }

    private void UpdateInputField()
    {
        _inputField.text = payCount.ToString();
    }

    public void PressLeftButton()
    {
        if (payCount > 0)
        {
            payCount--;
            UpdateInputField();
        }
    }

    public void PressRightButton()
    {
        payCount++;
        UpdateInputField();
    }

    public void AddNumber(int number)
    {
        // 현재 0이면 0을 지우고 숫자를 입력
        if (_inputField.text == "0")
        {
            _inputField.text = number.ToString();
        }
        else
        {
            _inputField.text += number.ToString();
        }

        // InputField의 값을 payCount에도 반영
        if (int.TryParse(_inputField.text, out int result))
        {
            payCount = result;
        }
    }

    public void DeleteNumber()
    {
        if (_inputField.text.Length > 0)
        {
            _inputField.text = _inputField.text.Substring(
                0,
                _inputField.text.Length - 1
            );
        }

        if (string.IsNullOrEmpty(_inputField.text))
        {
            _inputField.text = "0";
        }

        if (int.TryParse(_inputField.text, out int result))
        {
            payCount = result;
        }
    }

    public void ResetCount()
    {
        payCount = 0;
        _inputField.text = "0";
        
    }
}