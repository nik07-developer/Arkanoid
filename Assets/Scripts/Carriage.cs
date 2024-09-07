using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Controller : MonoBehaviour
    {
        private ICarriageController _controller;
        private Rigidbody2D _rigidbody;

        [SerializeField]
        private float _speed = 6;

        [SerializeField]
        private Rigidbody2D _ballRigidbody;

        private void Awake()
        {
            _controller = new MouseController();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _controller.Move(_rigidbody, _speed);
            _controller.Launch(_ballRigidbody);
        }
    }
}

