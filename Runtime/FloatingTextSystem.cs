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
        public void Spawn(string text, Vector3 worldPosition)
        {
            _floatingTextManager.Spawn(worldPosition, text);
        }
    }
}