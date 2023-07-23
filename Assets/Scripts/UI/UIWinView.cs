using GameStates;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace DefaultNamespace
{
    public class UIWinView : UIMenuView
    {
        [SerializeField] private Button _newGameButton;
        
        [Inject]
        private void Constructor(GameStateChanger gameStateChanger)
        {
            _newGameButton.onClick.AddListener(gameStateChanger.StartGame);
            gameStateChanger.OnGameStateChanged += GameStateChanged;
        }

        private void GameStateChanged(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Play:
                    Hide();
                    break;
                case GameState.Win:
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