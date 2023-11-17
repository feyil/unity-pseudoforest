using System.Collections.Generic;
using System.Linq;
using _game.Scripts.Core;
using UnityEngine;

namespace _game.Scripts
{
    public class Experiment : MonoBehaviour
    {
        [SerializeField] private Transform m_player;
        [SerializeField] private Transform m_sectionPrefab;

        private float _playerZMovement;
        private Vector3 _currentEnvironmentPos;
        private Vector3 _initialPlayerPos;

        private Vector3Int _gridPosition;

        private Dictionary<Vector3Int, Transform> _envDict;

        private void Start()
        {
            _envDict = new Dictionary<Vector3Int, Transform>();
            var controlManager = GameManager.Instance.GetControlManager();

            var playerMovementController = controlManager.GetPlayerMovementController();
            playerMovementController.AddListenerToPlayerPositionUpdate(OnPlayerPositionUpdate);

            var playerCameraController = controlManager.GetPlayerCameraController();
            playerCameraController.AddListenerToOnOrientationUpdate(OnOrientationUpdate);


            // Test(0, 0);
            // Test(0, 10);
            // Test(0, 20);
            // Test(0, 30);
            // Test(0, 40);
            // Test(0, -10);
            // Test(0, -20);
            // Test(0, -30);
            // Test(0, -40);
        }

        private void OnOrientationUpdate(Transform playerOrientation)
        {
            _envDict.Clear();

            var childCount = transform.childCount;
            var count = 0;
            for (var z = -10; z <= 80; z += 5)
            {
                for (var x = -20; x <= 20; x += 5)
                {
                    var envPoint = playerOrientation.TransformPoint(Vector3.right * x + Vector3.forward * z);
                    var gridPos = GetGridPosition(envPoint);

                    if (_envDict.ContainsKey(gridPos)) continue;

                    if (count < childCount)
                    {
                        var env = transform.GetChild(count);
                        env.position = gridPos;
                        env.gameObject.SetActive(true);

                        _envDict[gridPos] = env;
                        count++;
                    }
                    else
                    {
                        var env = Instantiate(m_sectionPrefab, gridPos, Quaternion.identity, transform);
                        _envDict[gridPos] = env;
                    }
                }
            }

            if (count < childCount)
            {
                for (var i = count; i < childCount; i++)
                {
                    var child = transform.GetChild(i);
                    child.gameObject.SetActive(false);
                }
            }
        }

        bool IsPointInsideRectangle(float x1, float y1, float x2, float y2, float px, float py)
        {
            return px >= x1 && px <= x2 && py <= y1 && py >= y2;
        }

        private void OnPlayerPositionUpdate(Vector3 position)
        {
            // var gridPosition = GetGridPosition(position);
            //
            // if (gridPosition.x != _gridPosition.x ||
            //     gridPosition.z != _gridPosition.z)
            // {
            //     OnGridUpdate();
            // }
            //
            // _gridPosition = gridPosition;
        }

        private Vector3Int GetGridPosition(Vector3 worldPosition)
        {
            var gridXDimension = 10;
            var gridZDimension = 10;

            var remainderX = worldPosition.x % gridXDimension;
            var remainderZ = worldPosition.z % gridZDimension;

            var gridX = (int)(worldPosition.x - remainderX);
            var gridZ = (int)(worldPosition.z - remainderZ);

            return new Vector3Int(gridX, 0, gridZ);
        }


        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                Test(0, 0);
                Test(0, 10);
                Test(0, 20);
                Test(0, 30);
                Test(0, 40);
                Test(0, -10);
                Test(0, -20);
                Test(0, -30);
                Test(0, -40);
            }
        }

        private void OnGridUpdate()
        {
            // for (var i = 0; i < transform.childCount; i++)
            // {
            //     Destroy(transform.GetChild(i).gameObject);
            // }
            //
            // Test(0, 0);
            // Test(0, 10);
            // Test(0, 20);
            // Test(0, 30);
            // Test(0, 40);
            // Test(0, -10);
            // Test(0, -20);
            // Test(0, -30);
            // Test(0, -40);
        }

        public void Test(float offsetX, float offsetZ)
        {
            var envPos1 = new Vector3(_gridPosition.x + offsetX, 0, _gridPosition.z + 5 + offsetZ);
            Instantiate(m_sectionPrefab, envPos1, Quaternion.identity, transform);

            var envPos2 = new Vector3(_gridPosition.x + 10 + offsetX, 0, _gridPosition.z + 5 + offsetZ);
            Instantiate(m_sectionPrefab, envPos2, Quaternion.identity, transform);

            var envPos3 = new Vector3(_gridPosition.x + 20 + offsetX, 0, _gridPosition.z + 5 + offsetZ);
            Instantiate(m_sectionPrefab, envPos3, Quaternion.identity, transform);

            var envPos4 = new Vector3(_gridPosition.x + 30 + offsetX, 0, _gridPosition.z + 5 + offsetZ);
            Instantiate(m_sectionPrefab, envPos4, Quaternion.identity, transform);

            var envPos5 = new Vector3(_gridPosition.x - 10 + offsetX, 0, _gridPosition.z + 5 + offsetZ);
            Instantiate(m_sectionPrefab, envPos5, Quaternion.identity, transform);

            var envPos6 = new Vector3(_gridPosition.x - 20 + offsetX, 0, _gridPosition.z + 5 + offsetZ);
            Instantiate(m_sectionPrefab, envPos6, Quaternion.identity, transform);

            var envPos7 = new Vector3(_gridPosition.x - 30 + offsetX, 0, _gridPosition.z + 5 + offsetZ);
            Instantiate(m_sectionPrefab, envPos7, Quaternion.identity, transform);

            var envPos8 = new Vector3(_gridPosition.x - 40 + offsetX, 0, _gridPosition.z + 5 + offsetZ);
            Instantiate(m_sectionPrefab, envPos8, Quaternion.identity, transform);

            var envPos9 = new Vector3(_gridPosition.x - 50 + offsetX, 0, _gridPosition.z + 5 + offsetZ);
            Instantiate(m_sectionPrefab, envPos9, Quaternion.identity, transform);

            var envPos10 = new Vector3(_gridPosition.x + 40 + offsetX, 0, _gridPosition.z + 5 + offsetZ);
            Instantiate(m_sectionPrefab, envPos10, Quaternion.identity, transform);

            var envPos11 = new Vector3(_gridPosition.x + 50 + offsetX, 0, _gridPosition.z + 5 + offsetZ);
            Instantiate(m_sectionPrefab, envPos11, Quaternion.identity, transform);
        }
    }
}