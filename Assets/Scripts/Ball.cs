using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ball : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;

        private Vector2 _cachedVelocity;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private bool Approximately(float a, float b, float accuracy = 1e-3f)
        {
            return Mathf.Abs(a - b) < accuracy;
        }

        private void Update()
        {
            if (!Approximately(_rigidbody.velocity.sqrMagnitude, 0f))
            {
                _cachedVelocity = _rigidbody.velocity;
            }

            if (Approximately(_rigidbody.velocity.y, 0f))
            {
                _rigidbody.velocity = (_rigidbody.velocity + Vector2.down).normalized * 4f;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var vec = Vector2.Reflect(_cachedVelocity, collision.GetContact(0).normal).normalized;
            _rigidbody.velocity = vec * 4f;

            if (collision.gameObject.TryGetComponent<Brick>(out var brick))
            {
                brick.Hit();
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            var normal = Vector2.zero;

            for (int i = 0; i < collision.contactCount; i++)
            {
                normal += collision.GetContact(i).normal;
            }
            
            _rigidbody.velocity = 4f * (normal.normalized + _rigidbody.velocity).normalized;
        }
    }
}

