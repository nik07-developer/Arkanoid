using Game.Boot;
using Game.Control;
using Game.UI;
using System.Collections;
using System.Collections.Generic;
using TriInspector;
using UnityEngine;

namespace Game
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField]
        private FailArea _failArea;

        [SerializeField]
        private LevelLoader _levelLoader;

        [SerializeField]
        private HudPanel _hudPanel;

        private const int maxLives = 3;

        [ShowInInspector, ReadOnly]
        private bool _isPause = false;

        [ShowInInspector, ReadOnly]
        private int _lives = maxLives;

        [ShowInInspector, ReadOnly]
        private int _level = 0;

        private Score _score;

        public void StopGame()
        {
            _isPause = true;
        }

        public void ContinueGame()
        {
            _isPause = false;
        }

        public void FailLevel()
        {

        }

        public void RestartLevel()
        {

        }


        private void Awake()
        {
            _score = Score.Instance;

            _failArea.OnFail += FF;
        }

        private void Start()
        {
            ResetGame();
        }

        private void OnDisable()
        {
            _failArea.OnFail -= FF;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                StopGame();
            }

            if (!_isPause)
            {

            }
        }

        private void FF()
        {
            _lives--;
            if (_lives > 0)
            {
                _levelLoader.LoadLevel(_level);
            }
            else
            {
                ResetGame();
            }
        }

        private void ResetGame()
        {
            _level = 0;
            _lives = maxLives;

            GameInput.Controller = GameSettings.ControlsType switch
            {
                ControlsType.Arrows => new ArrowsController(),
                ControlsType.Mouse => new MouseControl(),
                ControlsType.WASD => new WASDController(),
            };

            _levelLoader.LoadLevel(0);
        }
    }
}

