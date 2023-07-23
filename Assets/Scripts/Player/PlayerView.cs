using System;
using Characteristics;
using GameStates;
using UnityEngine;
using VContainer;
using Weapon;

namespace Player
{
    public class PlayerView : MonoBehaviour, IDamagable
    {
        
        [SerializeField] private HealthStats _healthStats;

        [Inject]
        private void Constructor(GameStateChanger gameStateChanger)
        {
            _healthStats.OnDeath += gameStateChanger.GameOver;
            gameStateChanger.OnGameStateChanged += GameStateChanged;
        }
        
        private void GameStateChanged(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Play:
                    _healthStats.RestoreHealth();
                    break;
            }
        }
        
        public void TakeDamage(int damage)
        {
            _healthStats.TakeDamage(damage);
        }
    }
}