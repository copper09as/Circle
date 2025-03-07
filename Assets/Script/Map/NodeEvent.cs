using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class NodeEvent : MonoBehaviour
{
    public int EventId;

    public int Day;
    public void EventTrig()
    {
        MapEventManager.Instance.EffectTrid(EventId);
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
            Debug.Log(EventId + "事件已结束");
    }
}
