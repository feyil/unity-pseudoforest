using _game.Scripts.ControlSystem;
using _game.Scripts.Core.Ui;
using _game.Scripts.Utility;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _game.Scripts.Core
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField] private ControlManager m_controlManager;
        [SerializeField] private WorldStreamer m_worldStreamer;

        private void Awake()
        {
            InitializeAwake();
        }

        private void Start()
        {
            InitializeStart();
        }

        private void InitializeAwake()
        {
            UiManager.Instance.Initialize();
            m_controlManager.Initialize();
        }

        private void InitializeStart()
        {
            StartGame();
        }

        [Button]
        private void StartGame()
        {
            m_worldStreamer.Initialize();
        }

        public ControlManager GetControlManager()
        {
            return m_controlManager;
        }
    }
}