using System.Collections;
using System.Collections.Generic;
using Npc.State;
using UnityEngine;
namespace Npc
{
    public abstract class Npc : MonoBehaviour
    {
        protected StateMachine machine;
        [Range(0,999)]
        [SerializeField]private int Health;

        [SerializeField] private List<itemId> takeItems;//携带物品

        public Sprite sprite;
        protected virtual void Awake()
        {
            Init();
        }
        protected abstract void Init();
        public abstract void OnHappy();

        public abstract void OnSad();

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

