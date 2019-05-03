using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : PickupItem
{
    [SerializeField] float respawnTime;
    [SerializeField] GameObject weaponPickup;
    [SerializeField] AudioController weaponCollect;
    [SerializeField] float rotateSpeed;

    void Update()
    {
        transform.Rotate(0, rotateSpeed, 0, Space.World);
    }
    public override void OnPickup(Transform item)
    {
        var playerInventory = item.GetComponentInChildren<Container>();
        GameManager.Instance.Respawner.Despawn(weaponPickup, respawnTime);
        item.GetComponent<Player>().WeaponController.EnableWeapon(weaponPickup.tag);
        weaponCollect.Play();
    }
}