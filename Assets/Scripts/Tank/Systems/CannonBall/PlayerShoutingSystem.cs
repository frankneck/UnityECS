using Unity.Entities;
using Unity.Burst;
using Unity.Collections;
using UnityEditor.VersionControl;
using Unity.Transforms;
using Unity.Rendering;

public partial struct PlayerShoutingSystem : ISystem
{   
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<FireProjectileTag>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged); 
        var config = SystemAPI.GetSingleton<TankConfig>();
        // readonly
        var localToWorldLookup = SystemAPI.GetComponentLookup<LocalToWorld>(true);

        foreach(var (tank, color) in SystemAPI.Query<RefRO<Tank>, RefRO<URPMaterialPropertyBaseColor>>().WithAll<FireProjectileTag>())
        {
            var cannonTransform = localToWorldLookup[tank.ValueRO.Cannon];
            var projectile = ecb.Instantiate(config.CannonBallPrefab);

            ecb.SetComponent(projectile, LocalTransform.FromPosition(cannonTransform.Position));
            ecb.SetComponent(projectile, color.ValueRO);
            ecb.SetComponent(projectile, new CannonBall
            {
                Velocity = config.BallSpeed
            });
        }
    }  
} 