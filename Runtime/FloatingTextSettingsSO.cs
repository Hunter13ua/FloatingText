using UnityEngine;
using TMPro;

namespace FloatingText
{
    /// <summary>
    /// ScriptableObject implementation of <see cref="IFloatingTextSettings"/>.
    /// This is the recommended way to define reusable floating text styles.
    /// Each SO instance automatically gets a unique SettingsKey via its name hash,
    /// so all texts spawned with the same SO share a pool.
    /// </summary>
    [CreateAssetMenu(menuName = "Floating Text/Settings", fileName = "FloatingTextSettings")]
    public class FloatingTextSettingsSO : ScriptableObject, IFloatingTextSettings
    {
        [Header("Animation")]
        [SerializeField] private float _lifetime = 1.5f;
        [SerializeField] private float _floatSpeed = 2f;
        [SerializeField] private Vector2 _floatDirection = new Vector2(0f, 1f);

        [Header("Appearance")]
        [SerializeField] private float _fontSize = 36f;
        [SerializeField] private Color _color = Color.white;
        [SerializeField] private TMP_FontAsset _font;

        [Header("Curves")]
        [SerializeField] private AnimationCurve _fadeCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
        [SerializeField] private AnimationCurve _scaleCurve = AnimationCurve.Constant(0f, 1f, 1f);

        public int SettingsKey => name.GetHashCode();
        public float Lifetime => _lifetime;
        public float FloatSpeed => _floatSpeed;
        public Vector2 FloatDirection => _floatDirection.normalized;
        public float FontSize => _fontSize;
        public Color Color => _color;
        public TMP_FontAsset Font => _font;
        public AnimationCurve FadeCurve => _fadeCurve;
        public AnimationCurve ScaleCurve => _scaleCurve;
    }
}