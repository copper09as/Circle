using System.Collections;
using System.Collections.Generic;
using Npc;
using UnityEngine;

public class BraveNpc : Npc.Npc
{
    public BraveNpc(string name, List<itemId> items) : base(name, items,1,1)
    {
        this.Health = Random.Range(20, 590);
    }

    public override void OnAngry()
    {
        throw new System.NotImplementedException();
    }

    public override void OnDead()
    {
        throw new System.NotImplementedException();
    }

    public override void OnHappy()
    {
        throw new System.NotImplementedException();
    }

    public override void OnLove()
    {
        throw new System.NotImplementedException();
    }

    public override void OnSad()
    {
        throw new System.NotImplementedException();
    }

    protected override void Init()
    {
        base.Init();
    }


}
