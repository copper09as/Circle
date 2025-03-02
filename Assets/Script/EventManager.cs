using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public static event Action updateMapUi;//Ë¢ÐÂui
    public static void UpdateMapUi() => updateMapUi?.Invoke();

    public static event Action updateSlotUi;

    public static void UpdateSlotUi() => updateSlotUi?.Invoke();

}
