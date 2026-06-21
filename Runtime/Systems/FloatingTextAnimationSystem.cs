using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using FloatingText.Components;

namespace FloatingText.Systems
{
    /// <summary>
    /// Animates all active floating text entities each frame:
    /// moves them upward, updates alpha/scale based on curves.
    /// </summary>
    [UpdateInGroup(typeof(PresentationSystemGroup))]
    [UpdateAfter(typeof(FloatingTextSpawnSystem))]
    public partial class FloatingTextAnimationSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            float deltaTime = SystemAPI.Time.DeltaTime;

            Entities
                .WithNone<Disabled>()
                .ForEach((ref FloatingTextAnimation anim, ref LocalTransform transform, ref PostTransformMatrix postMatrix) =>
                {
                    anim.Elapsed += deltaTime;

                    // Normalized time [0..1]
                    float t = math.saturate(anim.Elapsed / anim.Lifetime);

                    // Fade: ease-out quadratic
                    anim.AlphaFactor = 1f - t * t;

                    // Scale: small bump then settle
                    float scale = 1f + (1f - t) * 0.15f;
                    anim.ScaleFactor = scale;

                    // Movement: float upward + horizontal drift
                    float3 moveDelta = new float3(
                        anim.FloatDirection.x * anim.FloatSpeed * deltaTime,
                        anim.FloatDirection.y * anim.FloatSpeed * deltaTime,
                        0f
                    );
                    transform.Position += moveDelta;

                    // Apply scale
                    postMatrix.Value = float4x4.Scale(scale, scale, scale);
                })
                .ScheduleParallel();
        }
    }
}