using System;
using UnityEngine;

namespace Characteristics
{
    [Serializable]
    public class WeaponStats
    {
        [SerializeField] private int _damage;
        public int damage => _damage;
        [SerializeField] private float _attackDistance;
        public float attackDistance => _attackDistance;
        [SerializeField] private float _rechargeRate;
        public float rechargeRate => _rechargeRate;
        private float _currentRechargeTime;

        public bool CanShoot()
        {
            if (_currentRechargeTime < Time.time)
            {
                _currentRechargeTime = Time.time + _rechargeRate;
                return true;
            }

            return false;
        }
    }
}