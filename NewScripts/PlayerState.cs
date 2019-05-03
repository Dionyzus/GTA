using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public enum EMoveState
    {
        WALKING,
        RUNNING,
        CROUCHING,
        SNEAKING,
        COVER,
        FALLING
    }

    public enum EWeaponState
    {
        IDLE,
        FIRING,
        AIMING,
        AIMEDFIRING
    }
    bool isInCover = false;
    bool isFalling = false;
    
    public EMoveState MoveState;
    public EWeaponState WeaponState;

    private InputController m_InputController;
    public InputController InputController
    {
        get
        {
            if (m_InputController == null)
            {
                m_InputController = GameManager.Instance.InputController;
            }
            return m_InputController;
        }
    }

    public bool IsFalling
    {
        get
        {
            return isFalling;
        }

        set
        {
            isFalling = value;
        }
    }

    void Awake()
    {
        GameManager.Instance.EventBus.AddListener("CoverToggle", ToggleCover);
    }

    void ToggleCover()
    {
        isInCover = !isInCover;
    }

    void Update()
    {
        SetWeaponState();
        SetMoveState();
    }

    void SetWeaponState()
    {
        WeaponState = EWeaponState.IDLE;
        if (InputController.Fire1)
        {
            WeaponState = EWeaponState.FIRING;
        }
        if (InputController.IsAiming)
        {
            WeaponState = EWeaponState.AIMING;
        }
        if (InputController.Fire1 && InputController.IsAiming)
        {
            WeaponState = EWeaponState.AIMEDFIRING;
        }
    }

    void SetMoveState()
    {
        MoveState = EMoveState.WALKING;

        if (InputController.IsRunning)
        {
            MoveState = EMoveState.RUNNING;
        }
        if (InputController.IsSneaking)
        {
            MoveState = EMoveState.SNEAKING;
        }
        if (InputController.IsCrouched)
        {
            MoveState = EMoveState.CROUCHING;
        }
        if (isInCover)
        {
            MoveState = EMoveState.COVER;
        }
        if(IsFalling)
        {
            MoveState = EMoveState.FALLING;
        }
    }
}
