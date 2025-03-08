using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LosingChild : MapEvent
{
    public override void EffectTrig(int reputation,string limit = null)
    {
        switch (reputation)
        {
            case -100:
                InventoryManager.Instance.Gold += 100;
                Debug.Log("…±À¿¡À–°≈Û”—£°");
                break;
            case -50:
                Debug.Log("…±≤ªÀ¿–°≈Û”—£°");
                break;




        }


        /*if(Value > ValueDemand)
        {
            InventoryManager.Instance.Gold += 100;
            Debug.Log("…±À¿¡À–°≈Û”—£°");
        }
        else
        {
            Debug.Log("…±≤ªÀ¿–°≈Û”—£°");
        }*/
    }
}
