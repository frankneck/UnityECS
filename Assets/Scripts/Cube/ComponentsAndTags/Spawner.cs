using Unity.Entities;

namespace Cube
{
    struct Spawner : IComponentData 
    {
        public Entity CubePrefab;
    }    
}
