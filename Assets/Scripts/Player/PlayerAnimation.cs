using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimation : MonoBehaviour
    {
        private Animator _animator;
        
        [SerializeField] private Transform _weaponTrack;
        private Transform _weaponTarget;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void MoveAnimation(Vector2 moveVector)
        {
            _animator.SetFloat("horizontal", moveVector.x);
            _animator.SetFloat("vertical", moveVector.y);
        }

        public void SetWeaponTarget(Transform weaponTarget)
        {
            _weaponTarget = weaponTarget;
        }

        private void Update()
        {
            if (_weaponTarget != null)
            {
                _weaponTrack.position = Vector3.Lerp(_weaponTrack.position, _weaponTarget.position, 10f * Time.deltaTime);
            }
        }
    }
}