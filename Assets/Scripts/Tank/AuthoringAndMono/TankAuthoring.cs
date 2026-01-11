using Unity.Entities;
using UnityEngine;

public class TankAuthoring : MonoBehaviour
{
    public GameObject Turret;
    public GameObject Cannon;

    class TankBaker : Baker<TankAuthoring>
    {
        public override void Bake(TankAuthoring authoring)
        {
            var entity = GetEntity(authoring, TransformUsageFlags.Dynamic);

            AddComponent(entity, new Tank
            {
                Cannon = GetEntity(authoring.Cannon, TransformUsageFlags.Dynamic),
                Turret = GetEntity(authoring.Turret, TransformUsageFlags.Dynamic)
            });
        }
    }
}
