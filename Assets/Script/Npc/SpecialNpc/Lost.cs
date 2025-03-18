using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Npc
{ 
    public class Lost : Npc
{
    public override void AfterBeAttack()
    {
            GetItem(1, 1);
    }

    public override void AfterBeRefresh()
    {
            ChangeText("她能透过药草的协助，诵念咒语与召唤神明来施法，冒犯她的人会变成动物，并创造出不存在的幻影，她可以藏住月亮与太阳让大地一片漆黑");
            InventoryManager.Instance.Gold += 10;
    }

    public override void OnAngry()
    {
        throw new System.NotImplementedException();
    }

    public override void OnDead()
    {
            GetItem(1, 1);
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

    protected override void Attack()
    {
        throw new System.NotImplementedException();
    }

        protected override void Init()
        {
            this.npcName = "流浪者";
            this.damage = 0;
            this.Health = 3;
            this.takeItems = new List<itemId>
            {
                new itemId()
                {
                    mount = 1,
                    id = 1
                },
                new itemId()
                {
                    mount = 1,
                    id = 2
                }
            };

        }
    }


}

