using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

    public float Vertical;
    public float Horizontal;
    public Vector2 MouseInput;
    public bool Fire1;
    public bool Reload;
    public bool IsWalking;
    public bool IsRunning;
    public bool IsCrouched;
    public bool IsSneaking;
    public bool IsAiming;
    public bool MouseWheelUp;
    public bool MouseWheelDown;
    public bool CoverToggle;
    public bool Escape;
    public bool Jump;
    public bool Action;
    public bool exitCar;
    public bool enterCar;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    void Update()
    {
        if (EnterCar.InCar)
        {
            exitCar = Input.GetKey(KeyCode.L);
        }
        if (EnterCar.CanEnter)
        {
            enterCar = Input.GetKeyDown(KeyCode.Return);
        }
        Action = Input.GetKeyDown(KeyCode.E);
        Escape = Input.GetKey(KeyCode.Escape);
        Vertical = Input.GetAxis("Vertical");
        Horizontal = Input.GetAxis("Horizontal");
        MouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Fire1 = Input.GetButton("Fire1");
        Reload = Input.GetKeyDown(KeyCode.R);
        IsRunning = Input.GetKey(KeyCode.LeftShift);
        IsCrouched = Input.GetKey(KeyCode.C);
        IsSneaking = Input.GetKey(KeyCode.V);
        IsAiming = Input.GetButton("Fire2");
        CoverToggle = Input.GetKeyDown(KeyCode.F);
        Jump = Input.GetKeyDown(KeyCode.Space);

        if (IsAiming)
        {
            Vertical = 0;
            Horizontal = 0;
        }
        MouseWheelUp = Input.GetAxis("Mouse ScrollWheel") > 0;
        MouseWheelDown = Input.GetAxis("Mouse ScrollWheel") < 0;
    }
}
