using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Npc
{
    public class Defector : Npc
    {
        public Defector() : base("ϣ����", new List<itemId>(),300, 20,122,400,10)
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
            MapEventManager.Instance.eventUi.EventDescription.text = "�������������������Ҳû��ħ���������˶�����ƽ�ȵĻ���ȥ";
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
            MapEventManager.Instance.eventUi.EventDescription.text = "������������ħ�����߹�";
        }
    }
}

