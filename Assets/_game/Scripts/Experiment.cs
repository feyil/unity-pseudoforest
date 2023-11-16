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

        private float _gridX;
        private float _gridZ;

        private void Start()
        {
            var controlManager = GameManager.Instance.GetControlManager();
            
            var playerMovementController = controlManager.GetPlayerMovementController();
            playerMovementController.AddListenerToPlayerPositionUpdate(OnPlayerPositionUpdate);

            var playerCameraController = controlManager.GetPlayerCameraController();
            playerCameraController.AddListenerToOnOrientationUpdate(OnOrientationUpdate);
            

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

        private void OnOrientationUpdate(Transform playerOrientation)
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);

                var localPoint = playerOrientation.InverseTransformPoint(child.position);
                var isInRectangle = IsPointInsideRectangle(-40, 40, 40, -10, localPoint.x, localPoint.z);
                
                child.gameObject.SetActive(isInRectangle);
            }
        }

        bool IsPointInsideRectangle(float x1, float y1, float x2, float y2, float px, float py)
        {
            return px >= x1 && px <= x2 && py <= y1 && py >= y2;
        }

        private void OnPlayerPositionUpdate(Vector3 position)
        {
            var posX = position.x;
            var posZ = position.z;

            var gridXDimension = 10;
            var gridZDimension = 10;

            var remainderX = posX % gridXDimension;
            var remainderZ = posZ % gridZDimension;

            var gridX = posX - remainderX;
            var gridZ = posZ - remainderZ;

            // Debug.Log($"{gridX}:{gridZ} position:{position}");

            if (gridX != _gridX || gridZ != _gridZ)
            {
                OnGridUpdate();
            }

            _gridX = gridX;
            _gridZ = gridZ;
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
            for (var i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }

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

        public void Test(float offsetX, float offsetZ)
        {
            var envPos1 = new Vector3(_gridX + offsetX, 0, _gridZ + 5 + offsetZ);
            Instantiate(m_sectionPrefab, envPos1, Quaternion.identity, transform);

            var envPos2 = new Vector3(_gridX + 10 + offsetX, 0, _gridZ + 5 + offsetZ);
            Instantiate(m_sectionPrefab, envPos2, Quaternion.identity, transform);

            var envPos3 = new Vector3(_gridX + 20 + offsetX, 0, _gridZ + 5 + offsetZ);
            Instantiate(m_sectionPrefab, envPos3, Quaternion.identity, transform);

            var envPos4 = new Vector3(_gridX + 30 + offsetX, 0, _gridZ + 5 + offsetZ);
            Instantiate(m_sectionPrefab, envPos4, Quaternion.identity, transform);

            var envPos5 = new Vector3(_gridX - 10 + offsetX, 0, _gridZ + 5 + offsetZ);
            Instantiate(m_sectionPrefab, envPos5, Quaternion.identity, transform);

            var envPos6 = new Vector3(_gridX - 20 + offsetX, 0, _gridZ + 5 + offsetZ);
            Instantiate(m_sectionPrefab, envPos6, Quaternion.identity, transform);

            var envPos7 = new Vector3(_gridX - 30 + offsetX, 0, _gridZ + 5 + offsetZ);
            Instantiate(m_sectionPrefab, envPos7, Quaternion.identity, transform);

            var envPos8 = new Vector3(_gridX - 40 + offsetX, 0, _gridZ + 5 + offsetZ);
            Instantiate(m_sectionPrefab, envPos8, Quaternion.identity, transform);

            var envPos9 = new Vector3(_gridX - 50 + offsetX, 0, _gridZ + 5 + offsetZ);
            Instantiate(m_sectionPrefab, envPos9, Quaternion.identity, transform);

            var envPos10 = new Vector3(_gridX + 40 + offsetX, 0, _gridZ + 5 + offsetZ);
            Instantiate(m_sectionPrefab, envPos10, Quaternion.identity, transform);

            var envPos11 = new Vector3(_gridX + 50 + offsetX, 0, _gridZ + 5 + offsetZ);
            Instantiate(m_sectionPrefab, envPos11, Quaternion.identity, transform);
        }
    }
}