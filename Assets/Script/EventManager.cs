using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class EventManager
{
    public static event Action updateMapUi;//ˢ�µ�ͼui
    public static void UpdateMapUi() => updateMapUi?.Invoke();

    public static event Action nextDay;

    public static void NextDay()
    {
        
        nextDay?.Invoke();
        EventManager.UpdateMapUi();
    }

    public static event Action updateSlotUi;//��ǰӵ��bag��ˢ�¸������ݵķ���
    public static void UpdateSlotUi() => updateSlotUi?.Invoke();

}
