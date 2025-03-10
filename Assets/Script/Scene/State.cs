using UnityEngine;
namespace MainGame
{
    public class State : SingleTon<State>,IDestroySelf
    {
        public GameState currentState = GameState.Map;

        public void DestroySelf()
        {
            Destroy(gameObject);
        }

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

