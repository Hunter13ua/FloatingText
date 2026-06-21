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

        public void SetupComponent(string text)
        {
            if (_textComponent == null)
            {
                Initialize();
            }

            _textComponent.text = text;
        }
    }
}
