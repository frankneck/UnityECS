using Unity.Entities;
using UnityEngine;

public class TankConfigAuthoring : MonoBehaviour
{
    public GameObject TankPrefab;
    public GameObject CannonBallPrefab;
    public int TankCount;
    public float BallSpeed;
    public float PlayerTankSpeed;
    public float PlayerTankLookSpeed;

    class TankConfigBaker : Baker<TankConfigAuthoring>
    {
        public override void Bake(TankConfigAuthoring authoring)
        {
            var entity = GetEntity(authoring, TransformUsageFlags.None);

            AddComponent(entity, new TankConfig
            {
                TankPrefab = GetEntity(authoring.TankPrefab, TransformUsageFlags.Dynamic),
                CannonBallPrefab = GetEntity(authoring.CannonBallPrefab, TransformUsageFlags.Dynamic),
                TankCount = authoring.TankCount,
                BallSpeed = authoring.BallSpeed,
                PlayerTankSpeed = authoring.PlayerTankSpeed,
                PlayerTankLookSpeed = authoring.PlayerTankLookSpeed 
            });
        }
    }
}