using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplusionInstance : MonoBehaviour
{
    public ExplusionItems explusion;
    public Image[] imagePanel;

    void Start()
    {
        
        List<ExpulsionItem> randomItems = new List<ExpulsionItem>(explusion.expulsionItems);

        for (int i = 0; i < imagePanel.Length; i++)
        {
            int randomIndex = Random.Range(0, randomItems.Count);

            imagePanel[i].sprite = randomItems[randomIndex].ExpulsionItemSprite;
            //imagePanel[i].SetNativeSize(); // 원본 사진 크기 그대로
            
            randomItems.RemoveAt(randomIndex);
        }
    }
}