using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttLosingChild : MapEvent
{
    public override void EffectTrig(int Value = 0,int ValueDemand = 0)
    {
        if(Value > ValueDemand)
        {
            InventoryManager.Instance.Gold += 100;
            Debug.Log("…±À¿¡À–°≈Û”—£°");
        }
        else
        {
            Debug.Log("…±≤ªÀ¿–°≈Û”—£°");
        }
        
    }
}
