using NUnit.Framework;
using Unity.Entities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[UpdateInGroup(typeof(InitializationSystemGroup), OrderLast = true)]
public partial class GetPlayerInputSystem : SystemBase
{
    private InputSystem_Actions _inputActions;
    private Entity _playerEntity;

    protected override void OnCreate()
    {
        RequireForUpdate<PlayerTag>();
        RequireForUpdate<PlayerMoveInput>();

        _inputActions = new InputSystem_Actions();
    }

    protected override void OnStartRunning()
    {
        _inputActions.Enable();        
        _inputActions.Player.Jump.performed += OnShoot;
        _playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();        
    }

    protected override void OnUpdate()
    {
        var curMoveInput = _inputActions.Player.Move.ReadValue<Vector2>();
        var curLookInput = _inputActions.Player.Look.ReadValue<Vector2>();
        SystemAPI.SetSingleton(new PlayerMoveInput { inputValue = curMoveInput });
        SystemAPI.SetSingleton(new PlayerLookInput { inputValue = curLookInput });
    }

    protected override void OnStopRunning()
    {
        _inputActions.Disable();
        _inputActions.Player.Jump.performed -= OnShoot;
        _playerEntity = Entity.Null;
    }

    private void OnShoot(InputAction.CallbackContext ctx)
    {
        if (!SystemAPI.Exists(_playerEntity))
            return;
        
        SystemAPI.SetComponentEnabled<FireProjectileTag>(_playerEntity, true);
    }
}
