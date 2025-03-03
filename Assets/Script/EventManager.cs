using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class EventManager
{
    public static event Action updateMapUi;//Ë¢ÐÂui
    public static void UpdateMapUi() => updateMapUi?.Invoke();

    public static event Action updateSlotUi;
    public static void UpdateSlotUi() => updateSlotUi?.Invoke();

    public static event Action<int,int> addItem;

    public static void AddItem(int id, int mount) => addItem?.Invoke(id, mount);

    public static event Action<int, int> removeItem;

    public static void RemoveItem(int id, int mount) => removeItem?.Invoke(id, mount);

}
