namespace Npc.State
{
    public abstract class State
    {
        protected abstract State moreHappyState { get; }
        protected abstract State moreAngerState { get; }
        protected abstract State moreSadState { get; }

        protected readonly StateMachine machine;
        protected readonly Npc npc;
        public State(StateMachine machine, Npc npc)
        {
            this.machine = machine;
            this.npc = npc;
        }
        public virtual void OnEnter()
        {
            machine.currentState = this;
        }
        public virtual void OnExit()
        {

        }
        public virtual void Trig()
        {

        }


    }

}
