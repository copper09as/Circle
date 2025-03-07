using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MapEventFactory
{    public int EffectId;
    public MapEvent mapEvent;
 
    public abstract void Create(int Effectid);

    public virtual void EventTrig(int value = -1,int demand = -1)
    {
        if (value == -1) return;
    }
}
