using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MapEvent//事件效果处理
{
    public abstract void EffectTrig(int reputation,int threat,string limit = null);

}
