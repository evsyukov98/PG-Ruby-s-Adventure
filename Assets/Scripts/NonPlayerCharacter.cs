using UnityEngine;

namespace RubyAdventure
{
    
    public class NonPlayerCharacter : MonoBehaviour
    {
        [SerializeField] private float displayTime = 4;
        [SerializeField] private GameObject dialogBox = default;

        private float _timerDisplay;

        private void Start()
        {
            dialogBox.SetActive(false);
            _timerDisplay = -1.0f;
        }

        public void DisplayDialog()
        {
            _timerDisplay = displayTime;
            dialogBox.SetActive(true);
        }

        private void Update()
        {
            DialogTimer();
        }

        private void DialogTimer()
        {
            if (_timerDisplay < 0) return;

            _timerDisplay -= Time.deltaTime;

            if (_timerDisplay < 0) dialogBox.SetActive(false);
        }
    }
}