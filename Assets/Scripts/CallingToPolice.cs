using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class CallingToPolice : MonoBehaviour
{
    public Animator callPoliceAnim;
    public SpriteRenderer PhoneSprite;
    public GameObject takeInSpritePrefab;
    //public Vector3 policePosition;
    
    public void CallPolice()
    {
        PhoneSprite.gameObject.SetActive(true);

        callPoliceAnim.SetTrigger("Call");

        StartCoroutine(Process());
    }

    IEnumerator Process()
    {
        Debug.Log("연행하기");
        yield return new WaitForSeconds(2f);
        PhoneSprite.gameObject.SetActive(false);

        GameObject takeInSprite = Instantiate(takeInSpritePrefab, transform.position, Quaternion.identity);
        GameManager.Instance.currentCustomer.SetActive(false);

        

       

        // 화면 밖으로 나가면 제거
        /*takeInSprite.SetActive(false);
        GameManager.Instance.CustomerSpawn.StartNextCustomer();*/
    }
   
    
}
