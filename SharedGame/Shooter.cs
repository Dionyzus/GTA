using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Shooter : MonoBehaviour
{

    [SerializeField] float rateOfFire;
    [SerializeField] Projectile projectile;
    [SerializeField] Transform hand;
    [SerializeField] AudioController audioReload;
    [SerializeField] AudioController audioFire;
    [SerializeField] AudioController emptyClip;

    public Transform AimTarget;
    public Vector3 AimTargetOffset;

    [HideInInspector]
    Transform muzzle;

    public new GameObject light;

    public WeaponReloader reloader;
    private ParticleSystem muzzleFireParticleSystem;

    private WeaponRecoil m_WeaponRecoil;
    private WeaponRecoil WeaponRecoil
    {
        get
        {
            if(m_WeaponRecoil==null)
            {
                m_WeaponRecoil = GetComponent<WeaponRecoil>();
            }
            return m_WeaponRecoil;
        }
    }

    float nextFireAllowed;
    public bool canFire;

    public void Equip()
    {
        transform.SetParent(hand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    void Awake()
    {
        muzzle = transform.Find("Model/Muzzle");
        reloader = GetComponent<WeaponReloader>();
        muzzleFireParticleSystem = muzzle.GetComponent<ParticleSystem>();
        light.SetActive(false);
    }

    public void Reload()
    {
        if (reloader == null)
        {
            return;
        }
        reloader.Reload();
        audioReload.Play();
    }

    void FireEffect()
    {
        if (muzzleFireParticleSystem == null)
        {
            return;
        }
        muzzleFireParticleSystem.Play();
        light.SetActive(true);
        CameraShaker.Instance.ShakeOnce(0.25f, 0.25f, 0.1f, 0.5f);
    }

    public virtual void Fire()
    {
        light.SetActive(false);
        canFire = false;
        if (Time.time < nextFireAllowed || ShooterBlock.blockShooter == true)
            return;
        if (reloader != null)
        {
            if (reloader.IsReloading)
            {
                return;
            }

            if (reloader.RoundsRemainingInClip == 0)
            {
                emptyClip.Play();
                return;
            }
            reloader.TakeFromClip(1);
        }
        nextFireAllowed = Time.time + rateOfFire;
        bool isLocalPlayerControlled = AimTarget == null;
        if (!isLocalPlayerControlled)
        {
            muzzle.LookAt(AimTarget.position + AimTargetOffset);
        }

        Projectile newBullet = (Projectile)Instantiate(projectile, muzzle.position, muzzle.rotation);

        if (isLocalPlayerControlled)
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));

            RaycastHit hit;
            Vector3 targetPosition = ray.GetPoint(500);

            if (Physics.Raycast(ray, out hit))
            {
                targetPosition = hit.point;

            }
            newBullet.transform.LookAt(targetPosition + AimTargetOffset);
        }
        if(this.WeaponRecoil)
        {
            this.WeaponRecoil.Activate();
        }
        FireEffect();
        audioFire.Play();
        canFire = true;
    }
}
