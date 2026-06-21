using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

namespace FloatingText
{
    internal class FloatingTextManager : MonoBehaviour
    {
        static private readonly ProfilerMarker SpawnMarker = new("FloatingTextManager.Spawn");
        
        private FloatingTextFactory _factory;
        private List<FloatingTextView> _activeTexts = new ();

        public void Initialize()
        {
            _factory = new FloatingTextFactory();
        }

        public void Spawn(Vector3 position, string text)
        {
            SpawnMarker.Begin();

            var textView = _factory.Create();
            textView.SetupComponent(position, text);

            // track
            _activeTexts.Add(textView);

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
