using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Npc
{
    public class Defector : Npc
    {
        public Defector() : base("希律王", new List<itemId>(),300, 20,122,400,10)
        {
            
        }
        protected override void Attack()
        {
            MapManager.Instance.character.TakeDamage(2111);
        }
        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            OnSad();
            Attack();
        }
        
        public override void OnAngry()
        {
            throw new System.NotImplementedException();
        }
        public override void OnDead()
        {
            MapEventManager.Instance.eventUi.EventDescription.text = "我曾幻想过，世界上再也没有魔法，所有人都可以平等的活下去";
            InventoryManager.Instance.Gold += 293;
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
            MapEventManager.Instance.eventUi.EventDescription.text = "还是下手了吗，魔法的走狗";
        }
    }
}

