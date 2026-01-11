using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.VisualScripting.Dependencies.NCalc;

[BurstCompile]
public partial struct CannonBallJob : IJobEntity
{
    public float DeltaTime;
    public EntityCommandBuffer ECB;

    void Execute(Entity entity, ref CannonBall cannonBall, ref LocalTransform transform)
    {
        var gravity = new float3(0.0f, -9.82f, 0.0f);

        transform.Position += cannonBall.Velocity * DeltaTime;

        if (transform.Position.y <= 0.0f)
        {
            ECB.DestroyEntity(entity);
        } 

        cannonBall.Velocity += gravity * DeltaTime;
    }
}