namespace Enemy
{
    public abstract class EnemyState
    {
        protected EnemyAI _enemyAI;
        protected EnemyStateMachine _enemyStateMachine;

        public EnemyState(EnemyAI enemyAI)
        {
            _enemyAI = enemyAI;
        }

        public void InitStateMachine(EnemyStateMachine enemyStateMachine)
        {
            _enemyStateMachine = enemyStateMachine;
        }

        public virtual void Start()
        {
            
        }
        
        public virtual void Event()
        {
            
        }

        public virtual void Tick()
        {
            
        }
    }
}