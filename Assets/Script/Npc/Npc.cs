using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Npc.State;
using UnityEngine;
namespace Npc
{
    public abstract class Npc
    {
        public string npcName;

        protected StateMachine machine;
        [Range(0,999)]
        protected int Health;
        [Range(0,0.9f)]
        protected float defeat;
        protected float loveDefeat;
        protected int damage;
        protected int loveHealth;
        protected List<itemId> takeItems = new List<itemId>();//携带物品
        public string description;
        public string imagePath;
        public Npc(string name,List<itemId> items,int damage,float defeat, int loveHealth, int Health, float loveDefeat)
        {
            this.npcName = name;
            this.takeItems = items;
            this.Health = Health;
            this.loveDefeat = loveDefeat;
            this.loveHealth = loveHealth;
            this.damage = damage;
            this.defeat = defeat;
            Init();
        }
        protected virtual void Init()
        {
            machine = new StateMachine();
            Sad sad = new Sad(machine, this);
            Happy happy = new Happy(machine, this);
            machine.Init(sad);
        }
        public abstract void OnLove();
        public abstract void OnHappy();
        public abstract void OnSad();
        public abstract void OnDead();
        public abstract void OnAngry();
        public virtual void TakeDamage(int damage)
        {
            if(Health - damage*(1-defeat) < 0)
            {
                OnDead();
            }
        }
        public virtual void TakeLove(int loveValue)
        {
            if (loveHealth - loveValue * (1 - loveDefeat) < 0)
            {
                OnLove();
            }
        }
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


