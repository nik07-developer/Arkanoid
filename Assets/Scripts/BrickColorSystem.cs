using UnityEngine;

namespace Game
{
    public class BrickColorSystem : MonoBehaviour
    {
        [SerializeField]
        private Color _green;

        [SerializeField]
        private Color _yellow;

        [SerializeField]
        private Color _orange;

        [SerializeField]
        private Color _red;

        private void Awake()
        {
            EventEther.OnPowerUp += HandlePowerUp;
            EventEther.OnLevelLoaded += HandleLevelLoded;
        }

        private void OnDisable()
        {
            EventEther.OnPowerUp -= HandlePowerUp;
            EventEther.OnLevelLoaded -= HandleLevelLoded;
        }

        private void HandlePowerUp(int power)
        {
            var ball = FindObjectOfType<Ball>();

            foreach (var brick in GameObject.FindObjectsOfType<Brick>())
            {
                brick.Paint(RangeColorByLevel(brick.Power, ball.Power));
            }
        }

        private void HandleLevelLoded(int levelIndex)
        {
            var ball = FindObjectOfType<Ball>();

            foreach (var brick in GameObject.FindObjectsOfType<Brick>())
            {
                brick.Paint(RangeColorByLevel(brick.Power, ball.Power));
            }
        }

        public Color RangeColorByLevel(int brickPower, int ballPower)
        {
            if (brickPower <= ballPower)
            {
                return _green;
            }

            if (brickPower - ballPower < 10)
            {
                return _yellow;
            }

            if (brickPower - ballPower < 25)
            {
                return _orange;
            }

            return _red;
        }
    }
}