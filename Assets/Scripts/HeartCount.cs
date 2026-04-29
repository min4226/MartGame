using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeartCount : MonoBehaviour
{
    int count;
    public TextMeshProUGUI heartCount;
    public Sprite emptyHeart;
    public Sprite fullHeart;
    public Image heartImage;
    
    public void Start()
    {
        count = 0;
        heartCount.text = count.ToString();
        heartImage.sprite = emptyHeart;
    }
    public void PressHeart()
    {
        count++;
        heartCount.text = count.ToString();
        if (count >= 1)
        {
            heartImage.sprite = fullHeart;
        }
        else
        {
            heartImage.sprite = emptyHeart;
        }

    }
}
