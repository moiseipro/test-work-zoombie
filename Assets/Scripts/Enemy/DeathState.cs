using UnityEngine;

namespace Enemy
{
    public class DeathState : EnemyState
    {
        public DeathState(EnemyAI enemyAI) : base(enemyAI)
        {
            _enemyAI = enemyAI;
        }

        public override void Start()
        {
            _enemyAI.enemyAnimation.DeathAnimation();
            _enemyAI.navMeshAgent.ResetPath();
            Debug.Log("Death!");
        }
        
        public override void Event()
        {
            _enemyAI.Death();
        }
    }
}