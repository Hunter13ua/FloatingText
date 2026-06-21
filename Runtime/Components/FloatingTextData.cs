using Unity.Entities;

namespace FloatingText.Components
{
    /// <summary>
    /// Core data for a floating text entity.
    /// </summary>
    public struct FloatingTextData : IComponentData
    {
        /// <summary>
        /// Fixed string buffer size — texts longer than this will be truncated.
        /// </summary>
        public const int MaxTextLength = 64;

        /// <summary>
        /// The text to display (stored as a fixed string via UTF-8 bytes).
        /// </summary>
        public Unity.Collections.FixedString64Bytes Text;

        /// <summary>
        /// RGBA color packed into a single uint (R8G8B8A8).
        /// </summary>
        public Unity.Mathematics.float4 Color;

        /// <summary>
        /// Font size in world-space units.
        /// </summary>
        public float FontSize;
    }

    /// <summary>
    /// Animation state for a floating text entity.
    /// </summary>
    public struct FloatingTextAnimation : IComponentData
    {
        /// <summary>
        /// Maximum lifetime in seconds.
        /// </summary>
        public float Lifetime;

        /// <summary>
        /// Time elapsed since spawn, in seconds.
        /// </summary>
        public float Elapsed;

        /// <summary>
        /// Upward float speed.
        /// </summary>
        public float FloatSpeed;

        /// <summary>
        /// Horizontal drift (x) and vertical direction (y) as a normalized vector.
        /// </summary>
        public Unity.Mathematics.float2 FloatDirection;

        /// <summary>
        /// How to interpret alpha (ignored, EaseInOut, etc.) —
        /// stored as a BlobAsset reference or we can evaluate curves on the main thread.
        /// For simplicity, we store a combined factor that gets updated by the system.
        /// </summary>
        public float AlphaFactor;

        /// <summary>
        /// Scale multiplier — the AnimationSystem sets this each frame.
        /// </summary>
        public float ScaleFactor;
    }

    /// <summary>
    /// Pooling tag. Present on entities that belong to a pool group.
    /// If SettingsKey is -1, the entity is not pooled (destroyed on cleanup).
    /// </summary>
    public struct FloatingTextPooled : IComponentData
    {
        /// <summary>
        /// The settings key this entity was spawned with.
        /// Used to return it to the correct pool.
        /// </summary>
        public int SettingsKey;

        /// <summary>
        /// True while the entity is sitting in the pool (inactive).
        /// </summary>
        public bool IsInPool;
    }

    /// <summary>
    /// Tag component marking an entity as a floating text.
    /// Used to query all floating text entities in systems.
    /// </summary>
    public struct FloatingTextTag : IComponentData { }
}