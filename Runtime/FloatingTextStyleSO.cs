using UnityEngine;

namespace FloatingText
{
    [CreateAssetMenu(fileName = "FloatingTextStyle", menuName = "Scriptable Objects/Floating Text Style")]
    public class FloatingTextStyleSO : ScriptableObject
    {
        public Color Color = Color.white;
        
        public float Lifetime = 1f;
        
        public float Speed = 1f;
        
        public Vector3 Scale = Vector3.one;

        public FloatingTextAnimationType AnimationType = FloatingTextAnimationType.Normal;
    }
}
