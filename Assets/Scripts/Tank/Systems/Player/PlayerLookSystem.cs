using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[UpdateBefore(typeof(TransformSystemGroup))]
public partial struct PlayerLookSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        var config = SystemAPI.GetSingleton<TankConfig>();


        foreach (var (input, speed, tank) in SystemAPI.Query<RefRO<PlayerLookInput>, RefRW<PlayerMoveSpeed>, RefRW<Tank>>().WithAll<PlayerTag>())
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

            float my = input.ValueRO.inputValue.y;

            var camera = Camera.main.transform;
            
            if (my != 0)
            {
                float yCamera = camera.rotation.eulerAngles.x;
                if (yCamera > 180) yCamera -= 360;
                yCamera = Mathf.Clamp(
                    yCamera + -my * 10f,
                    speed.ValueRO.Value,
                    speed.ValueRO.Value
                );

                camera.rotation = Quaternion.Euler(
                    yCamera,
                    camera.rotation.eulerAngles.y,
                    camera.rotation.eulerAngles.z
                );
            }

        }
    } 
}