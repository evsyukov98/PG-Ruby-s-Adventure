using UnityEngine;

namespace RubyAdventure
{
    public class InputManager : MonoBehaviour
    {
        public static float Horizontal;
        public static float Vertical;
        public static bool X;
        public static bool C;

        private void Update()
        {
            Horizontal = Input.GetAxis("Horizontal");
            Vertical = Input.GetAxis("Vertical");
            X = Input.GetKeyDown(KeyCode.X);
            C = Input.GetKeyDown(KeyCode.C);
        }
    }
}