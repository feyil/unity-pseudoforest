using System.Collections.Generic;
using _game.Scripts.Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _game.Scripts
{
    public class WorldStreamer : MonoBehaviour
    {
        [SerializeField] private Transform m_envSectionPrefab;
        [SerializeField] private Vector2 m_sectionSizeXZ;
        [SerializeField] private Vector2 m_scanStepXZ;

        [SerializeField] private Vector2 m_fovX;
        [SerializeField] private Vector2 m_fovZ;

        private float _playerZMovement;
        private Vector3 _currentEnvironmentPos;
        private Vector3 _initialPlayerPos;

        private Vector3Int _gridPosition;

        private List<Transform> _envPool;
        private Dictionary<Vector3Int, Transform> _envDict;

        [Button]
        public void Initialize()
        {
            ClearEnv();

            _envDict = new Dictionary<Vector3Int, Transform>();
            _envPool = new List<Transform>();

            var controlManager = GameManager.Instance.GetControlManager();

            var playerMovementController = controlManager.GetPlayerMovementController();
            playerMovementController.RemoveListenerToPlayerPositionUpdate(OnPlayerPositionUpdate);
            playerMovementController.AddListenerToPlayerPositionUpdate(OnPlayerPositionUpdate);

            var playerCameraController = controlManager.GetPlayerCameraController();
            playerCameraController.RemoveListenerToOnOrientationUpdate(OnOrientationUpdate);
            playerCameraController.AddListenerToOnOrientationUpdate(OnOrientationUpdate);
        }

        private void ClearEnv()
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                Destroy(child.gameObject);
            }
        }

        private void OnOrientationUpdate(Transform playerOrientation)
        {
            _envDict.Clear();

            var poolObjectCount = _envPool.Count;
            var poolObjectIndex = 0;

            for (var z = m_fovZ.x; z <= m_fovZ.y; z += m_scanStepXZ.y)
            {
                for (var x = m_fovX.x; x <= m_fovX.y; x += m_scanStepXZ.x)
                {
                    var envPoint = playerOrientation.TransformPoint(Vector3.right * x + Vector3.forward * z);
                    var gridPos = GetGridPosition(envPoint);

                    if (_envDict.ContainsKey(gridPos)) continue;

                    if (poolObjectIndex < poolObjectCount)
                    {
                        var env = _envPool[poolObjectIndex];
                        env.position = gridPos;
                        env.gameObject.SetActive(true);

                        _envDict[gridPos] = env;
                        poolObjectIndex++;
                    }
                    else
                    {
                        var env = Instantiate(m_envSectionPrefab, gridPos, Quaternion.identity, transform);
                        _envPool.Add(env);
                        _envDict[gridPos] = env;
                    }
                }
            }

            if (poolObjectIndex < poolObjectCount)
            {
                for (var i = poolObjectIndex; i < poolObjectCount; i++)
                {
                    var poolObject = _envPool[i];
                    poolObject.gameObject.SetActive(false);
                }
            }
        }

        private void OnPlayerPositionUpdate(Vector3 position)
        {
        }

        private Vector3Int GetGridPosition(Vector3 worldPosition)
        {
            var remainderX = worldPosition.x % m_sectionSizeXZ.x;
            var remainderZ = worldPosition.z % m_sectionSizeXZ.y;

            var gridX = (int)(worldPosition.x - remainderX);
            var gridZ = (int)(worldPosition.z - remainderZ);

            return new Vector3Int(gridX, 0, gridZ);
        }
    }
}