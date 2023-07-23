using System;
using UnityEngine;

namespace Characteristics
{
    [Serializable]
    public class MovementStats
    {
        [SerializeField] private float _speed;
        public float speed => _speed;
        [SerializeField] private float _maxSpeed;
        public float maxSpeed => _maxSpeed;
        [SerializeField] private float _rotationSpeed;
        public float rotationSpeed => _rotationSpeed;
        [SerializeField] private float _accelerationFactor;
        public float accelerationFactor => _accelerationFactor;
        

        public MovementStats(float speed, float maxSpeed)
        {
            _speed = speed;
            _maxSpeed = maxSpeed;
        }
    }
}