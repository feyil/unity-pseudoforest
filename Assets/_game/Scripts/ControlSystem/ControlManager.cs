using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _game.Scripts.ControlSystem
{
    public class ControlManager : MonoBehaviour
    {
        [Title("PlayerControl")] [SerializeField]
        private Transform m_player;

        [SerializeField] private Transform m_orientation;
        [SerializeField] private float m_moveSpeed;

        [Title("CameraControl")] [SerializeField]
        private Transform m_cameraHolder;

        [SerializeField] private Transform m_camera;
        [SerializeField] private Transform m_cameraTarget;
        [SerializeField] private Vector2 m_sensitivity;

        private bool _isInitialized;
        private PlayerMovementController _playerMovementController;
        private PlayerCameraController _playerCameraController;

        public void Initialize()
        {
            _isInitialized = true;

            _playerMovementController = new PlayerMovementController(m_player, m_orientation, m_moveSpeed);
            _playerCameraController = new PlayerCameraController(m_camera, m_orientation, m_sensitivity);
        }

        private void Update()
        {
            if (!_isInitialized) return;

            var deltaTime = Time.deltaTime;

            var horizontalInput = Input.GetAxisRaw("Horizontal");
            var verticalInput = Input.GetAxisRaw("Vertical");

            _playerMovementController.Update(horizontalInput, verticalInput, deltaTime);

            var mouseX = Input.GetAxisRaw("Mouse X");
            var mouseY = Input.GetAxisRaw("Mouse Y");

            _playerCameraController.Update(mouseX, mouseY, deltaTime);

            // Sync cam position with the target
            m_cameraHolder.position = m_cameraTarget.position;
        }

        public PlayerMovementController GetPlayerMovementController()
        {
            return _playerMovementController;
        }
    }
}