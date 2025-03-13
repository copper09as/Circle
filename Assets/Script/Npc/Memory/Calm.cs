using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace Npc.State
{
    public class Calm : State
    {
        public Calm(StateMachine machine, Npc npc) : base(machine, npc)
        {

        }

        protected override State moreHappyState => throw new System.NotImplementedException();

        protected override State moreAngerState => throw new System.NotImplementedException();

        protected override State moreSadState => throw new System.NotImplementedException();

        public override void OnEnter()
        {
            base.OnEnter();
        }
        public override void OnExit()
        {

        }
        public override void Trig()
        {
            npc.OnHappy();
        }
    }



}


