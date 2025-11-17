using System;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IDamageable
{
    public LayerMask GroundLayerMask;
    public List<Weapon> Weapons = new List<Weapon>();
    // TODO: Move this logic to getting collider Size? 
    public float characterHeight = 2.0f;
    public float characterWidth = 1.0f;

    // TODO: Move to Globals
    public static bool DEBUG_INPUT = true;
    public float GAME_TIME_SCALE => 1.0f * Time.deltaTime;

    public float ColliderHeight => GetComponent<BoxCollider>().size.y;
    public float ColliderWidth => GetComponent<BoxCollider>().size.x;

    private PlayerControllerCommandSystem _commandSystem;
    private PlayerMovementState _movementState;
    private PlayerCombatState _combatState;
    private Weapon _activeWeapon;
    public bool FacingRight
    {
        get
        {
            return _facingRight;
        }
    }

    public bool ShootingRight
    {
        get
        {
            return _movementState.Type == PlayerMovementStateType.WallSliding ? !_facingRight : _facingRight;
        }
    }

    private bool _facingRight = true;
    public float DashSpeed { get; } = 15f;
    public float WalkSpeed { get; } = 5f;
    public float JumpForce { get; } = 25f;
    public float DashTime { get; } = 1f;
    public float MaxWallSlideGravity { get; } = 10;
    public Weapon Weapon => _activeWeapon;

    void Start()
    {
        _commandSystem = new PlayerControllerCommandSystem();
        SetMovementState(new PlayerIdleState(_commandSystem, this));
        _activeWeapon = Weapons[0];
        SetCombatState(_activeWeapon.GetInitialCombatState(_commandSystem, this));
    }

    void Update()
    {
        // TODO: Implement some sort of ignore large frames game wide things?
        if (Time.deltaTime > .05f)
        {
            return;
        }

        _commandSystem.Update(Time.deltaTime);
        _movementState.Update(Time.deltaTime);
        _combatState.Update(Time.deltaTime);
    }

    public void SetMovementState(PlayerMovementState state)
    {
        if (_movementState != null)
        {
            _movementState.ExitState();
        }

        //Debug.Log("New Movement State: " + state.ToSafeString());

        OnMovementStateChange(_movementState, state);
        _movementState = state;
        _movementState.EnterState();
    }

    private void OnMovementStateChange(PlayerMovementState oldState, PlayerMovementState newState)
    {
        switch (newState.Type)
        {
            case PlayerMovementStateType.Idle:
            default:
                break;
        }
    }

    public void SetCombatState(PlayerCombatState state)
    {
        if (_combatState != null)
        {
            _combatState.ExitState();
        }

        OnCombatStateChange(_combatState, state);
        _combatState = state;
        _combatState.EnterState();
    }

    private void OnCombatStateChange(PlayerCombatState oldState, PlayerCombatState newState)
    {
        switch (newState.Type)
        {
            case PlayerCombatStateType.Idle:
            default:
                break;
        }
    }

    public void SetFacing(bool rightFacing)
    {
        _facingRight = rightFacing;
        // TODO: Flip sprite/model
    }


    // IDamagable

    private int _currentHealth = 10;

    public DamagableObjectType GetDamagableObjectType()
    {
        return DamagableObjectType.Player;
    }

    public bool Hit(int damageValue)
    {
        _currentHealth -= damageValue;

        // TODO: Enter Damaged State

        if (_currentHealth <= 0)
        {
            Kill();
        }

        return true;
    }

    public void Kill()
    {

    }

    public int GetCurrentHealth() => _currentHealth;
}