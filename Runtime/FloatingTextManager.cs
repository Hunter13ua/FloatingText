using System.Collections.Generic;
using UnityEngine;

namespace FloatingText
{
    internal class FloatingTextManager : MonoBehaviour
    {
        private GameObject _floatingTextPrefab;

        private List<FloatingTextView> _activeTexts = new ();

        public void Initialize()
        {
            _floatingTextPrefab = Resources.Load<FloatingTextView>("FloatingText Prefab (TMP)").gameObject;
        }

        public void Spawn(Vector3 position, string text)
        {
            var newTextGO = Instantiate(_floatingTextPrefab, position, Quaternion.identity);
            var textView = newTextGO.GetComponent<FloatingTextView>();
            textView.SetupComponent(text);

            _activeTexts.Add(textView);

            // TODO: implement pooling, animation, and cleanup
        }

        private void Update()
        {
            foreach (var text in _activeTexts)
            {
                text.transform.position += Vector3.up * Time.deltaTime;
            }
        }
    }
}
