using Npc;
using UnityEngine;
public class MapEventManager : SingleTon<MapEventManager>
{
    public MapEvents eventsData;//�����¼�����
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
    public EventData FindEvent(int id)//����id�����¼�����
    {
        return eventsData.Sheet1.Find(i => i.id == id);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            npc.TakeDamage(1111);
            Debug.Log("���ڲ���npc����");
        }
    }
    private bool CanFindEvent(int id)
    {
        return FindEvent(id) != null;
    }
    private void SetEventUi(int id,Npc.Npc npc)//�����¼�ui
    {
        eventUi.gameObject.SetActive(true);
        eventUi.eventData = FindEvent(id);
        eventUi.InitEventUi();
        eventUi.npc = npc;
    }
    public void EffectEnter(int id,MapNode node)
    {
        Debug.Assert(CanFindEvent(id), "�¼����޴��¼�");
        var npc = node.AddNpc(FindEvent(id).NpcTag);
        SetEventUi(id, npc);
        this.npc = eventUi.npc;
    }
    public void PlaceCard()
    {
        EventManager.EventOver();
    }
    public void SkipEffect()
    {
        EventManager.EventOver();
    }
    private void EventOver()
    {
        //npc = null;
    }
}
