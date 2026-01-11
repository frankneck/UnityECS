using Unity.Entities;

public struct TankConfig : IComponentData
{
    public Entity TankPrefab;
    public Entity CannonBallPrefab;
    public int TankCount;
    public float BallSpeed;
    public float PlayerTankSpeed;
    public float PlayerTankLookSpeed;
}