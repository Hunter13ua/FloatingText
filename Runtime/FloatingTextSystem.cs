using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using FloatingText.Pooling;

namespace FloatingText
{
    /// <summary>
    /// Main entry point for spawning floating text popups.
    /// Can be called from any thread.
    /// </summary>
    public static class FloatingTextSystem
    {
        /// <summary>
        /// Spawn a floating text at a world position.
        /// </summary>
        /// <param name="text">The text to display.</param>
        /// <param name="worldPosition">Position in world space.</param>
        /// <param name="settings">Visual and animation settings. Must implement <see cref="IFloatingTextSettings"/>.</param>
        public static void Spawn(string text, Vector3 worldPosition, IFloatingTextSettings settings)
        {
            if (!SpawnRequestQueue.Queue.IsCreated)
            {
                Debug.LogWarning("[FloatingText] System not initialized. Add FloatingTextWorldAuthoring to your scene.");
                return;
            }

            SpawnRequestQueue.Queue.Enqueue(new SpawnRequest
            {
                Position = new float3(worldPosition.x, worldPosition.y, worldPosition.z),
                Text = new FixedString64Bytes(text),
                Color = new float4(settings.Color.r, settings.Color.g, settings.Color.b, settings.Color.a),
                FontSize = settings.FontSize,
                SettingsKey = settings.SettingsKey,
                Lifetime = settings.Lifetime,
                FloatSpeed = settings.FloatSpeed,
                FloatDirection = new float2(settings.FloatDirection.x, settings.FloatDirection.y)
            });
        }
    }
}