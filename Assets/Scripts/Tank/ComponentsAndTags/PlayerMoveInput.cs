using Unity.Entities;
using Unity.Mathematics;

public struct PlayerMoveInput : IComponentData
{
    public float2 inputValue;
}

public struct PlayerMoveSpeed : IComponentData
{
    public float Value;
}

public struct PlayerLookInput : IComponentData
{
    public float2 inputValue;
}

public struct PlayerLookSpeed : IComponentData
{
    public float Value;
}

public struct FireProjectileTag : IComponentData, IEnableableComponent {}