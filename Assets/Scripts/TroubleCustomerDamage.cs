using JetBrains.Annotations;
using Ookii.Dialogs;
using UnityEngine;

public class TroubleCustomerDamage : MonoBehaviour
{
    CustomerData customerData;
    private int currentHP;
    private ExpulsionItem currentItem;
    public ExplusionInstance explusion;
    private void Awake()
    {
        currentHP = customerData.troubleCustomerHealth;
        currentItem = explusion.GetSelectedItem();
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("데미지 주기");
        currentHP -= damage;
    }
}
