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
        //�������Ч��
        Debug.Log("����������");
    }

    protected override bool CanUse()
    {
        return MainGame.State.Instance.currentState == MainGame.GameState.Event;//�ڴ˽׶β���ʹ�õ�ħ��
    }
}
