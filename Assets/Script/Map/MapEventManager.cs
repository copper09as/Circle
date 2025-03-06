using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class MapEventManager : SingleTon<MapEventManager>
{
    public MapEvents eventsData;//所有事件数据
    private void Start()
    {

    }

    public EventData FindItem(int id)//根据id返回物品数据
    {
        return eventsData.Sheet1.Find(i => i.id == id);
    }
    public void EffectTrid(int id)
    {

        Addressables.InstantiateAsync("MapEventPanel").Completed += handle =>
        {
            handle.Result.transform.SetParent(transform.parent, false);
            var eventUi = handle.Result.GetComponent<MapEventUi>();
            eventUi.EventName.text = FindItem(id).Name;
            eventUi.EventDescription.text = FindItem(id).Description;
        };
    }
}
