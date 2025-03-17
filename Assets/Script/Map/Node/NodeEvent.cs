using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
public class NodeEvent : MonoBehaviour//����ص�ӵ�е��¼�����
{
    public int EventId;

    public int Day;
    public void EventTrig()
    {
        Day = 0;
        Debug.Log(EventId + "�¼��ѽ���");
        MapEventManager.Instance.EffectEnter(EventId,GetComponent<MapNode>());
    }
    private void OnEnable()
    {
        EventManager.nextDay += DayCost;
    }
    private void OnDisable()
    {
        EventManager.nextDay -= DayCost;
    }
    private void DayCost()
    {
        Day -= 1;
        if (Day == 0)
            Debug.Log(EventId + "�¼��ѽ���");
    }
}
