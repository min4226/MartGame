using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WriteTroubleCustomerName : MonoBehaviour
{
    TextMeshProUGUI troubleCustomerName;
    TMP_InputField writeTroubleCustomerName;
    // 적는 공간의 인풋 필드, 띄울 인풋 필드
    public void OnWriteTroubleCustomerNameButton()
    {
        Debug.Log("이름 입력 버튼 함수 실행");
        GameObject canvas = GameObject.Find("Canvas");
        Debug.Log($"canvas : {canvas}");
        TextMeshProUGUI troubleCustomerName = canvas.transform.Find("TroubleCustomerName").GetComponent<TextMeshProUGUI>(); // 띄워질 인풋필드 들고오는 것
        TMP_InputField writeTroubleCustomerName = GameObject.Find("WriteTroubleCustomerName").GetComponent<TMP_InputField>();
    }
}