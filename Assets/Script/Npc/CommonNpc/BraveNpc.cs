using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Npc
{
    public class BraveNpc : Npc
    {

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            MapManager.Instance.character.currentHealth -= 3490;
            Debug.Log(MapManager.Instance.character.currentHealth);
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

        protected override void Init()
        {
            throw new System.NotImplementedException();
        }

        public override void AfterBeRefresh()
        {
            throw new System.NotImplementedException();
        }
    }
}


