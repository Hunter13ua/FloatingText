using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

namespace FloatingText
{
    internal class FloatingTextManager : MonoBehaviour
    {
        static private readonly ProfilerMarker SpawnMarker = new("FloatingTextManager.Spawn");
        
        private GameObject _floatingTextPrefab;

        private List<FloatingTextView> _activeTexts = new ();

        public void Initialize()
        {
            _floatingTextPrefab = Resources.Load<FloatingTextView>("FloatingText Prefab (TMP)").gameObject;
        }

        public void Spawn(Vector3 position, string text)
        {
            SpawnMarker.Begin();

            var newTextGO = Instantiate(_floatingTextPrefab, position, Quaternion.identity);
            var textView = newTextGO.GetComponent<FloatingTextView>();
            textView.SetupComponent(text);

            _activeTexts.Add(textView);

            // TODO: implement pooling, animation, and cleanup

            SpawnMarker.End();
        }

        private void Update()
        {
            foreach (var text in _activeTexts)
            {
                text.transform.position += Vector3.up * Time.deltaTime * 0.1f;
            }
        }
    }
}
