using Npc;
using UnityEngine;
public class MapEventManager : SingleTon<MapEventManager>
{
    public MapEvents eventsData;//所有事件数据


    
    public MapEventUi eventUi;
    public Npc.Npc npc;
    private void OnEnable()
    {
        EventManager.eventOver += EventOver;
    }
    private void OnDisable()
    {
        EventManager.eventOver -= EventOver;
    }
    public EventData FindEvent(int id)//根据id返回事件数据
    {
        return eventsData.Sheet1.Find(i => i.id == id);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            npc.TakeDamage(11);
            Debug.Log("用于测试npc死亡");
        }
    }
    private bool CanFindEvent(int id)
    {
        return FindEvent(id) != null;
    }
    private void SetEventUi(int id,Npc.Npc npc)//启用事件ui
    {
        eventUi.gameObject.SetActive(true);
        eventUi.eventData = FindEvent(id);
        eventUi.InitEventUi();
        eventUi.npc = npc;
    }
    public void EffectEnter(int id)
    {
        Debug.Assert(CanFindEvent(id), "事件库无此事件");
        SetEventUi(id, new Defector());
        this.npc = eventUi.npc;
    }
    public void PlaceCard()
    {
        EventManager.EventOvr();
    }
    public void SkipEffect()
    {
        EventManager.EventOvr();
    }
    private void EventOver()
    {
        npc = null;
    }
}
