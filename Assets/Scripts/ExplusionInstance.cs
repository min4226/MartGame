using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplusionInstance : MonoBehaviour
{
    public ExplusionItems explusion;
    public Image[] imagePanel;
    public Image dragImage;
    public GameObject dragObject;
    private ExpulsionItem[] currentItems;

    void Start()
    {
        List<ExpulsionItem> randomItems = new List<ExpulsionItem>(explusion.expulsionItems);

        currentItems = new ExpulsionItem[imagePanel.Length];

        for (int i = 0; i < imagePanel.Length; i++)
        {
            int randomIndex = Random.Range(0, randomItems.Count);

            currentItems[i] = randomItems[randomIndex];

            imagePanel[i].sprite = currentItems[i].ExpulsionItemSprite;

            randomItems.RemoveAt(randomIndex);
        }
    }

    public void SelectItem(int index)
    {
        ExpulsionItem item = currentItems[index];
        dragImage.sprite = item.ExpulsionItemSprite;
        dragObject.SetActive(true);
        
    }
}