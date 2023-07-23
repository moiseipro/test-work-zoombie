using System;
using System.Collections.Generic;
using Characteristics;
using GameStates;
using Player;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;
using VContainer;

namespace Enemy
{
    public class EnemyAI : MonoBehaviour, IDamagable
    {
        public IObjectPool<EnemyAI> pool;
        
        [SerializeField] private HealthStats _healthStats;
        
        private Transform _enemyTransform;
        public Transform enemyTransform => _enemyTransform;

        private NavMeshAgent _navMeshAgent;
        public NavMeshAgent navMeshAgent => _navMeshAgent;

        [SerializeField] private PlayerMovement _playerMovement;
        public PlayerMovement playerMovement => _playerMovement;
        private EnemyAnimation _enemyAnimation;
        public EnemyAnimation enemyAnimation => _enemyAnimation;

        private EnemyStateMachine _enemyStateMachine;

        private GameData _gameData;
        
        [Inject]
        private void Constructor(GameData gameData, GameStateChanger gameStateChanger)
        {
            _gameData = gameData;
            gameStateChanger.OnGameStateChanged += GameStateChanged;
        }
        
        private void GameStateChanged(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Win or GameState.Lose:
                    FastKill();
                    break;
            }
        }
        
        private void Awake()
        {
            _enemyTransform = transform;
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _enemyAnimation = GetComponentInChildren<EnemyAnimation>();
            Dictionary<Type, EnemyState> enemyStates = new Dictionary<Type, EnemyState>()
            {
                {typeof(MoveState), new MoveState(this)},
                {typeof(AttackState), new AttackState(this)},
                {typeof(DeathState), new DeathState(this)}
            };
            _enemyStateMachine = new EnemyStateMachine(enemyStates);
        }

        private void Update()
        {
            _enemyStateMachine.Tick();
        }

        public void SetPlayerTarget(PlayerMovement playerMovement)
        {
            _playerMovement = playerMovement;
        }

        public void SetPosition(Vector3 newPosition)
        {
            _enemyTransform.position = newPosition;
        }
        
        public void Initialize()
        {
            _enemyStateMachine.SetState(typeof(MoveState));
            _healthStats.OnDeath += OnKilled;
            _enemyAnimation.OnAnimationEvent += _enemyStateMachine.Event;
            _healthStats.RestoreHealth();
        }

        public void TakeDamage(int damage)
        {
            _healthStats.TakeDamage(damage);
        }

        private void OnKilled()
        {
            if (gameObject.activeSelf)
            {
                _gameData.IncreaseKilledEnemies();
                _enemyStateMachine.SetState(typeof(DeathState));
            }
        }
        
        private void FastKill()
        {
            if (gameObject.activeSelf)
            {
                _enemyStateMachine.SetState(typeof(DeathState));
            }
        }

        public void Death()
        {
            _healthStats.OnDeath -= OnKilled;
            pool.Release(this);
        }

        private void OnDisable()
        {
            _enemyAnimation.OnAnimationEvent -= _enemyStateMachine.Event;
        }
    }
}