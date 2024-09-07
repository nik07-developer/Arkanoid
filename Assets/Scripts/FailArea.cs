using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class FailArea : MonoBehaviour
    {
        public event Action OnFail;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<Ball>(out var ball))
            {
                Debug.Log("GameOver");
                OnFail();
            }
        }
    }
}