using System;
using UnityEngine;
using VContainer;

namespace GameStates
{
    [Serializable]
    public class GameData
    {
        [SerializeField] private int _numberWaves = 3;
        public int numberWaves => _numberWaves;
        [SerializeField] private int _enemiesInWave = 10;
        public int enemiesInWave => _enemiesInWave;
        [SerializeField] private float _baseSpawnTime = 5f;
        public float baseSpawnTime => _baseSpawnTime;
        private int _currentWave;
        public int currentWave => _currentWave;
        private int _currentEnemiesSpawned;
        public int currentEnemiesSpawned => _currentEnemiesSpawned;
        private int _currentEnemiesKilled;
        public int currentEnemiesKilled => _currentEnemiesKilled;

        private GameStateChanger _gameStateChanger;

        public Action<int> OnWaveChanged;
        public Action<int, int> OnEnemyKilled;
        
        [Inject]
        private void Constructor(GameStateChanger gameStateChanger)
        {
            _gameStateChanger = gameStateChanger;
            _gameStateChanger.OnGameStateChanged += GameStateChanged;
        }
        
        private void GameStateChanged(GameState gameState)
        {
            if (gameState == GameState.Win || gameState == GameState.Lose)
            {
                ClearGameData();
            }
            else
            {
                UpdateEvents();
            }
        }
        
        private void NextWave()
        {
            _currentWave = Mathf.Clamp(_currentWave + 1, 0, _numberWaves);
            _currentEnemiesKilled = 0;
            _currentEnemiesSpawned = 0;
            if (_currentWave == _numberWaves)
            {
                _gameStateChanger.GameWin();
            }
            else
            {
                _gameStateChanger.ChangeWave();
            }
            UpdateEvents();
        }

        public void IncreaseSpawnedEnemies()
        {
            _currentEnemiesSpawned = Mathf.Clamp(_currentEnemiesSpawned + 1, 0, _enemiesInWave);
        }
        public void IncreaseKilledEnemies()
        {
            _currentEnemiesKilled = Mathf.Clamp(_currentEnemiesKilled + 1, 0, _enemiesInWave);
            OnEnemyKilled?.Invoke(_currentEnemiesKilled, _enemiesInWave);
            if (_currentEnemiesSpawned == _currentEnemiesKilled)
            {
                NextWave();
            }
        }

        private void ClearGameData()
        {
            _currentEnemiesSpawned = 0;
            _currentWave = 0;
            _currentEnemiesKilled = 0;
            UpdateEvents();
        }

        private void UpdateEvents()
        {
            OnEnemyKilled?.Invoke(_currentEnemiesKilled, _enemiesInWave);
            OnWaveChanged?.Invoke(_currentWave);
        }
    }
}