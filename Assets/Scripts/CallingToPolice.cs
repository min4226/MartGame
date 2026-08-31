using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class CallingToPolice : MonoBehaviour
{
    public Animator callPoliceAnim;
    public SpriteRenderer PhoneSprite;
    public GameObject takeInSpritePrefab;
    public Vector3 policePosition;

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

        GameObject takeInSprite = Instantiate(takeInSpritePrefab, 
            policePosition, Quaternion.identity);
        Debug.Log($"takeinsprite : {takeInSprite}");

        while (takeInSprite.transform.position.x > -10f)
        {
            takeInSprite.transform.position += Vector3.left * 5f * Time.deltaTime;
            yield return null;
        }

        // 화면 밖으로 나가면 제거
        takeInSprite.SetActive(false);
    }
}
