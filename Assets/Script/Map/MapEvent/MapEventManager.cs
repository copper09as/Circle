using Npc;
using UnityEngine;
using UnityEngine.AddressableAssets;
public class MapEventManager : SingleTon<MapEventManager>
{
    public MapEvents eventsData;//所有事件数据
    public EventData currentEvent;
    public Npc.Npc npc;
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
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            npc.TakeDamage(1111);
            EventManager.EventOvr();
        }
    }
    public void EffectTrid(int id)
    {
        if (FindItem(id) == null)
            Debug.LogError("事件id查找失败");
        Addressables.InstantiateAsync("MapEventPanel").Completed += handle =>
        {
            handle.Result.transform.SetParent(GameObject.Find("Canvas").transform, false);
            var eventUi = handle.Result.GetComponent<MapEventUi>();
            currentEvent = FindItem(id);
            eventUi.eventData = FindItem(id);
            eventUi.InitEventUi();
            eventUi.npc = new LostChild(null,null);
            this.npc = eventUi.npc;
        };
        //EventFactory = new MapEventFactory();
        //EventFactory.Create(id);
    }

    public void PlaceCard()
    {
            //EventFactory.EventTrig();
            EventManager.EventOvr();

        /*switch (ValueType)
        {
            case "Att": DesEffect(Value,currentEvent.Att);break;
            case "Ref":RefreshEffect(Value,currentEvent.Reply);break;
            case "Con":ControlEffect();break;
        }*/
    }
    /*public void DesEffect(int Value,int ValueDemand)
    {
        if (ValueDemand == -1) return;
        MapEventFactory attEventFactory = new MapEventFactory();
        attEventFactory.Create(currentEvent.id);
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
    }*/
    public void SkipEffect()
    {
        EventManager.EventOvr();
    }
    private void EventOver()
    {
        npc = null;
        currentEvent = null;
    }
}
