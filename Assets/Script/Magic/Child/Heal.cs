using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Magic/Heal")]
public class Heal : Magic
{
    public override void Fuction(Npc.Npc npc)
    {
        npc.TakeRefresh(123);
        npc.TakeLove(300);
    }
    protected override bool CanUse()
    {
        throw new System.NotImplementedException();
    }
}