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

        // OnLevelUp --- Ball Level
        private event Action LevelUp;

        public static event Action OnLevelUp
        {
            add { _instance.LevelUp += value; }
            remove { _instance.LevelUp -= value; } 
        }

        public static void CallLevelUp()
        {
            _instance.LevelUp?.Invoke();
        }


        // OnLevelLoad --- Game Level
        private event Action LevelLoaded;

        public static event Action OnLevelLoaded
        {
            add { _instance.LevelLoaded += value; }
            remove { _instance.LevelLoaded -= value; }
        }

        public static void CallLevelLoaded()
        {
            _instance.LevelLoaded?.Invoke();
        }
    }
}
