using Unity.Entities;
using Unity.Burst;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Collections;

[UpdateInGroup(typeof(SimulationSystemGroup), OrderLast = true)]
[UpdateAfter(typeof(EndSimulationEntityCommandBufferSystem))]
public partial struct ResetSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(Allocator.Temp);

        foreach(var (tag, entity)  in SystemAPI.Query<FireProjectileTag>().WithEntityAccess())
        {
            ecb.SetComponentEnabled<FireProjectileTag>(entity, false);   
        }

        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}