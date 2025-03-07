using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class EventManager
{
    public static event Action updateMapUi;//刷新地图ui
    public static void UpdateMapUi() => updateMapUi?.Invoke();

    public static event Action nextDay;

    public static void NextDay()//刷新下一日数据
    {
        
        nextDay?.Invoke();
        EventManager.UpdateMapUi();
    }

    public static event Action updateSlotUi;//当前拥有bag的刷新格子数据的方法
    public static void UpdateSlotUi() => updateSlotUi?.Invoke();

    public static event Action eventOver;

    public static void EventOvr() => eventOver?.Invoke();

}
