using System;

namespace Game
{
    public class EventEther
    {
        private readonly static EventEther _instance;

        static EventEther()
        {
            _instance = new EventEther();
        }

        // OnPowerUp --- Ball Power
        private event Action<int> PowerUp;

        public static event Action<int> OnPowerUp
        {
            add { _instance.PowerUp += value; }
            remove { _instance.PowerUp -= value; } 
        }

        public static void CallPowerUp(int newPower)
        {
            _instance.PowerUp?.Invoke(newPower);
        }

        
        // OnLevelLoad --- Game Level
        private event Action<int> LevelLoaded;

        public static event Action<int> OnLevelLoaded
        {
            add { _instance.LevelLoaded += value; }
            remove { _instance.LevelLoaded -= value; }
        }

        public static void CallLevelLoaded(int levelIndex)
        {
            _instance.LevelLoaded?.Invoke(levelIndex);
        }


        // OnLevelComplete
        private event Action<int> LevelCompleted;

        public static event Action<int> OnLevelCompleted
        {
            add { _instance.LevelCompleted += value; }
            remove { _instance.LevelCompleted -= value; }
        }

        public static void CallLevelCompleted(int levelIndex)
        {
            _instance.LevelCompleted?.Invoke(levelIndex);
        }


        // OnLevelLoad --- Game Level
        private event Action<Brick> BrickBroken;

        public static event Action<Brick> OnBrickBroken
        {
            add { _instance.BrickBroken += value; }
            remove { _instance.BrickBroken -= value; }
        }

        public static void CallBrickBroken(Brick brick)
        {
            _instance.BrickBroken?.Invoke(brick);
        }

        
        // OnScoreChanged
        private event Action<int> ScoreChanged;

        public static event Action<int> OnScoreChanged
        {
            add { _instance.ScoreChanged += value; }
            remove { _instance.ScoreChanged -= value; }
        }

        public static void CallScoreChanged(int score)
        {
            _instance.ScoreChanged?.Invoke(score);
        }


        // OnLivesChanged
        private event Action<int> LivesChanged;

        public static event Action<int> OnLivesChanged
        {
            add { _instance.LivesChanged += value; }
            remove { _instance.LivesChanged -= value; }
        }

        public static void CallLivesChanged(int lives)
        {
            _instance.LivesChanged?.Invoke(lives);
        }
    }
}
