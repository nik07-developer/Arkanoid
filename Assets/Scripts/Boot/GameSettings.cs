using TMPro;
using TriInspector;
using UnityEngine;

using Game.Control;

namespace Game.Boot
{
    public class GameSettings : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _controlsLabel;

        public static ControlsType ControlsType = ControlsType.Mouse;

        public void SwapControls()
        {
            switch (ControlsType)
            {
                case ControlsType.Arrows:
                    ControlsType = ControlsType.Mouse;
                    _controlsLabel.SetText("Mouse");
                    break;

                case ControlsType.Mouse:
                    ControlsType = ControlsType.WASD;
                    _controlsLabel.SetText("WASD");
                    break;

                case ControlsType.WASD:
                    ControlsType = ControlsType.Arrows;
                    _controlsLabel.SetText("Arrows");
                    break;
            }
        }
    }
}
