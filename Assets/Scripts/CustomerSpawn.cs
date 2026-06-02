using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
public class CustomerSpawn : MonoBehaviour 
{
    [SerializeField] CustomerData[] customerData; 
    [SerializeField] Transform poolPosition; 
    void Start() 
    { 
        StartCoroutine(SpawnCustomer()); 
    } 
    IEnumerator SpawnCustomer()
    { 
        while (true) 
        { 
            CustomerData data = customerData[Random.Range(0, customerData.Length)];
            GameObject prefabCustomer = Instantiate(data.ageSprite, poolPosition, false); 
            yield return new WaitForSeconds(1); 
            Destroy(prefabCustomer);
        } 
    }

    public void Destroy()
    { 
        gameObject.SetActive(false);
    }
}