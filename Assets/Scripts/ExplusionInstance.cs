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
    private ExpulsionItem item;
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
        item = currentItems[index];
        if (item.name == "MartCart")
        {
            ClickManager clickManager = FindFirstObjectByType<ClickManager>(FindObjectsInactive.Include);
            this.gameObject.SetActive(false);
            Debug.Log($"clickmanager : {clickManager}");
            clickManager.CartCollider();
            return;
        }
        this.gameObject.SetActive(false);
        Debug.Log("선택 후 item : " + item);

        

        dragImage.sprite = item.ExpulsionItemSprite;
        dragObject.SetActive(true);
    }
    public ExpulsionItem GetSelectedItem()
    {
        Debug.Log($"getselecteditem에서의 item : {item}");
        return item;
    }

}