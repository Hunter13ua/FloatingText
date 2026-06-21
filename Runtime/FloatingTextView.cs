using UnityEngine;

namespace FloatingText
{
    [RequireComponent(typeof(TMPro.TMP_Text))]
    internal class FloatingTextView : MonoBehaviour
    {
        private TMPro.TMP_Text _textComponent;

        private void Initialize()
        {
            _textComponent = GetComponent<TMPro.TMP_Text>();
        }

        public void SetupComponent(Vector3 position, string text)
        {
            if (_textComponent == null)
            {
                Initialize();
            }

            transform.position = position;
            _textComponent.text = text;
        }
        
        public void ApplyStyle(FloatingTextStyleSO style)
        {
            if (_textComponent == null)
            {
                Initialize();
            }

            transform.localScale = Vector3.one * style.Scale;
            _textComponent.color = style.Color;
        }
    }
}
