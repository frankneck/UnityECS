using Unity.Burst;
using Unity.Mathematics;
using Unity.Entities;
using UnityEngine;
using Unity.Entities.UniversalDelegates;
using Random = Unity.Mathematics.Random;
using Unity.Rendering;

[UpdateInGroup(typeof(InitializationSystemGroup))]
public partial struct TankSpawnSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<TankConfig>();
    }


    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        state.Enabled = false;
        
        var em = state.EntityManager; 
        var config = SystemAPI.GetSingleton<TankConfig>();

        var random = new Random(123);

        for (int i = 0; i < config.TankCount; i++)
        {
            var tankEntity = em.Instantiate(config.TankPrefab);

            var color = new URPMaterialPropertyBaseColor
            {
                Value = RandomColor(ref random)
            };

            // Linked entity group is a buffer of struct's values
            var linkedEntities = em.GetBuffer<LinkedEntityGroup>(tankEntity);
            
            foreach (var entity in linkedEntities)
            {
                if (em.HasComponent<URPMaterialPropertyBaseColor>(entity.Value))
                {
                    em.SetComponentData(entity.Value, color);   
                }
            }

            if (i == 0)
            {
                em.AddComponent<PlayerTag>(tankEntity);
                em.AddComponent<PlayerMoveInput>(tankEntity);
                em.AddComponent<FireProjectileTag>(tankEntity);
                em.AddComponentData(tankEntity, new PlayerMoveSpeed { Value = config.PlayerTankSpeed });
            }
            else
            {
                em.AddComponent<EnemyTag>(tankEntity);
            }
        }
    }

    static float4 RandomColor(ref Random random)
    {
        var hue = (random.NextFloat() + 0.6180334005f) % 1;

        return (Vector4) Color.HSVToRGB(hue, 1.0f, 1.0f); 
    }
}

