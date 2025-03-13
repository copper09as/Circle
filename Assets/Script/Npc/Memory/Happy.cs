namespace Npc.State
{
    public class Happy : State
    {

        public Happy(StateMachine machine, Npc npc) : base(machine, npc)
        {

        }

        protected override State moreHappyState => throw new System.NotImplementedException();

        protected override State moreSadState => throw new System.NotImplementedException();

        protected override State moreAngerState => throw new System.NotImplementedException();

        public override void OnEnter()
        {
            base.OnEnter();
        }
        public override void OnExit()
        {
            base.OnExit();
        }
        public override void Trig()
        {
            npc.OnHappy();
        }
    }
}

