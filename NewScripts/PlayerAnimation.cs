using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;

    private PlayerAim m_PlayerAim;
    private PlayerAim playerAim
    {
        get
        {
            if (m_PlayerAim == null)
            {
                m_PlayerAim = GameManager.Instance.LocalPlayer.playerAim;
            }
            return m_PlayerAim;
        }
    }
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
  
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsPaused)
        {
            return;
        }
        animator.SetFloat("Vertical", GameManager.Instance.InputController.Vertical);
        animator.SetFloat("Horizontal", GameManager.Instance.InputController.Horizontal);

        animator.SetBool("IsFalling", GameManager.Instance.LocalPlayer.PlayerState.IsFalling);

        animator.SetBool("EnterCar", GameManager.Instance.InputController.enterCar);
        animator.SetBool("ExitCar", GameManager.Instance.InputController.exitCar);

        animator.SetBool("IsReloading",GameManager.Instance.InputController.Reload);
        animator.SetBool("WeaponSwitch", GameManager.Instance.InputController.MouseWheelDown || GameManager.Instance.InputController.MouseWheelUp);
        animator.SetBool("IsJump", GameManager.Instance.InputController.Jump);
        animator.SetBool("IsRunning", GameManager.Instance.InputController.IsRunning);
        animator.SetBool("IsSneaking", GameManager.Instance.InputController.IsSneaking);
        animator.SetBool("IsCrouched", GameManager.Instance.InputController.IsCrouched);
        animator.SetBool("IsFiring", GameManager.Instance.InputController.Fire1);
        animator.SetBool("IsAiming", GameManager.Instance.LocalPlayer.PlayerState.WeaponState == PlayerState.EWeaponState.AIMING ||
            GameManager.Instance.LocalPlayer.PlayerState.WeaponState == PlayerState.EWeaponState.AIMEDFIRING);
        animator.SetFloat("AimAngle", playerAim.GetAngle());

        animator.SetBool("IsInCover", GameManager.Instance.LocalPlayer.PlayerState.MoveState == PlayerState.EMoveState.COVER);
    }
}
