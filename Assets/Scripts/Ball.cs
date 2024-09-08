using Game.Control;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ball : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _rigidbody;

        [SerializeField]
        private Transform _carriage;

        [SerializeField]
        private int _level;

        public int Level
        {
            get { return _level; }
            set
            {
                _level = value;
                EventEther.CallLevelUp();
            }
        }

        [SerializeField]
        private float _speed = 4f;

        private bool _isLaunched;
        private Vector2 _cachedVelocity;


        private bool Approximately(float a, float b, float accuracy = 1e-3f)
        {
            return Mathf.Abs(a - b) < accuracy;
        }

        private void Update()
        {
            if (_isLaunched)
            {
                if (!Approximately(_rigidbody.velocity.sqrMagnitude, 0f))
                {
                    _cachedVelocity = _rigidbody.velocity;
                }

                if (Approximately(_rigidbody.velocity.y, 0f))
                {
                    _rigidbody.velocity = (_rigidbody.velocity + Vector2.down).normalized * _speed;
                }
            }
            else
            {
                transform.position = new Vector2(_carriage.position.x, transform.position.y);

                if (GameInput.Launch())
                {
                    Launch();
                }
            }

        }

        private void Launch()
        {
            _rigidbody.velocity = new Vector2(Random.Range(-1f, 1f), 1f).normalized * _speed;
            _isLaunched = true;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var vec = Vector2.Reflect(_cachedVelocity, collision.GetContact(0).normal).normalized;
            _rigidbody.velocity = vec * _speed;
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            var normal = Vector2.zero;

            for (int i = 0; i < collision.contactCount; i++)
            {
                normal += collision.GetContact(i).normal;
            }

            _rigidbody.velocity = _speed * (normal.normalized + _rigidbody.velocity).normalized;
        }
    }
}

