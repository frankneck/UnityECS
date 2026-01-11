using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateBefore(typeof(TransformSystemGroup))]
public partial struct PlayerLookSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecb = SystemAPI.GetSingleton<EndInitializationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged);
        var config = SystemAPI.GetSingleton<TankConfig>();
        
        foreach (var (input, speed, tank, entity) in SystemAPI.Query<RefRO<PlayerLookInput>, RefRW<PlayerMoveSpeed>, RefRW<Tank>>().WithEntityAccess().WithAll<PlayerTag>())
        {
            speed.ValueRW.Value = config.PlayerTankLookSpeed; 
            float mx = input.ValueRO.inputValue.x;
            float xRotation = mx * SystemAPI.Time.DeltaTime * speed.ValueRO.Value;
            
            var turretTransform = SystemAPI.GetComponentRW<LocalTransform>(tank.ValueRW.Turret);

            if (mx != 0)
            {
                turretTransform.ValueRW.Rotation = math.mul(
                    quaternion.AxisAngle(math.up(), xRotation),
                    turretTransform.ValueRW.Rotation
                );
            }
        }
    } 
}