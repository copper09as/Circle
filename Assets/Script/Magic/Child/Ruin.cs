using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Magic/Ruin")]
public class Ruin : Magic
{
    public override void Fuction(Npc.Npc npc)
    {
        base.Fuction(npc);
        npc.TakeDamage(1231);
        //毁灭法阵的效果
        Debug.Log("毁灭法术触发");
    }

    protected override bool CanUse()
    {
        return MainGame.State.Instance.currentState == MainGame.GameState.Event;//在此阶段才能使用的魔法
    }
}
