using System.Collections.Generic;
using UnityEngine;
namespace Npc
{
    public class LostChild : Npc
    {

        public override void OnHappy()
        {
            GetItem(1, 2);
        }
        public override void OnSad()
        {
            Debug.Log("UnHappy");
        }
        public override void OnDead()
        {
            MapEventManager.Instance.eventUi.EventDescription.text = "我想要一位真正的神";
            GetItem(1, 2);
        }
        public override void OnAngry()
        {
            throw new System.NotImplementedException();
        }
        public override void OnLove()
        {
            throw new System.NotImplementedException();
        }

        protected override void Attack()
        {
            throw new System.NotImplementedException();
        }

        public override void TakeRefresh(int value)
        {
            throw new System.NotImplementedException();
        }

        public override void AfterBeAttack()
        {
            throw new System.NotImplementedException();
        }
    }
}

