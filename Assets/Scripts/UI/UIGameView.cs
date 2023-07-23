using GameStates;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace DefaultNamespace
{
    public class UIGameView : UIMenuView
    {
        [SerializeField] private Text _waveText;
        [SerializeField] private Text _enemyText;
        
        [Inject]
        private void Constructor(GameStateChanger gameStateChanger, GameData gameData)
        {
            gameStateChanger.OnGameStateChanged += GameStateChanged;
            gameData.OnWaveChanged += WaveChanged;
            gameData.OnEnemyKilled += EnemyChanged;
        }

        private void GameStateChanged(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Play:
                    Show();
                    break;
               default:
                    Hide();
                    break;
            }
        }

        private void WaveChanged(int currentWave)
        {
            _waveText.text = currentWave.ToString();
        }

        private void EnemyChanged(int currentEnemyKilled, int enemyInWave)
        {
            _enemyText.text = enemyInWave + " / " + currentEnemyKilled;
        }
        
        private void Awake()
        {
            Hide();
        }
    }
}