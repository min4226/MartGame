using System.Collections;
using UnityEngine;

public class PoolManager : ManagerBase
{
    [SerializeField] CustomerData[] customerData;
    protected override IEnumerator OnConnected(GameManager newManager)
    {
        throw new System.NotImplementedException();
    }

    protected override void OnDisconnected()
    {
        throw new System.NotImplementedException();
    }
}
