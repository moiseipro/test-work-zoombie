using Characteristics;
using GameStates;
using Player;
using UnityEngine;
using VContainer;

namespace Weapon
{
    public class WeaponView : MonoBehaviour
    {
        [SerializeField] private WeaponStats _weaponStats;
        [SerializeField] private BulletView _bulletViewPrefab;
        
        private GameInput _gameInput;
        private PlayerAnimation _playerAnimation;

        [SerializeField] private Transform _weaponTransform;

        private IDamagable _damagable;
        private Vector3 _hitPoint;

        private bool _canShooting;
        
        [Inject]
        private void Constructor(GameInput gameInput, GameStateChanger gameStateChanger)
        {
            _gameInput = gameInput;

            gameStateChanger.OnGameStateChanged += GameStateChanged;
        }
        
        private void GameStateChanged(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Play:
                    SetPossibilityShooting(true);
                    break;
                default:
                    SetPossibilityShooting(false);
                    break;
            }
        }
        
        private void Awake()
        {
            _playerAnimation = GetComponentInChildren<PlayerAnimation>();
        }

        private void Shoot()
        {
            if (_damagable != null)
            {
                _damagable.TakeDamage(_weaponStats.damage);
                BulletView newBullet = Instantiate(_bulletViewPrefab, _weaponTransform.position, _weaponTransform.rotation);
                newBullet.SetTargetPosition(_hitPoint);
            }
        }

        private void Update()
        {
            if (_canShooting && _gameInput.Player.Shoot.IsPressed() && _weaponStats.CanShoot())
            {
                Shoot();
            }
        }

        private void FixedUpdate()
        {
            if (Physics.SphereCast(_weaponTransform.position, 2f, _weaponTransform.forward, out RaycastHit raycastHit,
                    _weaponStats.attackDistance, 1 << 6))
            {
                _playerAnimation.SetWeaponTarget(raycastHit.transform);
                if (raycastHit.transform.TryGetComponent(out IDamagable damagable))
                {
                    _damagable = damagable;
                    _hitPoint = raycastHit.point;
                    Debug.DrawLine(_weaponTransform.position, raycastHit.point, Color.blue, 1f);
                }
            }
            else
            {
                _playerAnimation.SetWeaponTarget(null);
                _damagable = null;
            }
        }

        private void SetPossibilityShooting(bool canShooting)
        {
            _canShooting = canShooting;
        }
    }
}