using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : SingleTon<InventoryManager>
{
    public List<itemId> items;//���ӵ�е���Ʒ��ʹ��id����������
    public Items itemData;//excel�������Ʒ���ݼ�
    private void OnEnable()
    {
        DontDestroyOnLoad(gameObject);
    }
    public ItemData FindItem(int id)//����id������Ʒ����
    {
        return itemData.Sheet1.Find(i => i.id == id);
    }
    public void RemoveItem(int id, int itemMount)//�Ƴ���Ʒ
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
    public void AddItem(int id, int itemMount)//�����Ʒ
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
