using System;
using System.Collections;
using System.Collections.Generic;
using MainGame;
using Unity.VisualScripting;
using UnityEngine;
public static class EventManager
{
    public static event Action updateMapUi;//ˢ���ƶ����ı��Լ������ı�
    public static void UpdateMapUi() => updateMapUi?.Invoke();

    public static event Action nextDay;//�ƶ����ظ���������һ�������¼�����������һ

    public static void NextDay()//ˢ����һ������
    {
        if (MainGame.State.Instance.currentState != GameState.Map)
            return;
        nextDay?.Invoke();
        EventManager.UpdateMapUi();
        SaveGameData();
    }

    public static event Action updateSlotUi;//��ǰӵ��bag��ˢ�¸������ݵķ���
    public static void UpdateSlotUi() => updateSlotUi?.Invoke();

    public static event Action eventOver;//���������ǰ�����¼����ݻ��¼�ui��Ч��
    public static void EventOvr() => eventOver?.Invoke();

    public static event Action saveGameData;//������Ʒ����,��������
    public static void SaveGameData() => saveGameData?.Invoke();


}
