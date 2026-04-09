using System.Collections;
using UnityEngine;

public  abstract class ManagerBase : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    GameManager _connectedManager;

    public virtual int LoadingCount => 1;

    public IEnumerator Connect(GameManager newManager)
    {
        if (_connectedManager != null) Disconnect();

        _connectedManager = newManager;
        yield return OnConnected(newManager);
    }
    public void Disconnect()
    {
        _connectedManager = null;
        OnDisconnected();

    }

    protected abstract IEnumerator OnConnected(GameManager newManager);

    protected abstract void OnDisconnected();
}
