using UnityEngine;
using UnityEngine.AddressableAssets;

public class MapEventManager : SingleTon<MapEventManager>
{
    public MapEvents eventsData;//�����¼�����
    public EventData currentEvent;
    private MapEventFactory EventFactory;//�¼����칤��
    private void OnEnable()
    {
        EventManager.eventOver += EventOver;
    }
    private void OnDisable()
    {
        EventManager.eventOver -= EventOver;
    }
    public EventData FindItem(int id)//����id������Ʒ����
    {
        return eventsData.Sheet1.Find(i => i.id == id);
    }
    public void EffectTrid(int id)
    {
        if (FindItem(id) == null)
            Debug.LogError("�¼�id����ʧ��");
        Addressables.InstantiateAsync("MapEventPanel").Completed += handle =>
        {
            handle.Result.transform.SetParent(GameObject.Find("Canvas").transform, false);
            var eventUi = handle.Result.GetComponent<MapEventUi>();
            currentEvent = FindItem(id);
            eventUi.eventData = FindItem(id);
            eventUi.InitEventUi();
        };
        EventFactory = new MapEventFactory();
        EventFactory.Create(id);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            PlaceCard(-50);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            PlaceCard(-100);
        }
    }
    public void PlaceCard(int reputation,string limit = null)
    {

        //if (ValueDemand == -1) return;
        EventFactory.EventTrig(reputation);
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
        EventFactory = null;
        currentEvent = null;
    }
}
