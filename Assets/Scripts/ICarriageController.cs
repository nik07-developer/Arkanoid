using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Game
{
    public interface ICarriageController
    {
        void Move(Rigidbody2D carriage, float speed);
        void Launch(Rigidbody2D ball);
    }

    public class WASDCarController : ICarriageController
    {
        private bool _ready = true;

        public void Launch(Rigidbody2D ball)
        {
            if (Input.GetKeyDown(KeyCode.Space) && _ready)
            {
                _ready = false;
                ball.velocity = new Vector2(Random.Range(-1f, 1f), 1f).normalized;
            }
        }

        public void Move(Rigidbody2D carriage, float speed)
        {
            bool a = Input.GetKey(KeyCode.A);
            bool b = Input.GetKey(KeyCode.B);

            if (a && !b)
            {
                carriage.velocity = Vector2.left * speed;
            }
            else if (!a && b)
            {
                carriage.velocity = Vector2.right * speed;
            }
            else
            {
                carriage.velocity = Vector2.zero;
            }
        }
    }
}

