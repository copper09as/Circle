using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : SingleTon<InventoryManager>
{
    public ItemListSo itemListSo;
    public ItemData FindItem(int id)
    {
        return itemListSo.itemData.Find(i => i.id == id);
    }
}
