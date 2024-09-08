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
            EventEther.OnLevelUp += Handle;
            EventEther.OnLevelLoaded += Handle;
        }

        private void OnDisable()
        {
            EventEther.OnLevelUp -= Handle;
            EventEther.OnLevelLoaded -= Handle;
        }

        private void Handle()
        {
            var ball = FindObjectOfType<Ball>();

            foreach (var brick in GameObject.FindObjectsOfType<LevelBrick>())
            {
                brick.Paint(RangeColorByLevel(brick.Level, ball.Level));
            }
        }

        public Color RangeColorByLevel(int brickLevel, int ballLevel)
        {
            if (brickLevel <= ballLevel)
            {
                return _green;
            }

            if (brickLevel - ballLevel < 10)
            {
                return _yellow;
            }

            if (brickLevel - ballLevel < 25)
            {
                return _orange;
            }

            return _red;
        }
    }
}