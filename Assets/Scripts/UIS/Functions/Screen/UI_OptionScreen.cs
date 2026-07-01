using UnityEngine;

public class UI_OptionScreen : UI_ScreenBase
{
    public bool IsOpen => gameObject.activeSelf;
    public void Close()
    {

        gameObject.SetActive(false);
    }

    public void Open()
    {
        Debug.Log("open item price menu");

        var ui = UIManager.ClaimGetUI(UIType.ItemPriceMenu);
        Debug.Log(ui);

        gameObject.SetActive(true);


    }
    public void Toggle()
    {

        gameObject.SetActive(!IsOpen);
    }
}
