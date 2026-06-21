using UnityEngine;
using TMPro;

namespace FloatingText
{
    /// <summary>
    /// Per-instance floating text settings. Always returns SettingsKey = -1,
    /// meaning each spawn creates a unique entity that is destroyed (not pooled).
    /// Use this for one-off texts where you need full control, or implement
    /// <see cref="IFloatingTextSettings"/> yourself with a custom key for pooling.
    /// </summary>
    public class FloatingTextCustomSettings : IFloatingTextSettings
    {
        public int SettingsKey => -1;

        public float Lifetime { get; set; } = 1.5f;
        public float FloatSpeed { get; set; } = 2f;
        public Vector2 FloatDirection { get; set; } = new Vector2(0f, 1f);

        public float FontSize { get; set; } = 36f;
        public Color Color { get; set; } = Color.white;
        public TMP_FontAsset Font { get; set; }

        public AnimationCurve FadeCurve { get; set; } = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
        public AnimationCurve ScaleCurve { get; set; } = AnimationCurve.Constant(0f, 1f, 1f);
    }
}