using System.Collections;
using System.Collections.Generic;
using MainGame;
using UnityEngine;

public abstract class Magic : TScriptableObject
{
    public string MagicName;
    public MagicKind MagicType;
    public Sprite sprite;
    public virtual void Fuction()
    {
       
    }
    public virtual void Fuction(Npc.Npc npc)
    {
        return;
    }
    protected abstract bool CanUse();
}
