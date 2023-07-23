using Player;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class AttackState : EnemyState
    {
        public AttackState(EnemyAI enemyAI) : base(enemyAI)
        {
            _enemyAI = enemyAI;
        }

        public override void Start()
        {
            _enemyAI.enemyAnimation.AttackAnimation();
        }

        public override void Event()
        {
            if (Physics.SphereCast(
                    _enemyAI.enemyTransform.position + _enemyAI.transform.up - _enemyAI.transform.forward,
                    1f,
                    _enemyAI.enemyTransform.forward, 
                    out RaycastHit hitInfo, 
                    _enemyAI.navMeshAgent.stoppingDistance, 
                    1 << 7))
            {
                if (hitInfo.transform.TryGetComponent(out PlayerView playerView))
                {
                    playerView.TakeDamage(1);
                }
            }
        }

        public override void Tick()
        {
            if (Vector3.Distance(_enemyAI.enemyTransform.position,_enemyAI.playerMovement.GetCurrentPosition()) > _enemyAI.navMeshAgent.stoppingDistance)
            {
                _enemyStateMachine.SetState(typeof(MoveState));
            }
        }
    }
}