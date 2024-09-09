using Game.Boot;
using Game.Control;
using Game.UI;
using System.Collections;
using System.Collections.Generic;
using TriInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        [SerializeField]
        private Window _pauseWindow;

        [SerializeField]
        private Window _levelFailedWindow;

        [SerializeField]
        private Window _gameLosedWindow;

        [SerializeField]
        private Window _levelCompletedWindow;

        [SerializeField]
        private Window _gameCompletedWindow;

        private const int maxLives = 3;

        [ShowInInspector, ReadOnly]
        private bool _isPause = false;

        [ShowInInspector, ReadOnly]
        private int _lives = maxLives;

        [ShowInInspector, ReadOnly]
        private int _level = 0;

        private Score _score;

        private UIState _state;


        // Init
        private void Awake()
        {
            _score = Score.Instance;
            _failArea.OnFail += FF;
        }

        private void Start()
        {
            RestartGame();
        }

        private void OnDisable()
        {
            _failArea.OnFail -= FF;
        }


        // State Transitions

        public void StopGame()
        {
            SetPause(true);
            _pauseWindow.Show();
            _state = UIState.Pause;
        }

        public void ContinueGame()
        {
            SetPause(false);
            _pauseWindow.Hide();
            _state = UIState.GameRunning;
        }

        private void SetPause(bool pause)
        {
            if (pause)
            {
                _isPause = true;
                Time.timeScale = 0f;
            }
            else
            {
                _isPause = false;
                Time.timeScale = 1f;
            }

        }

        public void FailLevel()
        {

        }

        public void RestartLevel()
        {

        }

        public void NextLevel()
        {

        }

        public void BackToMainMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void RestartGame()
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
        

        private void Update()
        {
            _ = _state switch
            {
                UIState.GameRunning => HandleGameRunningState(),
                UIState.Pause => HandlePauseState(),
                UIState.Fail => HandleFailState(),
                UIState.Lose => HandleLoseState (),
                UIState.LevelCompleted => HandleLevelCompletedState(),
                UIState.Win => HandleWinState(),
                _ => UIState.GameRunning
            };
        }

        // State Handlers

        private UIState HandleGameRunningState()
        {
            if (GameInput.Pause())
            {
                StopGame();
                return UIState.Pause;
            }

            return UIState.GameRunning;
        }

        private UIState HandlePauseState()
        {
            if (GameInput.Pause())
            {
                ContinueGame();
                return UIState.GameRunning;
            }

            return UIState.Pause;
        }

        private UIState HandleFailState()
        {
            return UIState.Fail;
        }

        private UIState HandleLoseState()
        {
            return UIState.Lose;
        }

        private UIState HandleLevelCompletedState()
        {
            return UIState.LevelCompleted;
        }

        private UIState HandleWinState()
        {
            return UIState.Win;
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
                RestartGame();
            }
        }
    }
}

