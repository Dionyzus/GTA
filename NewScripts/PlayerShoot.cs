using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerShoot : WeaponController {

    bool IsPlayerAlive;

    void Start()
    {
        IsPlayerAlive = true;
        GetComponent<Player>().PlayerHealth.OnDeath += PlayerHealth_OnDeath;
    }

    private void PlayerHealth_OnDeath()
    {
        IsPlayerAlive = false;
    }

    void Update()
    {
        if(!IsPlayerAlive || GameManager.Instance.IsPaused)
        {
            return;
        }
        if(GameManager.Instance.InputController.MouseWheelDown)
        {
            SwitchWeapon(1);
        }
        if (GameManager.Instance.InputController.MouseWheelUp)
        {
            SwitchWeapon(-1);
        }

        if(GameManager.Instance.LocalPlayer.PlayerState.MoveState==PlayerState.EMoveState.RUNNING)
        {
            return;
        }

        if(!CanFire)
        {
            return;
        }

        if((GameManager.Instance.InputController.Fire1) || (GameManager.Instance.InputController.IsAiming && GameManager.Instance.InputController.Fire1))
        {
            ActiveWeapon.Fire();
        }

        if(GameManager.Instance.InputController.Reload)
        {
            ActiveWeapon.Reload();
        }

        /*if(GameManager.Instance.InputController.IsAiming && GameManager.Instance.InputController.Fire1)
        {
            sniperRifle.Fire();
        }*/

        /*if (GameManager.Instance.InputController.IsAiming && GameManager.Instance.InputController.Fire1)
        {
            pistol.Fire();
        }*/
    }

}
