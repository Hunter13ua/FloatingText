using UnityEngine;
using TMPro;

namespace FloatingText
{
    /// <summary>
    /// Settings provider for floating text appearance and animation behavior.
    /// </summary>
    public interface IFloatingTextSettings
    {
        /// <summary>
        /// Key used to group floating text instances into object pools.
        /// Instances with the same key share a pool (fast path, no allocation).
        /// Return -1 to disable pooling (each spawn creates a unique entity).
        /// </summary>
        int SettingsKey { get; }

        float Lifetime { get; }
        float FloatSpeed { get; }
        Vector2 FloatDirection { get; }

        float FontSize { get; }
        Color Color { get; }
        TMP_FontAsset Font { get; }

        /// <summary>
        /// Evaluates fade alpha over normalized lifetime [0..1].
        /// 1 = fully visible, 0 = invisible.
        /// </summary>
        AnimationCurve FadeCurve { get; }

        /// <summary>
        /// Evaluates scale multiplier over normalized lifetime [0..1].
        /// 1 = normal size.
        /// </summary>
        AnimationCurve ScaleCurve { get; }
    }
}