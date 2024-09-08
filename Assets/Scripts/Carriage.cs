using Game.Control;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Carriage : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _rigidbody;

        [SerializeField]
        private float _speed = 6;

        private void Update()
        {
            bool l = GameInput.Left();
            bool r = GameInput.Right();

            if (l && !r)
            {
                _rigidbody.velocity = Vector2.left * _speed;
            }
            else if (!l && r)
            {
                _rigidbody.velocity = Vector2.right * _speed;
            }
            else
            {
                _rigidbody.velocity = Vector2.zero;
            }
        }
    }
}

