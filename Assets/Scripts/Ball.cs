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
        private int _power;

        public int Power
        {
            get { return _power; }
            set
            {
                _power = value;
                EventEther.CallPowerUp(value);
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
                var velocity = _rigidbody.velocity;

                if (Approximately(velocity.sqrMagnitude, 0f))
                {
                    _rigidbody.velocity = Vector2.down * _speed;
                }
                else
                {
                    if (Mathf.Abs(velocity.x) / Mathf.Abs(velocity.y) > 10)
                    {
                        velocity = new Vector2(velocity.x, velocity.y > 0 ? 5 : -5).normalized * _speed;
                        _rigidbody.velocity = velocity;
                    }

                    _cachedVelocity = velocity;
                }

                /*if (Approximately(_rigidbody.velocity.y, 0f) 
                {
                    _rigidbody.velocity = (_rigidbody.velocity + 2 * Vector2.down).normalized * _speed;
                }*/
            }
            else
            {
                transform.position = new Vector2(_carriage.position.x, transform.position.y);

                if (GameInput.Launch() && Time.timeScale > 0)
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
            if (collision.gameObject.TryGetComponent<Carriage>(out var car))
            {
                var carPos = car.transform.position;
                var ballPos = transform.position;
                float len = 2f;

                var dx = ballPos.x - (carPos.x - len / 2);
                var x = Mathf.Lerp(-1.4f, 1.4f, dx / len);

                _rigidbody.velocity = _speed * new Vector2(x, 1f).normalized;
                return;
            }

            var vec = Vector2.Reflect(_cachedVelocity, collision.GetContact(0).normal).normalized;
            _rigidbody.velocity = vec * _speed;
        }

        /*private void OnCollisionStay2D(Collision2D collision)
        {
            var normal = Vector2.zero;

            for (int i = 0; i < collision.contactCount; i++)
            {
                normal += collision.GetContact(i).normal;
            }

            _rigidbody.velocity = _speed * (normal.normalized + _rigidbody.velocity).normalized;
        }*/
    }
}

