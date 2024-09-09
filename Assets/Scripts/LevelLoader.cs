using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Game
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField]
        private Transform _currentLevelRoot;

        [SerializeField]
        private List<GameObject> _levelPrefabs;

        public int LevelCount => _levelPrefabs.Count;

        public void LoadLevel(int levelIndex)
        {
            if (_currentLevelRoot.childCount > 0)
            {
                Destroy(_currentLevelRoot.GetChild(0).gameObject);
            }

            Instantiate(_levelPrefabs[levelIndex], _currentLevelRoot);
            EventEther.CallLevelLoaded(levelIndex);
        }
    }
}

