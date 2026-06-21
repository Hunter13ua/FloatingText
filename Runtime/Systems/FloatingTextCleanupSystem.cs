using Unity.Collections;
using Unity.Entities;
using FloatingText.Components;
using FloatingText.Pooling;

namespace FloatingText.Systems
{
    /// <summary>
    /// Destroys or recycles floating text entities whose lifetime has expired.
    /// </summary>
    [UpdateInGroup(typeof(PresentationSystemGroup))]
    [UpdateAfter(typeof(FloatingTextAnimationSystem))]
    public partial class FloatingTextCleanupSystem : SystemBase
    {
        protected override void OnCreate()
        {
            RequireForUpdate<FloatingTextPool>();
        }

        protected override void OnUpdate()
        {
            var pool = SystemAPI.GetSingletonRW<FloatingTextPool>();
            if (pool.ValueRO.IsInitialized == 0)
                return;

            var ecb = new EntityCommandBuffer(Allocator.Temp);
            var pools = pool.ValueRW.Pools;

            Entities
                .WithNone<Disabled>()
                .ForEach((Entity entity, ref FloatingTextAnimation anim, ref FloatingTextPooled pooled) =>
                {
                    if (anim.Elapsed < anim.Lifetime)
                        return;

                    if (pooled.SettingsKey != -1 && !pooled.IsInPool)
                    {
                        // Return to pool
                        int key = pooled.SettingsKey;

                        if (!pools.ContainsKey(key))
                        {
                            pools[key] = new NativeQueue<Entity>(Allocator.Persistent);
                        }

                        pools[key].Enqueue(entity);

                        pooled.IsInPool = true;

                        // Disable / hide
                        ecb.AddComponent<Disabled>(entity);
                    }
                    else
                    {
                        // Not pooled — destroy
                        ecb.DestroyEntity(entity);
                    }
                })
                .Run();

            ecb.Playback(EntityManager);
        }
    }
}