using System.Collections;
using System.Collections.Generic;
using Npc.State;
using UnityEngine;
namespace Npc
{
    public class LostChild : Npc 
    {
        protected override void Awake()
        {
            base.Awake();

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
            machine = new StateMachine();
            Sad sad = new Sad(machine, this);
            Happy happy = new Happy(machine, this);
            machine.Init(sad);

        }

        public override void OnDead()
        {
            GetItem(1, 2);
        }

        // Start is called before the first frame update
    }
}

