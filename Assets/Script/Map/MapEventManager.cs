using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class MapEventManager : MonoBehaviour
{
    private void Start()
    {
        Addressables.InstantiateAsync("MapEventPanel").Completed += handle =>
        {
            handle.Result.transform.SetParent(transform.parent, false);
        };
    }

    /*public ItemData FindItem(int id)//根据id返回物品数据
    {
        return itemData.Sheet1.Find(i => i.id == id);
    }*/
}
