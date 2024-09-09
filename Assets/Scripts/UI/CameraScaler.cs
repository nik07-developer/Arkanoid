using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    public class CameraScaler : MonoBehaviour
    {
        const float defaultScale = 5f;

        const int x = 1600;
        const int y = 900;

        [SerializeField]
        private Camera _camera;

        private void Update()
        {
            _camera.orthographicSize = defaultScale * (x / (float)Screen.width);
        }
    }
}