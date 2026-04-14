using UnityEngine;

public class UI_QuitConfirmWindow : OpenbleUIBase
{
    public void Confirm() => GameManager.QuitGame();

    public void Cancel() => UIManager.ClaimCloseUI(UIType.GameQuit);



}
