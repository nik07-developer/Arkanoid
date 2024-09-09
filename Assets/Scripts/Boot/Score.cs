using System.Collections;
using System.Collections.Generic;
using TriInspector;
using UnityEngine;

namespace Game.Boot
{
    public class Score : MonoBehaviour
    {
        public static Score Instance { get; private set; } = null;

        [ShowInInspector, ReadOnly]
        public int Max { get; set; }

        [ShowInInspector, ReadOnly]
        public int Current { get; set; }

        [ShowInInspector, ReadOnly]
        public int Saved { get; set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            Max = 0;
            Current = 0;
            Saved = 0;
            DontDestroyOnLoad(gameObject);
        }
    }
}
