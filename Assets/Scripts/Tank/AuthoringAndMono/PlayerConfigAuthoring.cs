using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerConfigAuthoring : MonoBehaviour
{
    public InputAction MoveAction;

    class PlayerConfigBaker : Baker<PlayerConfigAuthoring>
    {
        public override void Bake(PlayerConfigAuthoring authoring)
        {
            var entity = GetEntity(authoring, TransformUsageFlags.None);

            // Initialization of player input component
            AddComponent(entity, new PlayerMoveInput 
            {
                inputValue = float2.zero 
            });
        }
    }
} 