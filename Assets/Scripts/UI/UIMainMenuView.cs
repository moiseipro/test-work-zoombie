using GameStates;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace DefaultNamespace
{
    public class UIMainMenuView : UIMenuView
    {
        [SerializeField] private Button _startGameButton;
        
        [Inject]
        private void Constructor(GameStateChanger gameStateChanger)
        {
            _startGameButton.onClick.AddListener(gameStateChanger.StartGame);
            gameStateChanger.OnGameStateChanged += GameStateChanged;
        }

        private void GameStateChanged(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Play:
                    Hide();
                    break;
            }
        }
    }
}