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
                Debug.Log("ɱ����С���ѣ�");
                break;
            case -50:
                Debug.Log("ɱ����С���ѣ�");
                break;




        }


        /*if(Value > ValueDemand)
        {
            InventoryManager.Instance.Gold += 100;
            Debug.Log("ɱ����С���ѣ�");
        }
        else
        {
            Debug.Log("ɱ����С���ѣ�");
        }*/
    }
}
