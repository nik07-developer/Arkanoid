using TMPro;
using UnityEngine;

namespace Game
{
    public class LevelBrick : MonoBehaviour
    {
        [SerializeField]
        private int _level;

        public int Level
        {
            get
            {
                return _level;
            }
            private set
            {
                _level = value;
                _levelText.text = _level.ToString();
            }
        }

        [SerializeField]
        private SpriteRenderer _sprite;

        [SerializeField]
        private TMP_Text _levelText;

        public void Paint(Color color)
        {
            _sprite.color = color;
        }

        private void OnValidate()
        {
            _levelText.text = _level.ToString();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<Ball>(out var ball))
            {
                if (Level > ball.Level)
                {
                    Level--;
                    EventEther.CallLevelUp(); // to update colors
                }
                else
                {
                    ball.Level += Level;
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
