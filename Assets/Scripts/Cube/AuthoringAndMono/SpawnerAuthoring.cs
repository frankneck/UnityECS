using Unity.Entities;
using UnityEngine;

namespace Cube
{
    public class SpawnerAuthoring : MonoBehaviour
    {    
        public GameObject CubePrefab;    
        
        class SpawnerBaker : Baker<SpawnerAuthoring>
        {        
            public override void Bake(SpawnerAuthoring authoring)        
            {            
                var entity = GetEntity(authoring, TransformUsageFlags.None);            
                
                var spawner = new Spawner
                {
                    CubePrefab = GetEntity(authoring.CubePrefab, TransformUsageFlags.Dynamic)
                };

                AddComponent(entity, spawner);
            }    
        }
    }
    
}
