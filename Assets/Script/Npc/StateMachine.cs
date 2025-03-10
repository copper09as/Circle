using UnityEngine;

namespace Npc.State
{
    public class StateMachine : MonoBehaviour
    {
        public State currentState;
        public State PreState;
        public void TransState(State setState)
        {
            if (currentState != null)
                currentState.OnExit();
            setState.OnEnter();
        }
        public void SetState(State setState)
        {
            currentState = setState;
        }
        public void Init(State currentState)
        {
            this.currentState = currentState;
        }
    }
    public enum Memory
    {
        Happy,
        Sad,
        Angle,
        Calm

    }
}
