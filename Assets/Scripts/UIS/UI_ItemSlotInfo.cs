using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemSlotInfo : UIBase
{
    [SerializeField] Image iconImage;
    [SerializeField] TextMeshProUGUI amountText;
    [SerializeField] Sprite errorIcon;
    ItemSlots connectedSlot;
    public void ConnectSlot(ItemSlots targetSlot)
    {
        DisconnectSlot();
        if (targetSlot is null) return;
        connectedSlot = targetSlot;
        connectedSlot.OnItemSlotChanged -= VisualUpdate;
        connectedSlot.OnItemSlotChanged += VisualUpdate;
        VisualUpdate(connectedSlot);
    }

    public void DisconnectSlot()
    {
        if (connectedSlot is null) return;
        connectedSlot.OnItemSlotChanged -= VisualUpdate;
        connectedSlot = null;
    }

    protected virtual void VisualUpdate(ItemSlots targetSlot)
    {
        if (targetSlot is null) return;
        Item targetItem = targetSlot.GetItem();

        if (iconImage)
        {
            if (targetItem)
            {
                iconImage.sprite = targetItem.icon ?? errorIcon;
                iconImage.enabled = true;
            }
            else
            {
                iconImage.enabled = false;
            }
        }
        if (amountText)
        {
            int targetStack = targetSlot.GetStack();
            if (!targetItem || targetItem.maxStack <= 1 || targetStack <= 0)
            {
                amountText.SetText("");
            }
            else
            {
                bool isMax = targetSlot.GetIsMax();
                if (isMax) amountText.color = Color.red; 
                else amountText.color = Color.cornflowerBlue;

                amountText.SetText($"{targetStack}");
            }

        }
    }
}
