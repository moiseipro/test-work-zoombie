using System;
using UnityEngine;

namespace Characteristics
{
    [Serializable]
    public class HealthStats
    {
        [SerializeField] private int _health;
        public float health => _health;
        [SerializeField] private int _maxHealth;
        public float maxHealth => _maxHealth;

        public bool isDeath => _health == 0;
        
        public Action OnDeath;

        public HealthStats(int maxHealth)
        {
            _maxHealth = maxHealth;
            _health = _maxHealth;
        }

        public void TakeDamage(int damage)
        {
            if (_health == 0) return;
            _health = Mathf.Clamp(_health - damage, 0, _maxHealth);
            Debug.Log("Hit: "+damage);
            if (_health == 0)
            {
                OnDeath?.Invoke();
                Debug.Log("Death!");
            }
        }

        public void RestoreHealth()
        {
            _health = _maxHealth;
        }
    }
}