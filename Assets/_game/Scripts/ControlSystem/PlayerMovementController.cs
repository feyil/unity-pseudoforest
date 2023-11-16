using System;
using UnityEngine;

namespace _game.Scripts.ControlSystem
{
    public class PlayerMovementController
    {
        private event Action<Vector3> OnPlayerPositionUpdate;

        private readonly Transform _player;
        private readonly Transform _orientation;

        private readonly float _moveSpeed;

        public PlayerMovementController(Transform player, Transform orientation, float moveSpeed)
        {
            _player = player;
            _moveSpeed = moveSpeed;
            _orientation = orientation;
        }

        public void Update(float horizontalInput, float verticalInput, float deltaTime)
        {
            var moveDirection = _orientation.forward * verticalInput + _orientation.right * horizontalInput;
            var updatedPosition = _player.position + moveDirection * (_moveSpeed * deltaTime);
            _player.position = updatedPosition;
            OnPlayerPositionUpdate?.Invoke(updatedPosition);
        }

        public void AddListenerToPlayerPositionUpdate(Action<Vector3> listener)
        {
            OnPlayerPositionUpdate += listener;
        }

        public void RemoveListenerToPlayerPositionUpdate(Action<Vector3> listener)
        {
            OnPlayerPositionUpdate -= listener;
        }
    }
}