using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;



public class Trigger : MonoBehaviour
{
    int activeItemCount;
    private TMP_InputField inputField;
    private Button EnterButton;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            inputField = GameManager.Instance.InputField;
            EnterButton = GameManager.Instance.EnterButton;
            activeItemCount--;
            

            if (activeItemCount <= 0)
            {
                inputField.gameObject.SetActive(true);
                EnterButton.gameObject.SetActive(true);
            }

            Destroy(other.gameObject);
        }
    }
    public void SetItemCount(int count)
    {
        activeItemCount = count;
        
    }
    
}
