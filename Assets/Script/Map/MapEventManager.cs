using UnityEngine;
using UnityEngine.AddressableAssets;

public class MapEventManager : SingleTon<MapEventManager>
{
    public MapEvents eventsData;//所有事件数据
    public EventData currentEvent;
    private void OnEnable()
    {
        EventManager.eventOver += EventOver;
    }
    private void OnDisable()
    {
        EventManager.eventOver -= EventOver;
    }
    public EventData FindItem(int id)//根据id返回物品数据
    {
        return eventsData.Sheet1.Find(i => i.id == id);
    }
    public void EffectTrid(int id)
    {
        Addressables.InstantiateAsync("MapEventPanel").Completed += handle =>
        {
            handle.Result.transform.SetParent(GameObject.Find("Canvas").transform, false);
            var eventUi = handle.Result.GetComponent<MapEventUi>();
            currentEvent = FindItem(id);
            eventUi.eventData = FindItem(id);
            eventUi.InitEventUi();
        };
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            PlaceCard(40, "Att");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            PlaceCard(20, "Att");
        }
    }
    public void PlaceCard(int Value, string ValueType)
    {
        switch (ValueType)
        {
            case "Att": DesEffect(Value,currentEvent.Att);break;
            case "Ref":RefreshEffect(Value,currentEvent.Reply);break;
            case "Con":ControlEffect();break;
        }
    }
    public void DesEffect(int Value,int ValueDemand)
    {
        if (ValueDemand == -1) return;
        AttEventFactory attEventFactory = new AttEventFactory();
        attEventFactory.Create(currentEvent.AttEffid);
        attEventFactory.EventTrig(Value,ValueDemand);
        EventManager.EventOvr();

    }
    public void RefreshEffect(int Value, int ValueDemand)
    {
        if (ValueDemand == -1) return;
        EventManager.EventOvr();
    }
    public void ControlEffect()
    {
        EventManager.EventOvr();
    }
    public void SkipEffect()
    {
        EventManager.EventOvr();
    }
    private void EventOver()
    {
        currentEvent = null;
    }
}
