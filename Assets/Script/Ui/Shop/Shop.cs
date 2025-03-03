using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public List<ShopSlot> slots;
    public List<itemId> items;
    private void Start()
    {
        UpdateShopSlotUi();
    }
    private void UpdateShopSlotUi()
    {
        for (int i = 0; i < items.Count; i++)
        {
            var item = InventoryManager.Instance.FindItem(items[i].id);
            slots[i].Mount = items[i].mount;
            slots[i].itemData = item;
            slots[i].GetComponent<Image>().sprite = item.sprite;
            slots[i].mountText.text = slots[i].Mount.ToString();
        }
    }
    public void RemoveItem(int id, int itemMount)
    {
        bool isBeyond = false;
        foreach (var i in items)
        {
            if (id == i.id)
                isBeyond = true;
        }
        if (isBeyond)
        {
            if (items.Find(i => i.id == id).mount - itemMount > 0)
            {
                items.Find(i => i.id == id).mount -= itemMount;
            }
            else
            {
                items.Remove(items.Find(i => i.id == id));
                slots[items.Count].ClearData();
            }
        }

        else
        {
            return;
        }
        UpdateShopSlotUi();

    }
}
