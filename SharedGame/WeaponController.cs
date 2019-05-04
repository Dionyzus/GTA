using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour {

    [SerializeField] float weaponSwitchTime;
    [HideInInspector]
    public bool CanFire;

    Shooter[] weapons;
    Shooter[] disabledWeapons;
    
    int currentWeaponIndex;

    [SerializeField] AudioController weaponSwitch;
    Transform weaponHolster;
    Transform inactiveWeapons;
 

    public event System.Action<Shooter> OnWeaponSwitch;

    Shooter m_ActiveWeapon;
    public Shooter ActiveWeapon
    {
        get
        {
            return m_ActiveWeapon;
        }
    }

    void Awake()
    {
        CanFire = true;

        weaponHolster = transform.Find("Weapons");
        inactiveWeapons = transform.Find("InactiveWeapons");

        disabledWeapons = inactiveWeapons.GetComponentsInChildren<Shooter>();
        weapons = weaponHolster.GetComponentsInChildren<Shooter>();

        if (weapons.Length > 0)
        {
            Equip(0);
        }

        DisableInactiveWeapons();
    }

    private void DisableInactiveWeapons()
    {
        for (int i = 0; i < disabledWeapons.Length; i++)
        {
       
            disabledWeapons[i].gameObject.SetActive(false);
            
            for (int j = 0; j < weapons.Length; j++)
            {
                if (disabledWeapons[i].tag == weapons[j].tag)
                {
                    weapons[j].gameObject.SetActive(false);
                }
            }
        }
    }

    public void EnableWeapon(string weaponTag)
    {

        for (int i = 0; i < disabledWeapons.Length; i++)
        {
            if (disabledWeapons[i].tag == weaponTag)
            {
                for (int j = 0; j < weapons.Length; j++)
                {
                    if (weapons[j].tag == weaponTag)
                    {
                        weapons[j].gameObject.SetActive(true);

                        Equip(j);

                        weaponSwitch.Stop();
                    }
                }
                RemoveAt(ref disabledWeapons, i);
                break;

            }
        }

    }
    public static void RemoveAt<T>(ref T[] arr, int index)
    {
        // replace the element at index with the last element
        arr[index] = arr[arr.Length - 1];
        // finally, let's decrement Array's size by one
        Array.Resize(ref arr, arr.Length - 1);
    }

    void DeactivateWeapons()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(false);
            weapons[i].GetComponent<WeaponReloader>().WeaponImage.GetComponent<Image>().enabled = false;
            weapons[i].GetComponent<WeaponReloader>().BulletImage.GetComponent<Image>().enabled = false;
            weapons[i].transform.SetParent(weaponHolster);
        }
    }

    internal void SwitchWeapon(int direction)
    {
        CanFire = false;
        currentWeaponIndex += direction;

        if (currentWeaponIndex > weapons.Length - 1)
        {
            currentWeaponIndex = 0;
        }
        if (currentWeaponIndex < 0)
        {
            currentWeaponIndex = weapons.Length - 1;
        }
        GameManager.Instance.Timer.Add(() =>
        {
            Equip(currentWeaponIndex);
        }, weaponSwitchTime);

    }
    internal void Equip(int indexOfWeapon)
    {
        DeactivateWeapons();
        CanFire = true;

        indexOfWeapon = CheckInactive(indexOfWeapon);
        
        m_ActiveWeapon = weapons[indexOfWeapon];
        m_ActiveWeapon.Equip();

        weapons[indexOfWeapon].gameObject.SetActive(true);
        weapons[indexOfWeapon].GetComponent<WeaponReloader>().WeaponImage.GetComponent<Image>().enabled = true;
        weapons[indexOfWeapon].GetComponent<WeaponReloader>().BulletImage.GetComponent<Image>().enabled = true;
        weaponSwitch.Play();
        if (OnWeaponSwitch != null)
        {
            OnWeaponSwitch(m_ActiveWeapon);
        }
    }

    private int CheckInactive(int indexOfWeapon)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            for (int j = 0; j < disabledWeapons.Length; j++)
            {
                if (weapons[indexOfWeapon].tag == disabledWeapons[j].tag)
                {
                    indexOfWeapon = 0;
                }
            }
        }

        return indexOfWeapon;
    }
}
