using Unity.Entities;
using Unity.Burst;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Entities.UniversalDelegates;
using Unity.Transforms;
using System.Runtime.CompilerServices;

public partial struct TankMovement : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var dt = SystemAPI.Time.DeltaTime;

        foreach (var (transform, entity) in SystemAPI.Query<RefRW<LocalTransform>>().WithAll<Tank>().WithNone<PlayerTag>().WithEntityAccess())
        {
            var pos = transform.ValueRO.Position;
            pos.y = (float) entity.Index;
            var angle = (0.5f + noise.cnoise(pos / 10f)) * 4.0f * math.PI;
            var dir = float3.zero;
            math.sincos(angle, out dir.x, out dir.z);

            transform.ValueRW.Position += dir * dt * 5.0f;
            transform.ValueRW.Rotation = quaternion.Euler(angle);
        } 

        var spin = quaternion.RotateY(dt * math.PI);

        foreach (var tank in SystemAPI.Query<RefRW<Tank>>().WithNone<PlayerTag>())
        {
            var trans = SystemAPI.GetComponentRW<LocalTransform>(tank.ValueRW.Turret);
            trans.ValueRW.Rotation = math.mul(spin, trans.ValueRO.Rotation);   
        }
    }
}

