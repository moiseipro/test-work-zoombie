using GameStates;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace DefaultNamespace
{
    public class UIGameOverView : UIMenuView
    {
        [SerializeField] private Button _restartGameButton;
        
        [Inject]
        private void Constructor(GameStateChanger gameStateChanger)
        {
            _restartGameButton.onClick.AddListener(gameStateChanger.StartGame);
            gameStateChanger.OnGameStateChanged += GameStateChanged;
        }

        private void GameStateChanged(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Play:
                    Hide();
                    break;
                case GameState.Lose:
                    Show();
                    break;
            }
        }
        
        private void Awake()
        {
            Hide();
        }
    }
}