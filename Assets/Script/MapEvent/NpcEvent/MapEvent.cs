using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MapEvent//�¼�Ч������
{
    public abstract void EffectTrig(int reputation,int threat,string limit = null);

}
