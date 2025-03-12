using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : TScriptableObject
{
    public string MagicName;
    public MagicKind MagicType;
    public Sprite sprite;
    public virtual void Fuction()
    {
        return;
    }
    public virtual void Fuction(Npc.Npc npc)
    {
        return;
    }
}
