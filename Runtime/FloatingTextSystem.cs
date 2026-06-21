using UnityEngine;

namespace FloatingText
{
    /// <summary>
    /// Main entry point for spawning floating text popups.
    /// Use <see cref="Spawn"/> to create a floating text at a world position.
    /// </summary>
    public class FloatingTextSystem
    {
        private FloatingTextManager _floatingTextManager;

        public FloatingTextSystem()
        {
            var managerGO = new GameObject("FloatingTextManager");
            managerGO.hideFlags = HideFlags.HideInHierarchy;

            _floatingTextManager = managerGO.AddComponent<FloatingTextManager>();
            _floatingTextManager.Initialize();
        }

        /// <summary>
        /// Spawn a floating text at the given world position.
        /// </summary>
        /// <param name="text">The text to display.</param>
        /// <param name="worldPosition">Position in world space where the text appears.</param>
        public void Spawn(string text, FloatingTextStyleSO style, Vector3 worldPosition)
        {
            var rotation = Quaternion.identity;
            if (Camera.main != null)
            {
                rotation = Quaternion.LookRotation(worldPosition - Camera.main.transform.position);
            }
            _floatingTextManager.Spawn(text, style, worldPosition, rotation);
        }

        public void Spawn(string text, FloatingTextStyleSO style, Vector3 worldPosition, Quaternion worldRotation)
        {
            _floatingTextManager.Spawn(text, style, worldPosition, worldRotation);
        }
    }
}