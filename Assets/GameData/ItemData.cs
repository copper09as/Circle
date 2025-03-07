using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "InventoryInventoryList_SO", menuName = "Inventory/item")]
[Serializable]
public class ItemData:TScriptableObject
{
    public int id;
    public string name;
    public int price;
    public Sprite image;
    public string description;
    public int effectId;
}
