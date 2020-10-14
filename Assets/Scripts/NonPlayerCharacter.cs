using UnityEngine;

namespace RubyAdventure
{
    public class NonPlayerCharacter : MonoBehaviour
    {
        private readonly float _displayTime = 4;
        private GameObject _dialogBox;

        private float _timerDisplay;

        private void Start()
        {
            _dialogBox.SetActive(false);
            _timerDisplay = -1.0f;
        }

        public void DisplayDialog()
        {
            _timerDisplay = _displayTime;
            _dialogBox.SetActive(true);
        }

        private void Update()
        {
            DialogTimer();
        }

        private void DialogTimer()
        {
            if (!(_timerDisplay >= 0)) return;

            _timerDisplay -= Time.deltaTime;

            if (_timerDisplay < 0)
            {
                _dialogBox.SetActive(false);
            }
        }
    }
}