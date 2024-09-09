using UnityEngine;

namespace Game.UI
{
    public class CameraScaler : MonoBehaviour
    {
        const float defaultScale = 5.75f;

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