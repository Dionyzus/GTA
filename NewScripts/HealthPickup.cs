using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : PickupItem
{
    [SerializeField] GameObject medkitPickup;
    [SerializeField] AudioController medkitPickupSound;
    [SerializeField] float respawnTime;

    public override void OnPickup(Transform item)
    {
        var playerHealth = item.GetComponent<PlayerHealth>();
        GameManager.Instance.Respawner.Despawn(medkitPickup, respawnTime);
        playerHealth.TakeDamage(-40);
        medkitPickupSound.Play();
    }
}
