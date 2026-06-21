using UnityEditor;
using UnityEngine;

namespace FloatingText.Editor
{
    [CustomEditor(typeof(FloatingTextStyleSO))]
    public class FloatingTextStyleSOEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var style = (FloatingTextStyleSO)target;

            EditorGUILayout.LabelField("Floating Text Style", EditorStyles.boldLabel);
            EditorGUILayout.Space(6);

            EditorGUILayout.LabelField("Visual", EditorStyles.boldLabel);
            style.Color = EditorGUILayout.ColorField("Color", style.Color);
            style.Scale = EditorGUILayout.FloatField("Scale", style.Scale);

            EditorGUILayout.Space(6);

            EditorGUILayout.LabelField("Motion", EditorStyles.boldLabel);
            style.Speed = EditorGUILayout.FloatField("Speed", style.Speed);
            style.AnimationType =
                (FloatingTextAnimationType)EditorGUILayout.EnumPopup("Animation", style.AnimationType);

            EditorGUILayout.Space(6);

            EditorGUILayout.LabelField("Lifetime", EditorStyles.boldLabel);
            style.Lifetime = EditorGUILayout.FloatField("Lifetime", style.Lifetime);

            EditorGUILayout.Space(10);

            // validation
            if (style.Lifetime <= 0)
                EditorGUILayout.HelpBox("Lifetime must be > 0", MessageType.Warning);

            if (style.Scale <= 0)
                EditorGUILayout.HelpBox("Scale must be > 0", MessageType.Warning);

            EditorGUILayout.Space(5);

            if (GUI.changed)
            {
                EditorUtility.SetDirty(style);
            }
        }
    }
}