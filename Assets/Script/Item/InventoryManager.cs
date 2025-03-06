using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : SingleTon<InventoryManager>
{
    public List<itemId> items;//玩家拥有的物品，使用id和数量保存
    public Items itemData;//excel储存的物品数据集
    private void OnEnable()
    {
        DontDestroyOnLoad(gameObject);
    }
    public ItemData FindItem(int id)//根据id返回物品数据
    {
        return itemData.Sheet1.Find(i => i.id == id);
    }
    public void RemoveItem(int id, int itemMount)//移除物品
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
            else if (items.Find(i => i.id == id).mount - itemMount == 0)
            {
                items.Remove(items.Find(i => i.id == id));
                //slots[items.Count].ClearData();
            }
            else
                return;
            EventManager.UpdateSlotUi();
        }
        else
        {
            return;
        }

    }
    public void AddItem(int id, int itemMount)//添加物品
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
