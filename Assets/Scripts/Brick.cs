using TMPro;
using UnityEngine;

namespace Game
{
    public class Brick : MonoBehaviour
    {
        [SerializeField]
        private int _power;

        public int Power
        {
            get
            {
                return _power;
            }
            private set
            {
                _power = value;
                _powerText.text = _power.ToString();
            }
        }

        [SerializeField]
        private SpriteRenderer _sprite;

        [SerializeField]
        private TMP_Text _powerText;

        public void Paint(Color color)
        {
            _sprite.color = color;
        }

        private void OnValidate()
        {
            _powerText.text = _power.ToString();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<Ball>(out var ball))
            {
                if (Power > ball.Power)
                {
                    Power--;
                    EventEther.CallPowerUp(ball.Power); // to update colors
                }
                else
                {
                    ball.Power += Power;
                    gameObject.SetActive(false);
                    EventEther.CallBrickBroken(this);
                }
            }
        }
    }
}
