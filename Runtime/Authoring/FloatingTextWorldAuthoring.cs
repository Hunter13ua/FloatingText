using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using FloatingText.Pooling;

namespace FloatingText.Authoring
{
    /// <summary>
    /// Attach this to a GameObject in your scene (or a sub-scene) to initialize
    /// the floating text pool. This must exist for the system to work.
    /// </summary>
    public class FloatingTextWorldAuthoring : MonoBehaviour
    {
        [Header("Pool Initialization")]
        [Tooltip("Optional: a prefab that has all required TMP rendering components baked onto it. " +
                 "If null, the system will create entities without render components (spawn request " +
                 "will still be logged).")]
        public GameObject FloatingTextPrefab;
    }

    /// <summary>
    /// Unmanaged component holding the baked floating text prefab.
    /// </summary>
    public struct FloatingTextPrefabHolder : IComponentData
    {
        public Entity Prefab;
    }

    /// <summary>
    /// Baker for <see cref="FloatingTextWorldAuthoring"/>.
    /// Creates the pool singleton and bakes the prefab archetype.
    /// </summary>
    public class FloatingTextWorldBaker : Baker<FloatingTextWorldAuthoring>
    {
        public override void Bake(FloatingTextWorldAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);

            // Create the pool singleton
            var pool = new FloatingTextPool
            {
                Pools = new NativeParallelHashMap<int, NativeQueue<Entity>>(16, Allocator.Persistent),
                IsInitialized = 1
            };

            AddComponent(entity, pool);

            // If a prefab was provided, bake it and store as a prefab entity
            if (authoring.FloatingTextPrefab != null)
            {
                var prefabEntity = GetEntity(authoring.FloatingTextPrefab, TransformUsageFlags.Dynamic);
                AddComponent(entity, new FloatingTextPrefabHolder
                {
                    Prefab = prefabEntity
                });
            }
        }
    }
}