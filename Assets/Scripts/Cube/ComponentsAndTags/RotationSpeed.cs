using Unity.Entities;

namespace Cube
{
    public struct RotationSpeed : IComponentData  
    {
        public float RadiansPerSecond; // how quickly the entities rotates  
    }
}