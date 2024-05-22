using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
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
    public Transform shootPoint;

    [Header("Referencia de Sonidos")]
    public AudioClip isAShoot;
    public AudioClip isWithoutBullets;
    public AudioClip isTakingInMag;
    public AudioClip isTakingOutMag;
    public AudioClip noAmmunition;
    public AudioClip isDrawingGun;

    [Header("Atributos de Arma")]
    public FireMode fireMode = FireMode.FullAuto;
    public float damage;
    public float fireRate = 0.3f;
    public int remainingBullets;
    public int bulletsInMag;
    public int magSize;
    public int maxBullets;
    private SoundManager soundManager;

    void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        if (soundManager == null)
        {
            throw new System.Exception("SoundManager not found in the scene.");
        }

        animator = GetComponent<Animator>();

        // Verificar y reutilizar el AudioSource existente
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        bulletsInMag = magSize;
        remainingBullets = maxBullets;

        Invoke("EnableGun", 0.5f);
    }

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
        soundManager.SelectAudio(audioSource, isAShoot, 0.5f);
        noShootTime = true;

        ReproduceShootingAnimation();
        bulletsInMag--;
        StartCoroutine(RestartNoShootTime());

        DirectShoot();
    }

    void DirectShoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit))
        {
            if (hit.transform.CompareTag("AnkleGrabber") || 
               hit.transform.CompareTag("TortoiseBoss") || 
               hit.transform.CompareTag("CyberMonster"))
            {
                Life life = hit.transform.GetComponent<Life>();
                if (life == null)
                {
                    throw new System.Exception("The component Life of the Enemy has not been found");
                }
                else
                {
                    life.takeDamage(damage);
                    UnityEngine.Debug.Log("El enemigo ha recibido daño");
                }
            }
        }
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
        soundManager.SelectAudio(audioSource, isWithoutBullets, 0.5f);
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
        soundManager.SelectAudio(audioSource, isDrawingGun, 0.5f);
    }

    public void TakingInMag()
    {
        soundManager.SelectAudio(audioSource, isTakingInMag, 0.5f);
    }

    public void TakingOutMag()
    {
        soundManager.SelectAudio(audioSource, isTakingOutMag, 0.5f);
    }

    public void NoAmmunition()
    {
        soundManager.SelectAudio(audioSource, noAmmunition, 0.5f);
        Invoke("RestartReload", 0.1f);
    }

    void RestartReload()
    {
        reloading = false;
    }
}
