using System;
using System.Collections;
using System.Collections.Generic;
using MainGame;
using Unity.VisualScripting;
using UnityEngine;
public static class EventManager
{
    public static event Action updateMapUi;//刷新移动力文本以及天数文本
    public static void UpdateMapUi() => updateMapUi?.Invoke();

    public static event Action nextDay;//移动力回复，天数加一，所有事件存在天数减一

    public static void NextDay()//刷新下一日数据
    {
        if (MainGame.State.Instance.currentState != GameState.Map)
            return;
        nextDay?.Invoke();
        EventManager.UpdateMapUi();
        SaveGameData();
    }

    public static event Action updateSlotUi;//当前拥有bag的刷新格子数据的方法
    public static void UpdateSlotUi() => updateSlotUi?.Invoke();

    public static event Action eventOver;//包含清除当前触发事件；摧毁事件ui的效果
    public static void EventOvr() => eventOver?.Invoke();

    public static event Action saveGameData;//保存物品数据,天数数据
    public static void SaveGameData() => saveGameData?.Invoke();


}
