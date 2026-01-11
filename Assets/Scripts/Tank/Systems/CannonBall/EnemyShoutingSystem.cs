using System.Diagnostics.Contracts;
using System.Threading;
using Mono.Cecil.Cil;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

// This is needed for projectiles 
[UpdateInGroup(typeof(SimulationSystemGroup))]
[UpdateBefore(typeof(TransformSystemGroup))]
public partial struct ShoutingSystem : ISystem
{
    private float timer;

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(Allocator.Temp);

        timer -= SystemAPI.Time.DeltaTime;
        if (timer > 0.0f)
        {
            return;
        }
        timer = 1.0f; 

        var config = SystemAPI.GetSingleton<TankConfig>();
        
        var cannonBallTransform = state.EntityManager.GetComponentData<LocalTransform>(config.CannonBallPrefab);

        foreach (var (tank, color) in SystemAPI.Query<RefRO<Tank>, RefRO<URPMaterialPropertyBaseColor>>().WithAll<EnemyTag>())
        {
            Entity cannonBallEntity = ecb.Instantiate(config.CannonBallPrefab);
            ecb.SetComponent(cannonBallEntity, color.ValueRO);

            var cannonTransform = state.EntityManager.GetComponentData<LocalToWorld>(tank.ValueRO.Cannon);
            cannonBallTransform.Position = cannonTransform.Position;

            ecb.SetComponent(cannonBallEntity, cannonBallTransform);
            
            ecb.SetComponent(cannonBallEntity, new CannonBall
            {
                Velocity = math.normalize(cannonTransform.Up) * config.BallSpeed
            });
        }

        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}