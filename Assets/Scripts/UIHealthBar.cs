using UnityEngine;
using UnityEngine.UI;

namespace RubyAdventure
{
    public class UIHealthBar : MonoBehaviour
    {
        public static UIHealthBar Instance { get; private set; }

        [SerializeField] private Image mask;

        private float _originalSize;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _originalSize = mask.rectTransform.rect.width;
        }

        public void SetValue(float value)
        {
            mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _originalSize * value);
        }
    }
}
