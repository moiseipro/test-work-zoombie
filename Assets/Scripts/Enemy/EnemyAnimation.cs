using System;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimation : MonoBehaviour
    {
        private Animator _animator;

        public Action OnAnimationEvent;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void DeathAnimation()
        {
            _animator.SetTrigger("death");
        }

        public void AttackAnimation()
        {
            _animator.ResetTrigger("move");
            _animator.SetTrigger("attack");
        }

        public void MoveAnimation()
        {
            _animator.SetTrigger("move");
        }

        public void SetSpeed(float speed)
        {
            _animator.SetFloat("speed", speed);
        }

        public void TriggerEvent()
        {
            OnAnimationEvent?.Invoke();
        }
    }
}