using UnityEngine;
// 나의 마트로 가기 씬에서 단말기를 눌렀을 때 빨간 버튼을 눌렀을 경우
// 상점템이 나오고 초록색 버튼을 눌렀을 때 내 아이템 패널이 나오는 코드
public class ItemlListInstance : MonoBehaviour
{
    [SerializeField] GameObject shopItem;
    [SerializeField] GameObject myItem;
    
    public void OnShopItemListButton()
    {
        shopItem.SetActive(true);
        myItem.SetActive(false);
    }

    public void OnMyItemListButton()
    {
        myItem.SetActive(true);
        shopItem.SetActive(false);
    }

    public void OnBothExitButton()
    {
        myItem.SetActive(false);
        shopItem.SetActive(false);
    }

}
