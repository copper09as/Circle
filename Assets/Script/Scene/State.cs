using UnityEngine;
namespace MainGame
{
    public class State : SingleTon<State>
    {
        public GameState currentState = GameState.Map;
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public enum GameState
    {
        Map,
        Bag,
        Event,
        Shop
    }
}

