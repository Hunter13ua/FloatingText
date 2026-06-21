using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace FloatingText.Pooling
{
    /// <summary>
    /// Singleton component holding the floating text object pool.
    /// Maps SettingsKey -> queue of recycled entities.
    /// </summary>
    public struct FloatingTextPool : IComponentData
    {
        /// <summary>
        /// Mapping from SettingsKey to a queue of reusable entities.
        /// </summary>
        public NativeParallelHashMap<int, NativeQueue<Entity>> Pools;

        /// <summary>
        /// Whether the pool has been initialized.
        /// </summary>
        public byte IsInitialized;
    }

    /// <summary>
    /// A spawn request enqueued by the public API and processed by SpawnSystem.
    /// </summary>
    public struct SpawnRequest
    {
        public float3 Position;
        public FixedString64Bytes Text;
        public float4 Color;
        public float FontSize;
        public int SettingsKey;
        public float Lifetime;
        public float FloatSpeed;
        public float2 FloatDirection;
    }

    /// <summary>
    /// Thread-safe bridge between the static Spawn() API and the ECS SpawnSystem.
    /// </summary>
    public static class SpawnRequestQueue
    {
        internal static NativeQueue<SpawnRequest> Queue;

        internal static void Initialize()
        {
            if (!Queue.IsCreated)
                Queue = new NativeQueue<SpawnRequest>(Allocator.Persistent);
        }

        internal static void Dispose()
        {
            if (Queue.IsCreated)
            {
                Queue.Dispose();
            }
        }
    }
}