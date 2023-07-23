using System;
using Characteristics;
using GameStates;
using Unity.VisualScripting;
using UnityEngine;
using VContainer;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private MovementStats _movementStats;
        
        private Transform _transform;
        private CharacterController _characterController;
        private Camera _camera;
        private GameInput _gameInput;
        private PlayerAnimation _playerAnimation;

        private Vector2 _currentInputVector;
        private Vector2 _inputVelocity;

        private Vector3 _gravityVector3;
        private Vector3 _moveVector3;
        public Vector3 moveVector3 => _moveVector3;

        private bool _canMove;

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
                    StartMovement();
                    break;
                default:
                    StopMovement();
                    break;
            }
        }
        
        private void Awake()
        {
            _transform = transform;
            _characterController = GetComponent<CharacterController>();
            _camera = Camera.main;
            _playerAnimation = GetComponentInChildren<PlayerAnimation>();
        }

        private void Update()
        {
            
            Gravity();
            if (_canMove)
            {
                Move();
                Rotation();
            }
            
        }

        private void Gravity()
        {
            if (_characterController.isGrounded && _characterController.velocity.y < 0)
            {
                _gravityVector3.y = 0;
            }

            _gravityVector3.y -= 9.8f * Time.deltaTime;
            _characterController.Move(_gravityVector3);
        }
        
        private void Move()
        {
            Vector2 inputVector2 = _gameInput.Player.Move.ReadValue<Vector2>();
            _currentInputVector = Vector2.SmoothDamp(
                _currentInputVector, 
                inputVector2, 
                ref _inputVelocity, 
                _movementStats.accelerationFactor);
            _playerAnimation.MoveAnimation(_currentInputVector);
            _moveVector3 = _transform.TransformDirection(new Vector3(_currentInputVector.x, 0, _currentInputVector.y));
            _characterController.Move(_moveVector3 * (_movementStats.speed * Time.deltaTime));
        }

        private void Rotation()
        {
            Vector3 rotationVector3 = _camera.transform.forward;
            rotationVector3.y = 0;
            rotationVector3.Normalize();
            
            Quaternion newRotation = Quaternion.Lerp(
                _transform.rotation, Quaternion.LookRotation(rotationVector3), 
                _movementStats.rotationSpeed * Time.deltaTime);

            _transform.rotation = newRotation;
        }

        public Vector3 GetCurrentPosition()
        {
            return _transform.position;
        }

        private void StopMovement()
        {
            _canMove = false;
        }

        private void StartMovement()
        {
            _canMove = true;
        }
    }
}