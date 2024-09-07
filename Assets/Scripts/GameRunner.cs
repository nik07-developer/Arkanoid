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
        private HudPanel _hudPanel;

        private const int maxLives = 3;

        [ShowInInspector, ReadOnly]
        private bool _isPause = false;

        [ShowInInspector, ReadOnly]
        private int _lives = maxLives;

        [ShowInInspector, ReadOnly]
        private int _score = 0;

        [ShowInInspector, ReadOnly]
        private int _level = 0;


        private void Awake()
        {
            _failArea.OnFail += FF;
            ResetGame();
        }

        private void OnDisable()
        {
            _failArea.OnFail -= FF;
        }

        private void Update()
        {
            if ( Input.GetKeyDown(KeyCode.Escape))
            {
                _isPause = !_isPause;
            }

            if (!_isPause)
            {

            }
        }

        private void FF()
        {

        }

        private void ResetLevel()
        {
            
        }

        private void ResetGame()
        {
            _score = 0;
            _level = 0;
            _lives = maxLives;

            GameInput.Controller = GameSettings.ControlsType switch
            {
                ControlsType.Arrows => new ArrowsController(),
                ControlsType.Mouse => new MouseControl(),
                ControlsType.WASD => new WASDController(),
            };
        }
    }
}

