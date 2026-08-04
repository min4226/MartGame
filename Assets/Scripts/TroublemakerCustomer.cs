using UnityEngine;
using UnityEngine.UI;

public class TroublemakerCustomer : MonoBehaviour
{
    [SerializeField] Image explusionImage;

    public void GenerateExtermination(ExplusionItems explusion)
    {
        int randomExplusion = Random.Range(0, explusion.expulsionItems.Length);
        explusionImage.sprite = explusion.expulsionItems[randomExplusion].ExpulsionItemSprite;
    }


}
