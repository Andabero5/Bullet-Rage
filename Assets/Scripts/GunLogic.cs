using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FireMode
{
    SemiAuto,
    FullAuto
}

public class GunLogic : MonoBehaviour
{
    protected Animator animator;
    protected AudioSource audioSource;
    public bool noShootTime = false;
    public bool canShoot = false;
    public bool reloading = false;

    [Header("Referencia de Objetos")]
    public ParticleSystem fireGun;

    [Header("Referencia de Sonidos")]
    public AudioClip isAShoot;
    public AudioClip isWithoutBullets;
    public AudioClip isTakingInMag;
    public AudioClip isTakingOutMag;
    public AudioClip noAmmunition;
    public AudioClip isDrawingGun;

    [Header("Atributos de Arma")]
    public FireMode fireMode = FireMode.FullAuto;
    public float damage = 3;
    public float fireRate = 0.3f;
    public int remainingBullets;
    public int bulletsInMag;
    public int magSize = 12;
    public int maxBullets = 100;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        bulletsInMag = magSize;
        remainingBullets = maxBullets;

        Invoke("EnableGun", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (fireMode == FireMode.FullAuto && Input.GetButton("Fire1"))
        {
            CheckFire();
        }
        else if (fireMode == FireMode.SemiAuto && Input.GetButtonDown("Fire1"))
        {
            CheckFire();
        }
        if (Input.GetButtonDown("Reload"))
        {
            CheckReload();
        }
    }

    void EnableGun()
    {
        canShoot = true;
    }

    void CheckFire()
    {
        if (!canShoot) return;
        if (noShootTime) return;
        if (reloading) return;
        if (bulletsInMag > 0)
        {
            Shoot();
        }
        else
        {
            NoBullets();
        }
    }

    void Shoot()
    {
        audioSource.PlayOneShot(isAShoot);
        noShootTime = true;
        fireGun.Stop();
        fireGun.Play();

        ReproduceShootingAnimation();
        bulletsInMag--;
        StartCoroutine(RestartNoShootTime());
    }

    public virtual void ReproduceShootingAnimation()
    {
        if (gameObject.name == "Magnum")
        {
            animator.CrossFadeInFixedTime("Fire", 0.1f);
        }
    }

    void NoBullets()
    {
        audioSource.PlayOneShot(isWithoutBullets);
        noShootTime = true;
        StartCoroutine(RestartNoShootTime());
    }

    IEnumerator RestartNoShootTime()
    {
        yield return new WaitForSeconds(fireRate);
        noShootTime = false;
    }

    void CheckReload()
    {
        if (remainingBullets > 0 && bulletsInMag < magSize)
        {
            Reload();
        }
    }

    void Reload()
    {
        if (reloading) return;
        reloading = true;
        ReloadAmmunition();
        RestartReload();
        //animator.CrossFadeInFixedTime("Reload", 0.1f);
    }

    void ReloadAmmunition()
    {
        int bulletsForReloading = magSize - bulletsInMag;
        int minusBullets = (remainingBullets >= bulletsForReloading) ? bulletsForReloading : remainingBullets;

        remainingBullets -= minusBullets;
        bulletsInMag += bulletsForReloading;
    }

    public void DrawOn()
    {
        audioSource.PlayOneShot(isDrawingGun);
    }

    public void TakingInMag()
    {
        audioSource.PlayOneShot(isTakingInMag);
    }

    public void TakingOutMag()
    {
        audioSource.PlayOneShot(isTakingOutMag);
    }

    public void NoAmmunition()
    {
        audioSource.PlayOneShot(noAmmunition);
        Invoke("RestartReload", 0.1f);
    }

    void RestartReload()
    {
        reloading = false;
    }
}
