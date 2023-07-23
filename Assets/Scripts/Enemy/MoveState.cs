using UnityEngine;

namespace Enemy
{
    public class MoveState : EnemyState
    {
        public MoveState(EnemyAI enemyAI) : base(enemyAI)
        {
            _enemyAI = enemyAI;
        }

        public override void Start()
        {
            _enemyAI.enemyAnimation.MoveAnimation();
        }

        public override void Tick()
        {
            if (_enemyAI.playerMovement != null)
            {
                _enemyAI.navMeshAgent.SetDestination(
                    _enemyAI.playerMovement.GetCurrentPosition()+_enemyAI.playerMovement.moveVector3);
                if (Vector3.Distance(_enemyAI.enemyTransform.position,_enemyAI.playerMovement.GetCurrentPosition()) <= _enemyAI.navMeshAgent.stoppingDistance)
                {
                    _enemyStateMachine.SetState(typeof(AttackState));
                }
            }
            _enemyAI.enemyAnimation.SetSpeed(_enemyAI.navMeshAgent.velocity.magnitude);
        }
    }
}