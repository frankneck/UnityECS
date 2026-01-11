using Unity.Entities;
using Unity.Burst;
using Unity.VisualScripting;

public partial struct CannonBallSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        // ecb is a special entity command buffer that has cool commands like DestroyEntity etc
        // in last simulatin phase 
        // ecb is a manager of command buffer
        var ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();  

        var cannonBallJob = new CannonBallJob
        {
            // executes at the end of a frame
            ECB = ecb.CreateCommandBuffer(state.WorldUnmanaged),
            DeltaTime = SystemAPI.Time.DeltaTime   
        }; 

        cannonBallJob.Schedule();
    }
}