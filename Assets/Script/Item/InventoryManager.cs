using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : SingleTon<InventoryManager>
{
    public List<itemId> items;
    public ItemListSo itemListSo;
    public int Gold;
    private void OnEnable()
    {
        EventManager.addItem += AddItem;
        EventManager.removeItem += RemoveItem;
    }
    private void OnDisable()
    {
        EventManager.addItem -= AddItem;
        EventManager.removeItem -= RemoveItem;
    }
    public ItemData FindItem(int id)
    {
        return itemListSo.itemData.Find(i => i.id == id);
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
                //slots[items.Count].ClearData();
            }

            EventManager.UpdateSlotUi();
        }

        else
        {
            return;
        }

    }
    public void AddItem(int id, int itemMount)
    {
        bool isBeyond = false;
        foreach (var i in items)
        {
            if (id == i.id)
                isBeyond = true;
        }
        if (isBeyond)
        {
            items.Find(i => i.id == id).mount += itemMount;
        }

        else
        {
            var _item = new itemId
            {
                id = id,
                mount = itemMount
            };
            items.Add(_item);
        }
        EventManager.UpdateSlotUi();
    }
}
