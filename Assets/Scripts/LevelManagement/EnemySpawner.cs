using System.Collections;
using Enemy;
using GameStates;
using Player;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

namespace LevelManagement
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovementTarget;
        [SerializeField] private EnemyPool _enemyPool;

        private GameData _gameData;

        [SerializeField] private Transform[] _spawnPoints;

        private Coroutine _coroutine;
        
        [Inject]
        private void Constructor(GameStateChanger gameStateChanger, GameData gameData)
        {
            gameStateChanger.OnGameStateChanged += GameStateChanged;
            _gameData = gameData;
        }

        private void GameStateChanged(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Play:
                    StartSpawnEnemy();
                    break;
                default:
                    StopSpawnEnemy();
                    break;
            }
        }

        private void StartSpawnEnemy()
        {
            Debug.Log("Start spawn enemies");
            _coroutine = StartCoroutine(SpawnEnemy());
        }

        private void StopSpawnEnemy()
        {
            StopCoroutine(_coroutine);
        }
        
        private IEnumerator SpawnEnemy()
        {
            while (_gameData.currentEnemiesSpawned<_gameData.enemiesInWave)
            {
                yield return new WaitForSeconds(_gameData.baseSpawnTime*_gameData.currentEnemiesSpawned/_gameData.enemiesInWave);
                Debug.Log("Enemy spawned!");
                EnemyAI newEnemyAI = _enemyPool.Pool.Get();
                newEnemyAI.SetPlayerTarget(_playerMovementTarget);
                newEnemyAI.SetPosition(_spawnPoints[Random.Range(0, _spawnPoints.Length)].position);
                _gameData.IncreaseSpawnedEnemies();
            }
            
        }
        
    }
}