using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Npc
{
    public class Wizza : Npc
    {
        public override void AfterBeAttack()
        {
            throw new System.NotImplementedException();
        }
        protected override void Init()
        {
            this.npcName = "巫师";
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
        public override void AfterBeRefresh()
        {
            ChangeText("巫师越是临近死亡，魔法便愈加强大，如你所见，我的时日已经不多了");
            GetItem(1, 1);
        }

        public override void OnAngry()
        {
            throw new System.NotImplementedException();
        }

        public override void OnDead()
        {
            ChangeText("......");
            GetItem(2, 1);
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
    }


}


