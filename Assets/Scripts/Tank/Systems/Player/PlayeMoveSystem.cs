using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.InputSystem;

[UpdateBefore(typeof(TransformSystemGroup))]
public partial struct PlayerMoveSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecb = SystemAPI.GetSingleton<EndInitializationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged);
        var config = SystemAPI.GetSingleton<TankConfig>();
        
        foreach (var (transform, input, speed, tank) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<PlayerMoveInput>, RefRW<PlayerMoveSpeed>>().WithEntityAccess().WithAll<PlayerTag>())
        {
            ecb.SetComponent(tank, new PlayerMoveSpeed { Value = config.PlayerTankSpeed });

            float3 movement = new float3(input.ValueRO.inputValue.x, 0.0f, input.ValueRO.inputValue.y)
                * SystemAPI.Time.DeltaTime 
                * speed.ValueRW.Value;
            
            transform.ValueRW.Position += movement;
        }
    } 
}
