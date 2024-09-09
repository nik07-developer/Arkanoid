using Game.Boot;
using Game.Control;
using Game.UI;
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
        private HudWindow _hudWindow;

        [SerializeField]
        private Window _pauseWindow;

        [SerializeField]
        private LevelFailedWindow _levelFailedWindow;

        [SerializeField]
        private Window _gameLosedWindow;

        [SerializeField]
        private Window _levelCompletedWindow;

        [SerializeField]
        private WinWindow _gameCompletedWindow;

        private const int maxLives = 3;

        [ShowInInspector, ReadOnly]
        private int _lives = maxLives;

        public int Lives
        {
            get { return _lives; }
            private set
            {
                _lives = value;
                EventEther.CallLivesChanged(value);
            }
        }

        [ShowInInspector, ReadOnly]
        private int _level = 1;

        private GameState _state;


        // Init
        private void Awake()
        {
            _failArea.OnFail += FailLevel;
            EventEther.OnBrickBroken += CheckLevel;

            EventEther.OnPowerUp += UpdatePower;
            EventEther.OnLevelLoaded += UpdateHud; // not a updateLevel
            EventEther.OnScoreChanged += UpdateScore;
            EventEther.OnLivesChanged += UpdateLives;
        }

        private void Start()
        {
            RestartGame();
        }

        private void OnDisable()
        {
            _failArea.OnFail -= FailLevel;
            EventEther.OnBrickBroken -= CheckLevel;

            EventEther.OnPowerUp -= UpdatePower;
            EventEther.OnLevelLoaded -= UpdateHud;
            EventEther.OnScoreChanged -= UpdateScore;
            EventEther.OnLivesChanged -= UpdateLives;
        }


        // State Transitions

        public void StopGame()
        {
            SetPause(true);
            _pauseWindow.Show();
            _state = GameState.Pause;
        }

        public void ContinueGame()
        {
            HideAll();
            SetPause(false);
            _state = GameState.GameRunning;
        }

        private void SetPause(bool pause)
        {
            if (pause)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }

        private void HideAll()
        {
            _pauseWindow.Hide();
            _levelCompletedWindow.Hide();
            _gameCompletedWindow.Hide();
            _levelFailedWindow.Hide();
            _gameLosedWindow.Hide();
        }

        private void UpdateHud(int tmp)
        {
            var ball = GameObject.FindObjectOfType<Ball>();
            UpdateLevel(_level);
            UpdateLives(_lives);
            UpdatePower(ball.Power);
            UpdateScore(Score.Instance.Current);
        }

        private void UpdatePower(int power) => _hudWindow.UpdatePower(power);
        private void UpdateScore(int score) => _hudWindow.UpdateScore(score);
        private void UpdateLevel(int level) => _hudWindow.UpdateLevel(level);
        private void UpdateLives(int lives) => _hudWindow.UpdateLives(lives);

        public void FailLevel()
        {
            SetPause(true);
            Score.Instance.Max = Mathf.Max(Score.Instance.Max, Score.Instance.Current);
            Lives--;

            if (Lives > 0)
            {
                _levelFailedWindow.Show();
                _levelFailedWindow.ShowFailText(_lives);
                _state = GameState.LevelFailed;
            }
            else
            {
                _gameLosedWindow.Show();
                _state = GameState.GameLosed;
            }
        }

        public void CompleteLevel()
        {
            SetPause(true);
            Score.Instance.Saved = Score.Instance.Current;
            Score.Instance.Max = Mathf.Max(Score.Instance.Max, Score.Instance.Current);
            EventEther.CallLevelCompleted(_level);

            if (_level < _levelLoader.LevelCount)
            {
                _levelCompletedWindow.Show();
            }
            else
            {
                _gameCompletedWindow.Show();
                _gameCompletedWindow.ShowFinalScore(Score.Instance.Current, Score.Instance.Max);
            }
        }

        public void RestartLevel()
        {
            Score.Instance.Max = Mathf.Max(Score.Instance.Max, Score.Instance.Current);
            Score.Instance.Current = Score.Instance.Saved;
            _levelLoader.LoadLevel(_level - 1);
            _state = GameState.GameRunning;
            HideAll();
            SetPause(false);
        }

        public void NextLevel()
        {
            Score.Instance.Max = Mathf.Max(Score.Instance.Max, Score.Instance.Current);
            Score.Instance.Saved = Score.Instance.Current;
            HideAll();

            _level++;
            if (_levelLoader.LevelCount > _level - 1)
            {
                _levelLoader.LoadLevel(_level - 1);
                _state = GameState.GameRunning;
                SetPause(false);
            }
        }

        public void BackToMainMenu()
        {
            Score.Instance.Max = Mathf.Max(Score.Instance.Max, Score.Instance.Current);
            Score.Instance.Saved = Score.Instance.Current;
            SceneManager.LoadScene(0);
            SetPause(false);
        }

        public void RestartGame()
        {
            _level = 1;
            Lives = maxLives;

            Score.Instance.Max = Mathf.Max(Score.Instance.Max, Score.Instance.Current);
            Score.Instance.Saved = 0;
            Score.Instance.Current = 0;

            GameInput.Controller = GameSettings.ControlsType switch
            {
                ControlsType.Arrows => new ArrowsController(),
                ControlsType.Mouse => new MouseControl(),
                ControlsType.WASD => new WASDController(),
                _ => new MouseControl()
            };

            _levelLoader.LoadLevel(0);
            HideAll();
            _state = GameState.GameRunning;
            SetPause(false);
        }

        private void CheckLevel(Brick brick)
        {
            var bricks = GameObject.FindObjectsOfType<Brick>();

            if (bricks == null || bricks.Length == 0)
            {
                CompleteLevel();
            }
        }

        private void Update()
        {
            _ = _state switch
            {
                GameState.GameRunning => HandleGameRunningState(),
                GameState.Pause => HandlePauseState(),
                GameState.LevelFailed => HandleFailState(),
                GameState.GameLosed => HandleLoseState(),
                GameState.LevelCompleted => HandleLevelCompletedState(),
                GameState.Win => HandleWinState(),
                _ => GameState.GameRunning
            };
        }

        // State Handlers

        private GameState HandleGameRunningState()
        {
            if (GameInput.Pause())
            {
                StopGame();
                return GameState.Pause;
            }

            return GameState.GameRunning;
        }

        private GameState HandlePauseState()
        {
            if (GameInput.Pause())
            {
                ContinueGame();
                return GameState.GameRunning;
            }

            return GameState.Pause;
        }

        private GameState HandleFailState()
        {
            return GameState.LevelFailed;
        }

        private GameState HandleLoseState()
        {
            return GameState.GameLosed;
        }

        private GameState HandleLevelCompletedState()
        {
            return GameState.LevelCompleted;
        }

        private GameState HandleWinState()
        {
            return GameState.Win;
        }
    }
}

