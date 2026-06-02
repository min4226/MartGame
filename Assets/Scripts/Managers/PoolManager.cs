using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class PoolManager : ManagerBase
{
    [SerializeField] CustomerData[] customerData;
    [SerializeField] Transform poolPosition;
    
    Dictionary<GameObject, Queue<GameObject>> poolDictionary = new Dictionary<GameObject, Queue<GameObject>>();
    Queue<GameObject> poolQueue = new Queue<GameObject>();
    protected override IEnumerator OnConnected(GameManager newManager)
    {
        foreach (CustomerData data in customerData)
        {
           GameObject poolPrefab = Instantiate(data.ageSprite, poolPosition, false);


           poolPrefab.SetActive(false);

           poolQueue.Enqueue(poolPrefab);
            

           poolDictionary.Add(data.ageSprite, poolQueue);
        }
        yield break;
    }

    public GameObject GetCustomer(CustomerData customerData)
    {
        poolQueue.Dequeue();
    }
    

    protected override void OnDisconnected()
    {
        throw new System.NotImplementedException();
    }
}*/
