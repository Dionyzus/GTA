using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : PickupItem {

    [SerializeField] EWeaponType weaponType;
    [SerializeField] float respawnTime;
    [SerializeField] int amount;
    [SerializeField] AudioController ammoCollect;
    //Way to trigger event via EventBus, just test example.
    /*public void Start()
    {
        GameManager.instance.EventBus.AddListener("EnemyDeath", new EventBus.EventListener()
        {
            Method = () =>
              {
                  print("Enemy Death Listener");
              }
        });
    */
    public override void OnPickup(Transform item)
    {
        var playerInventory = item.GetComponentInChildren<Container>();
        GameManager.Instance.Respawner.Despawn(gameObject, respawnTime);
        playerInventory.Put(weaponType.ToString(), amount);
        ammoCollect.Play();
        item.GetComponent<Player>().PlayerShoot.ActiveWeapon.reloader.HandleOnAmmoChanged();
    }
}
