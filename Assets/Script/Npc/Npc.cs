using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
namespace Npc
{
    public abstract class Npc:MonoBehaviour
    {
        public string npcName;
       [SerializeField]protected int Health;
        protected float defeat;
        protected float loveDefeat;
        protected int damage;
        protected int loveHealth;
        protected List<itemId> takeItems = new List<itemId>();//携带物品
        public string description;
        public string imagePath;
        private void Awake()
        {
            Init();
        }
        public virtual void Init()
        {
            this.npcName = "maJie";
            this.Health = 100;
            this.defeat = 20;
        }
        protected abstract void Attack();
        public abstract void OnLove();
        public abstract void OnHappy();
        public abstract void OnSad();
        public abstract void OnDead();
        public abstract void OnAngry();
        public abstract void AfterBeAttack();
        public virtual void TakeDamage(int damage)
        {
            if(Health - damage*((100-defeat)/100) < 0)
            {
                OnDead();
                Debug.Log("dead");
                return;
            }
            AfterBeAttack();
        }
        public virtual void TakeLove(int loveValue)
        {
            if (loveHealth - loveValue * (1 - loveDefeat) < 0)
            {
                OnLove();
            }
        }
        public abstract void TakeRefresh(int value);
        public void GetItem(int id,int mount)//获取npc身上物品
        {
            itemId getItem = takeItems.Find(i => (i.id == id) && (i.mount >= mount));
            if (getItem!=null)
            {
                InventoryManager.Instance.AddItem(id, mount);
                if (getItem.mount - mount > 0)
                    getItem.mount -= mount;
                else if (getItem.mount - mount == 0)
                    takeItems.Remove(getItem);
            }
        }
    }
    
}
public enum npcType
{ 
    braNpc,
    intNpc,
    chiNpc,
    speNpc
}


