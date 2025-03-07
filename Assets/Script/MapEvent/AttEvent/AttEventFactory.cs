using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttEventFactory : MapEventFactory
{


    public override void Create(int Effectid)
    {
        switch(Effectid)
        {
            case 10000:
                this.mapEvent = new AttLosingChild();break;
        }
    }
    public override void EventTrig(int value = -1,int demand = -1)
    {
        Debug.Log("工厂触发效果");

        this.mapEvent.EffectTrig(value,demand);
    }
}

