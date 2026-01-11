using UnityEngine;
using Unity.Mathematics;
using Unity.Entities;
using Unity.Entities.UniversalDelegates;

namespace Cube
{
    // This class for Inspector
    public class RotationSpeedAuthoring : MonoBehaviour
    {
        public float DegreesPerSecond = 360f;
    }

    class RotationBaker : Baker<RotationSpeedAuthoring>
    {
        // This code execute once
        public override void Bake(RotationSpeedAuthoring authoring)
        {
            // getting entity associated with authoring + TransformComponnets 
            var entity = GetEntity(authoring, TransformUsageFlags.Dynamic);

            var rotationSpeed = new RotationSpeed
            {
                RadiansPerSecond = math.radians(authoring.DegreesPerSecond)
            };

            // Adding the rotation component to entity
            AddComponent(entity, rotationSpeed);
        }
    }
}

