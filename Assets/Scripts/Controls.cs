using Game.Control;
using UnityEngine;

namespace Game
{
    public class WASDController : IController
    {
        public bool Launch() => Input.GetKeyDown(KeyCode.Space);
        public bool Left() => Input.GetKey(KeyCode.A);
        public bool Right() => Input.GetKey(KeyCode.D);
    }

    public class ArrowsController : IController
    {
        public bool Launch() => Input.GetKeyDown(KeyCode.Space);
        public bool Left() => Input.GetKey(KeyCode.LeftArrow);
        public bool Right() => Input.GetKey(KeyCode.RightArrow);
    }

    public class MouseControl : IController
    {
        private GameObject _carriage;

        public bool Launch()
        {
            return Input.GetMouseButtonDown(0);
        }

        private bool ChechCarriage()
        {
            if (_carriage == null)
            {
                var tmp = GameObject.FindAnyObjectByType<Carriage>();

                if (tmp != null)
                {
                    _carriage = tmp.gameObject;
                }
            }

            return _carriage != null;
        }

        public bool Left()
        {
            if (ChechCarriage())
            {
                var vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                return _carriage.transform.position.x - vec.x > 0.25f;
            }

            return false;
        }

        public bool Right()
        {
            if (ChechCarriage())
            {
                var vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                return _carriage.transform.position.x - vec.x < -0.25f;
            }

            return false;
        }
    }
}
