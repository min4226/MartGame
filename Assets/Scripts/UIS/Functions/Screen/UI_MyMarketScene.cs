using UnityEngine;

public class UI_MyMarketScene : UI_ScreenBase
{
    private void OnEnable()
    {
        if (GameManager.Instance == null)
            return;

        if (GameManager.Instance.CustomerSpawn == null)
            return;

        GameManager.Instance.CustomerSpawn.SetCustomerVisible(false);
    }

    private void OnDisable()
    {
        if (GameManager.Instance == null)
            return;

        if (GameManager.Instance.CustomerSpawn == null)
            return;

        GameManager.Instance.CustomerSpawn.SetCustomerVisible(true);
    }
}
