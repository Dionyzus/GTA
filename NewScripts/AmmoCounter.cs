using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoCounter : MonoBehaviour {

    [SerializeField] TextMeshProUGUI text;

    PlayerShoot playerShoot;
    WeaponReloader weapReloader;

    void Awake () {
        GameManager.Instance.OnLocalPlayerJoined += HandleOnLocalPlayerJoined; ;
	}

    void HandleOnLocalPlayerJoined(Player player)
    {
      
        playerShoot = player.PlayerShoot;
        playerShoot.OnWeaponSwitch += HandleOnWeaponSwitch;
  
    }
    
    void HandleOnWeaponSwitch(Shooter activeWeapon)
    {
        weapReloader = activeWeapon.reloader;
        weapReloader.OnAmmoChanged += HandleOnAmmoChanged;
        HandleOnAmmoChanged();
    }

    void HandleOnAmmoChanged()
    {
        
        int amountInInventory = weapReloader.RoundsRemainingInInventory;
        int amountInClip = weapReloader.RoundsRemainingInClip;
        text.text = string.Format("{0}/{1}", amountInClip, amountInInventory);
      
    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
