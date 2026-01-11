using Unity.Burst;
using Unity.Mathematics;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.VisualScripting;
using Unity.Jobs;

namespace Cube
{
    
public partial struct SpawnSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<Spawner>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        state.Enabled = false;

        var prefab = SystemAPI.GetSingleton<Spawner>().CubePrefab;
        var instances = state.EntityManager.Instantiate(prefab, 100000, Allocator.Temp);

        var random = new Unity.Mathematics.Random(123);

        foreach (var entity in instances)
        {
            var transform = SystemAPI.GetComponentRW<LocalTransform>(entity);
            transform.ValueRW.Position = random.NextFloat3(new float3(1000, 1000, 1000));
        }
    }

}
}