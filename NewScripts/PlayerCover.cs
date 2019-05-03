using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCover : MonoBehaviour {

    private bool canTakeCover;
    private bool isInCover;
    private RaycastHit closestHit;

    [SerializeField] int numberOfRays;
    [SerializeField] LayerMask coverMask;

    bool isAiming
    {
        get
        {
            return GameManager.Instance.LocalPlayer.PlayerState.WeaponState == PlayerState.EWeaponState.AIMING ||
                GameManager.Instance.LocalPlayer.PlayerState.WeaponState == PlayerState.EWeaponState.AIMEDFIRING;

        }
    }


    internal void SetPlayerCoverAllowed(bool value)
    {
        canTakeCover = value;

        if (!canTakeCover && isInCover)
        {
            ExecuteCoverToggle();
        }
    }

    void Update()
    {
        if(isAiming && isInCover)
        {
            ExecuteCoverToggle();
            return;
        }

        if (!canTakeCover)
        {
            return;
        }

        if (GameManager.Instance.InputController.CoverToggle)
        {
            TakeCover();
        }
    }

    void TakeCover()
    {
        FindCoverAroundPlayer();

        if (closestHit.distance == 0)
        {
            return;
        }
        ExecuteCoverToggle();
    }

    void ExecuteCoverToggle()
    {
        isInCover = !isInCover;
        GameManager.Instance.EventBus.RaiseEvent("CoverToggle");
        transform.rotation = Quaternion.LookRotation(closestHit.normal) * Quaternion.Euler(0, 180f, 0);
    }

    private void FindCoverAroundPlayer()
    {
        closestHit = new RaycastHit();
        float angleStep = 360 / numberOfRays;
        for (int i = 0; i < numberOfRays; i++)
        {
            Quaternion angle = Quaternion.AngleAxis(i * angleStep, transform.up);

            CheckClosestPoint(angle);
        }
    }

    private void CheckClosestPoint(Quaternion angle)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * .3f, angle * Vector3.forward, out hit, 5f, coverMask))
        {
            if (closestHit.distance == 0 || hit.distance < closestHit.distance)
            {
                closestHit = hit;
            }

        }
    }
}
