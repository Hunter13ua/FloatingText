using UnityEngine;

namespace FloatingText
{
    /// <summary>
    /// Main entry point for spawning floating text popups.
    /// Use <see cref="Spawn"/> to create a floating text at a world position.
    /// </summary>
    public static class FloatingTextSystem
    {
        /// <summary>
        /// Spawn a floating text at the given world position.
        /// </summary>
        /// <param name="text">The text to display.</param>
        /// <param name="worldPosition">Position in world space where the text appears.</param>
        public static void Spawn(string text, Vector3 worldPosition)
        {
            // TODO: implement pooling, animation, and cleanup
            Debug.Log($"[FloatingText] Spawned \"{text}\" at {worldPosition}");
        }

        /// <summary>
        /// Spawn a floating text with an optional color.
        /// </summary>
        public static void Spawn(string text, Vector3 worldPosition, Color color)
        {
            // TODO: implement pooling, animation, and cleanup with color
            Debug.Log($"[FloatingText] Spawned \"{text}\" at {worldPosition} with color {color}");
        }
    }
}