using UnityEngine;
using SFB;
using System.IO;
using UnityEngine.UI; // 파일 등을 가져오기 위함
public class ImageChangeButton : MonoBehaviour
{
    public Image profileImage;

    public void OpenGallery()
    {
        string[] paths = StandaloneFileBrowser.OpenFilePanel("사진 선택", 
            "","", false);
        if (paths.Length == 0) return;
        byte[] imageData = File.ReadAllBytes(paths[0]);
        Texture2D texture = new Texture2D(2,2);
        texture.LoadImage(imageData);

        Sprite newSprite = Sprite.Create(texture, new Rect(0,0, texture.width,
            texture.height),new Vector2(0.5f, 0.5f));

        profileImage.sprite = newSprite;
    }
}
