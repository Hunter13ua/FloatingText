using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using FloatingText.Components;
using FloatingText.Pooling;
using FloatingText.Authoring;

namespace FloatingText.Systems
{
    /// <summary>
    /// Processes pending spawn requests from <see cref="SpawnRequestQueue"/>
    /// and creates or recycles floating text entities.
    /// </summary>
    [UpdateInGroup(typeof(PresentationSystemGroup))]
    public partial class FloatingTextSpawnSystem : SystemBase
    {
        protected override void OnCreate()
        {
            RequireForUpdate<FloatingTextPool>();
        }

        protected override void OnUpdate()
        {
            if (!SpawnRequestQueue.Queue.IsCreated || SpawnRequestQueue.Queue.IsEmpty())
                return;

            var pool = SystemAPI.GetSingletonRW<FloatingTextPool>();
            if (pool.ValueRO.IsInitialized == 0)
                return;

            var ecb = new EntityCommandBuffer(Allocator.Temp);
            var poolsMap = pool.ValueRW.Pools;

            // Check if we have a prefab for instantiation
            bool hasPrefab = SystemAPI.HasSingleton<FloatingTextPrefabHolder>();
            Entity prefabEntity = Entity.Null;
            if (hasPrefab)
            {
                prefabEntity = SystemAPI.GetSingleton<FloatingTextPrefabHolder>().Prefab;
            }

            while (SpawnRequestQueue.Queue.TryDequeue(out SpawnRequest request))
            {
                Entity entity;
                bool isPooled = request.SettingsKey != -1;

                if (isPooled && poolsMap.ContainsKey(request.SettingsKey) && !poolsMap[request.SettingsKey].IsEmpty())
                {
                    // Recycle from pool
                    entity = poolsMap[request.SettingsKey].Dequeue();

                    // Move to spawn position
                    ecb.SetComponent(entity, LocalTransform.FromPosition(request.Position));
                    ecb.RemoveComponent<Disabled>(entity);

                    // Apply per-spawn data
                    ecb.SetComponent(entity, new FloatingTextData
                    {
                        Text = request.Text,
                        Color = request.Color,
                        FontSize = request.FontSize
                    });
                    ecb.SetComponent(entity, new FloatingTextAnimation
                    {
                        Lifetime = request.Lifetime,
                        Elapsed = 0f,
                        FloatSpeed = request.FloatSpeed,
                        FloatDirection = request.FloatDirection,
                        AlphaFactor = 1f,
                        ScaleFactor = 1f
                    });
                    ecb.SetComponent(entity, new FloatingTextPooled
                    {
                        SettingsKey = request.SettingsKey,
                        IsInPool = false
                    });
                }
                else if (hasPrefab && prefabEntity != Entity.Null)
                {
                    // Instantiate from prefab
                    entity = ecb.Instantiate(prefabEntity);
                    ecb.SetComponent(entity, LocalTransform.FromPosition(request.Position));
                    ecb.AddComponent(entity, new FloatingTextTag());

                    // Apply per-spawn data
                    ecb.SetComponent(entity, new FloatingTextData
                    {
                        Text = request.Text,
                        Color = request.Color,
                        FontSize = request.FontSize
                    });
                    ecb.SetComponent(entity, new FloatingTextAnimation
                    {
                        Lifetime = request.Lifetime,
                        Elapsed = 0f,
                        FloatSpeed = request.FloatSpeed,
                        FloatDirection = request.FloatDirection,
                        AlphaFactor = 1f,
                        ScaleFactor = 1f
                    });
                    ecb.SetComponent(entity, new FloatingTextPooled
                    {
                        SettingsKey = request.SettingsKey,
                        IsInPool = false
                    });
                }
                else
                {
                    // No prefab — create a bare entity (no render, but data exists)
                    entity = ecb.CreateEntity();
                    ecb.AddComponent(entity, LocalTransform.FromPosition(request.Position));
                    ecb.AddComponent(entity, new PostTransformMatrix { Value = float4x4.Scale(1f) });
                    ecb.AddComponent(entity, new FloatingTextTag());
                    ecb.AddComponent(entity, new FloatingTextData
                    {
                        Text = request.Text,
                        Color = request.Color,
                        FontSize = request.FontSize
                    });
                    ecb.AddComponent(entity, new FloatingTextAnimation
                    {
                        Lifetime = request.Lifetime,
                        Elapsed = 0f,
                        FloatSpeed = request.FloatSpeed,
                        FloatDirection = request.FloatDirection,
                        AlphaFactor = 1f,
                        ScaleFactor = 1f
                    });
                    ecb.AddComponent(entity, new FloatingTextPooled
                    {
                        SettingsKey = request.SettingsKey,
                        IsInPool = false
                    });
                }
            }

            ecb.Playback(EntityManager);
        }
    }
}