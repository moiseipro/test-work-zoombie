using System;
using Enemy;
using UnityEngine;
using UnityEngine.Pool;
using VContainer;

namespace LevelManagement
{
    public class EnemyPool : MonoBehaviour
    {
        private Func<Transform, EnemyAI> _factory;

        public EnemyAI EnemyAIPrefab;
        public enum PoolType
        {
            Stack,
            LinkedList
        }

        public PoolType poolType;

        public bool collectionChecks = true;
        public int maxPoolSize = 10;

        IObjectPool<EnemyAI> m_Pool;

        public IObjectPool<EnemyAI> Pool
        {
            get
            {
                if (m_Pool == null)
                {
                    if (poolType == PoolType.Stack)
                        m_Pool = new ObjectPool<EnemyAI>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, 10, maxPoolSize);
                    else
                        m_Pool = new LinkedPool<EnemyAI>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, maxPoolSize);
                }
                return m_Pool;
            }
        }
        
        [Inject]
        private void Constructor(Func<Transform, EnemyAI> factory)
        {
            _factory = factory;
        }

        EnemyAI CreatePooledItem()
        {
            Debug.Log("Create enemy");
            EnemyAI newEnemy = _factory.Invoke(transform);
            newEnemy.pool = Pool;

            return newEnemy;
        }

        void OnReturnedToPool(EnemyAI enemyAI)
        {
            enemyAI.gameObject.SetActive(false);
        }

        void OnTakeFromPool(EnemyAI enemyAI)
        {
            enemyAI.gameObject.SetActive(true);
            enemyAI.Initialize();
        }

        void OnDestroyPoolObject(EnemyAI enemyAI)
        {
            Destroy(enemyAI.gameObject);
        }
    }
}