using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Npc.State
{

    public class Sad : State
    {
        public Sad(StateMachine machine, Npc npc) : base(machine, npc)
        {
            
        }

        protected override State moreHappyState => throw new System.NotImplementedException();

        protected override State moreAngerState => throw new System.NotImplementedException();

        protected override State moreSadState => throw new System.NotImplementedException();


    }



}


