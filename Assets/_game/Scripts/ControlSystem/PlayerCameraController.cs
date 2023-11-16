using System;
using UnityEngine;

namespace _game.Scripts.ControlSystem
{
    public class PlayerCameraController
    {
        private event Action<Transform> OnOrientationUpdate;

        private readonly Transform _camera;
        private readonly Transform _orientation;
        private readonly Vector2 _sensitivity;

        private float _yRotation;
        private float _xRotation;

        public PlayerCameraController(Transform camera, Transform orientation, Vector2 sensitivity)
        {
            _camera = camera;
            _orientation = orientation;
            _sensitivity = sensitivity;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void Update(float mouseX, float mouseY, float deltaTime)
        {
            mouseX = mouseX * deltaTime * _sensitivity.x;
            mouseY = mouseY * deltaTime * _sensitivity.y;

            _yRotation += mouseX;
            _xRotation -= mouseY;

            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

            _camera.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);

            _orientation.rotation = Quaternion.Euler(0, _yRotation, 0);
            OnOrientationUpdate?.Invoke(_orientation);
        }

        public void AddListenerToOnOrientationUpdate(Action<Transform> listener)
        {
            OnOrientationUpdate += listener;
        }

        public void RemoveListenerToOnOrientationUpdate(Action<Transform> listener)
        {
            OnOrientationUpdate -= listener;
        }
    }
}