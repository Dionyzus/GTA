using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerState))]
[RequireComponent(typeof(PlayerHealth))]
public class Player : MonoBehaviour
{

    [System.Serializable]
    public class MouseInput
    {
        public Vector2 Damping;
        public Vector2 Sensitivity;
        public bool LockMouse;
    }

    [SerializeField] Soldier settings;
    [SerializeField] MouseInput MouseControl;
    [SerializeField] AudioController footSteps;
    [SerializeField] float minimumMoveThreshold;

    public float jumpSpeed = 0.5f;
    public float gravity = 30.0f;

    private Vector3 moveDirection = Vector3.zero;

    public PlayerAim playerAim;
    Vector3 previousPosition;

    private PlayerShoot m_PlayerShoot;
    public PlayerShoot PlayerShoot
    {
        get
        {
            if (m_PlayerShoot == null)
                m_PlayerShoot = GetComponent<PlayerShoot>();
            return m_PlayerShoot;
        }
    }

    private CharacterController m_MoveController;
    public CharacterController MoveController
    {
        get
        {
            if (m_MoveController == null)
            {
                m_MoveController = GetComponent<CharacterController>();
            }
            return m_MoveController;
        }
    }

    private WeaponController m_WeaponController;
    public WeaponController WeaponController
    {
        get
        {
            if (m_WeaponController == null)
            {
                m_WeaponController = GetComponent<WeaponController>();
            }
            return m_WeaponController;
        }
    }
    private PlayerState m_PlayerState;
    public PlayerState PlayerState
    {
        get
        {
            if (m_PlayerState == null)
            {
                m_PlayerState = GetComponent<PlayerState>();
            }
            return m_PlayerState;
        }
    }

    private PlayerHealth m_PlayerHealth;
    public PlayerHealth PlayerHealth
    {
        get
        {
            if (m_PlayerHealth == null)
            {
                m_PlayerHealth = GetComponent<PlayerHealth>();
            }
            return m_PlayerHealth;
        }
    }

    InputController playerInput;
    Vector2 mouseInput;

    void Awake()
    {
        playerInput = GameManager.Instance.InputController;
        GameManager.Instance.LocalPlayer = this;
        if (MouseControl.LockMouse)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        gameObject.transform.position = new Vector3(12,14,-15);

    }

    void Update()
    {
        if (!PlayerHealth.IsAlive || GameManager.Instance.IsPaused)
        {
            return;
        }
        Move();
        LookAround();
    }

    void LookAround()
    {
        mouseInput.x = Mathf.Lerp(mouseInput.x, playerInput.MouseInput.x, 1f / MouseControl.Damping.x);
        mouseInput.y = Mathf.Lerp(mouseInput.y, playerInput.MouseInput.y, 1f / MouseControl.Damping.y);


        transform.Rotate(Vector3.up * mouseInput.x * MouseControl.Sensitivity.x);

        playerAim.SetRotation(mouseInput.y * MouseControl.Sensitivity.y);
    }

    void Move()
    {
        float moveSpeed = settings.WalkSpeed;
        if (playerInput.IsRunning)
        {
            moveSpeed = settings.RunSpeed;
        }
        if (playerInput.IsSneaking)
        {
            moveSpeed = settings.SneakSpeed;
        }
        if (playerInput.IsCrouched)
        {
            moveSpeed = settings.CrouchSpeed;
        }
        if (PlayerState.MoveState == PlayerState.EMoveState.COVER)
        {
            moveSpeed = settings.WalkSpeed;
        }
       
        if (!MoveController.isGrounded)
        {

            PlayerState.IsFalling = true;

        }
        if (MoveController.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes
            PlayerState.IsFalling = false;
            moveDirection = new Vector3(playerInput.Horizontal, 0.0f, playerInput.Vertical);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * moveSpeed;

            if (playerInput.Jump)
            {
                moveDirection.y = jumpSpeed;
            }
        }
        if (Vector3.Distance(moveDirection * Time.deltaTime, previousPosition) > minimumMoveThreshold)
        {
            footSteps.Play();
        }

        // Apply gravity
        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);

        // Move the controller
        MoveController.Move(moveDirection * Time.deltaTime);
    }
}
