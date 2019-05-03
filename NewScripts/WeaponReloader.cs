using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponReloader : MonoBehaviour {

    [SerializeField] int maxAmmo;
    [SerializeField] float reloadTime;
    [SerializeField] int clipSize;
    [SerializeField] Container inventory;
    [SerializeField] EWeaponType weaponType;
    [SerializeField] Image weaponImage;
    [SerializeField] Image bulletImage;

   
    public int shotsFiredInClip;
    bool isReloading;
    System.Guid containerItemId;

    public event System.Action OnAmmoChanged;

    public int RoundsRemainingInClip
    {
        get
        {
            return clipSize - shotsFiredInClip;
        }
    }
    public int RoundsRemainingInInventory
    {
        get
        {
            return inventory.GetAmountRemaining(containerItemId);
        }
    }

    public bool IsReloading
    {
        get
        {
            return isReloading;
        }
    }

    public EWeaponType WeaponType
    {
        get
        {
            return weaponType;
        }

        set
        {
            weaponType = value;
        }
    }

    public Image WeaponImage
    {
        get
        {
            return weaponImage;
        }

        set
        {
            weaponImage = value;
        }
    }

    public Image BulletImage
    {
        get
        {
            return bulletImage;
        }

        set
        {
            bulletImage = value;
        }
    }

    void Awake()
    {
        inventory.OnContainerReady += () => {
            containerItemId = inventory.Add(WeaponType.ToString(), maxAmmo);
        };
    }


    public void Reload()
    {
        isReloading = true;

        GameManager.Instance.Timer.Add(() =>
        {
            ExecuteReload(inventory.TakeFromContainer(containerItemId, clipSize - RoundsRemainingInClip));

        }, reloadTime);

    }
    private void ExecuteReload(int amount)
    {
        isReloading = false;

        shotsFiredInClip -= amount;
        HandleOnAmmoChanged();

    }

    public void TakeFromClip(int amount)
    {
        shotsFiredInClip += amount;
        HandleOnAmmoChanged();
        
    }

    public void HandleOnAmmoChanged()
    {
        if (OnAmmoChanged != null)
        {
            OnAmmoChanged();
        }
    }
}
