using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
[CreateAssetMenu(fileName = "InventoryInventoryList_SO", menuName = "Inventory/item")]
[Serializable]
public class ItemData:TScriptableObject
{
    public int id;
    public string Name;
    public int price;
    public Sprite image;
    public string description;
    public int effectId;
    public void Func()
    {
        Type targetType = typeof(ItemEfect);
        MethodInfo staticMethod = targetType.GetMethod(Name);
        staticMethod.Invoke(null, new object[] {});
    }
}
