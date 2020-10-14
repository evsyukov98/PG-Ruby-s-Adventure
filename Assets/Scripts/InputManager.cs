using UnityEngine;

namespace RubyAdventure
{
    public class InputManager
    {

        public static float Horizontal => Input.GetAxis("Horizontal");
        public static float Vertical => Input.GetAxis("Vertical");

        public static bool IsActionPressed => Input.GetKeyDown(KeyCode.X);
        public static bool IsFirePressed => Input.GetKeyDown(KeyCode.C);

    }
}