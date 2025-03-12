using System.Collections.Generic;
using Npc.State;
using UnityEngine;
namespace Npc
{
    public class LostChild : Npc
    {
        public LostChild(string name,List<itemId>items) : base(name,items,1,1)
        {
            
        }
        public override void OnHappy()
        {
            GetItem(1, 2);
        }
        public override void OnSad()
        {
            Debug.Log("UnHappy");
        }
        protected override void Init()
        {

        }
        public override void OnDead()
        {
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
    }
}

