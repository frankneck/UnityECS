using Unity.Transforms;
using Unity.Entities;
using Unity.Burst;
using UnityEngineInternal;

namespace Cube
{
    public partial struct RotationCubeSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
    
            foreach (var (transform, rotationSpeed) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotationSpeed>>())
            {
                var radians = rotationSpeed.ValueRO.RadiansPerSecond * deltaTime;
                transform.ValueRW = transform.ValueRW.RotateY(radians);
            } 
        }
    }
}
