using Unity.Entities;
using Unity.Rendering;
using UnityEngine;

public class CannonBallAuthoring : MonoBehaviour
{
    public float Veloctiy;
    class Baker : Baker<CannonBallAuthoring>
    {
        public override void Bake(CannonBallAuthoring authoring)
        {
            var entity = GetEntity(authoring, TransformUsageFlags.Dynamic);
            AddComponent<CannonBall>(entity);
            AddComponent<URPMaterialPropertyBaseColor>(entity);
            

        }
    }
}