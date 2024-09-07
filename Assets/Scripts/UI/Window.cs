using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    public class Window : MonoBehaviour
    {
        [SerializeField]
        private GameObject _window;

        public void Show()
        {
            _window.SetActive(true);
        }

        public void Hide()
        {
            _window.SetActive(false);
        }
    }
}

