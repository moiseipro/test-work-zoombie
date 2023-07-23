using System;
using System.Security.Cryptography;
using Enemy;
using UnityEngine;

namespace Weapon
{
    public class BulletView : MonoBehaviour
    {
        [SerializeField] private float _bulletSpeed = 20f;
        private ParticleSystem _bulletParticle;
        private Transform _transform;
        
        private Vector3 _bulletTargetPosition;

        public Action OnDealDamage;

        private void Awake()
        {
            _transform = transform;
            _bulletParticle = GetComponentInChildren<ParticleSystem>();
        }
        
        private void Update()
        {
            _transform.position =
                Vector3.MoveTowards(_transform.position, _bulletTargetPosition, Time.deltaTime * _bulletSpeed);
            if (Vector3.Distance(_transform.position, _bulletTargetPosition) < 0.5f)
            {
                OnDealDamage?.Invoke();
                _bulletParticle.transform.SetParent(null);
                Destroy(gameObject);
            }
        }
        
        public void SetTargetPosition(Vector3 targetPosition)
        {
            _bulletTargetPosition = targetPosition;
        }
    }
}