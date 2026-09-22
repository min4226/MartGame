using UnityEngine;
using System.Collections.Generic;
using System.Collections;

// 전화기를 눌렀을 때 경찰이 나오는 스크립트
public class CallingToPolice : MonoBehaviour
{
    public Animator callPoliceAnim;
    public SpriteRenderer PhoneSprite;
    public GameObject takeInSpritePrefab;
    
    public void CallPolice()
    {
        PhoneSprite.gameObject.SetActive(true);

        callPoliceAnim.SetTrigger("Call");

        StartCoroutine(Process());
    }

    IEnumerator Process()
    {
        yield return new WaitForSeconds(2f);
        PhoneSprite.gameObject.SetActive(false);

        GameObject takeInSprite = Instantiate(takeInSpritePrefab, transform.position, Quaternion.identity);
        GameManager.Instance.currentCustomer.SetActive(false);
    }
   
}
