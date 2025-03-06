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

    /*public ItemData FindItem(int id)//����id������Ʒ����
    {
        return itemData.Sheet1.Find(i => i.id == id);
    }*/
}
