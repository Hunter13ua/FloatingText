using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

namespace FloatingText
{
    internal class FloatingTextManager : MonoBehaviour
    {
        static private readonly ProfilerMarker SpawnMarker = new("FloatingTextManager.Spawn");
        
        private FloatingTextFactory _factory;
        private List<ActiveText> _activeTexts = new ();

        public void Initialize()
        {
            _factory = new FloatingTextFactory();
        }

        public void Spawn(string text, FloatingTextStyleSO style, Vector3 position, Quaternion rotation)
        {
            SpawnMarker.Begin();

            var textView = _factory.Create(style);
            textView.SetupComponent(position, rotation, text);

            var activeText = new ActiveText()
            {
                View = textView,
                Lifetime = 0f,
                MaxLifetime = style.Lifetime,
                Position = position,
                Style = style
            };

            // track
            _activeTexts.Add(activeText);

            SpawnMarker.End();
        }

        private void Update()
        {
            for (int i = _activeTexts.Count - 1; i >= 0; i--)
            {
                var text = _activeTexts[i];

                text.Position += Vector3.up * Time.deltaTime * text.Style.Speed;
                text.Lifetime += Time.deltaTime;

                text.View.transform.position = text.Position;

                if (text.Lifetime >= text.MaxLifetime)
                {
                    _factory.Release(text.View, text.Style);
                    _activeTexts.RemoveAt(i);
                }
            }
        }
    }
}
