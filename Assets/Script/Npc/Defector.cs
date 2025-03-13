using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Npc
{
    public class Defector : Npc
    {
        public Defector() : base("Ï£ÂÉÍõ", new List<itemId>(),300, 20,122,400,10)
        {
            
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
    }
}

