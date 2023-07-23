using System;
using System.Collections.Generic;
using System.Linq;

namespace Enemy
{
    public class EnemyStateMachine
    {
        protected EnemyState _currentEnemyState;

        protected Dictionary<Type, EnemyState> _enemyStates;

        public EnemyStateMachine(Dictionary<Type, EnemyState> enemyStates)
        {
            _enemyStates = enemyStates;
            foreach (var enemyState in _enemyStates)
            {
                enemyState.Value.InitStateMachine(this);
            }

            _currentEnemyState = _enemyStates.First().Value;
        }

        public void SetState(Type enemyStateClass)
        {
            _currentEnemyState = _enemyStates[enemyStateClass];
            _currentEnemyState.Start();
        }

        public void Event()
        {
            _currentEnemyState.Event();
        }

        public void Tick()
        {
            _currentEnemyState.Tick();
        }
    }
}