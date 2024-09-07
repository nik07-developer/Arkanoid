using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Control
{
    public enum ControlsType
    {
        Arrows,
        Mouse,
        WASD,
    }

    public static class GameInput
    {
        public static IController Controller;

        public static bool Left() => Controller.Left();
        public static bool Right() => Controller.Right();
        public static bool Launch() => Controller.Launch();
    }

    public interface IController
    {
        bool Left();
        bool Right();
        bool Launch();
    }
}

