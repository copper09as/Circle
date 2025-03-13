using UnityEngine;
namespace MainGame
{
    public class State : SingleTon<State>,IDestroySelf
    {
        public GameState currentState = GameState.Map;
        [SerializeField] private TitleUi titleUi;

        public void DestroySelf()
        {
            Destroy(gameObject);
        }

        private void Awake()
        {
            if(titleUi!=null)
            titleUi.importantManager.Add(this);
            DontDestroyOnLoad(gameObject);
        }
    }
    public enum GameState
    {
        Map,
        Bag,
        Event,
        Shop,
        People,
        Inn
    }
}

