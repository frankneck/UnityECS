using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.InputSystem;

[UpdateBefore(typeof(TransformSystemGroup))]
public partial struct PlayerMoveSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var config = SystemAPI.GetSingleton<TankConfig>();

        JobHandle handle = new PlayerMoveJob 
        {
            DeltaTime = SystemAPI.Time.DeltaTime,
            Speed = config.PlayerTankSpeed
        }.ScheduleParallel(state.Dependency);

        state.Dependency = handle;

        
        // foreach (var (transform, input, speed, tank) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<PlayerMoveInput>, RefRW<PlayerMoveSpeed>>().WithEntityAccess().WithAll<PlayerTag>())
        // {
        //     speed.ValueRW.Value = config.PlayerTankSpeed;

        //     float3 movement = new float3(input.ValueRO.inputValue.x, 0.0f, input.ValueRO.inputValue.y)
        //         * SystemAPI.Time.DeltaTime 
        //         * speed.ValueRW.Value;
            
        //     transform.ValueRW.Position += movement;
        // }
    } 
}

[BurstCompile]
public partial struct PlayerMoveJob : IJobEntity
{
    public float DeltaTime;
    public float Speed;

    [BurstCompile]
    private void Execute(ref LocalTransform transform, in PlayerMoveInput moveInput, PlayerMoveSpeed speed)
    {
        speed.Value = Speed;
        transform.Position.xz += moveInput.inputValue * speed.Value * DeltaTime;

        if (math.length(moveInput.inputValue) > float.Epsilon)
        {
            var forward = new float3(moveInput.inputValue.x, 0f, moveInput.inputValue.y);
            transform.Rotation = quaternion.LookRotation(forward, math.up());
        }
    }
}
