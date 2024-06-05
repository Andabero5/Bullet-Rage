using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLogic : MonoBehaviour
{
    public GameObject enemy;
    private GameObject target;
    private NavMeshAgent agent;
    private Life life;
    private Animator animator;
    private Collider collider;
    private Life playerLife;
    private PlayerLogic playerLogic;
    public bool isLife0 = false;
    public bool isAttacking = false;
    public float speed;
    public float angularSpeed;
    public float damage;
    public Transform other;
    public bool isLooking;
    private string enemyTag;
    private AudioSource audioSource;
    private Coroutine audioCoroutine;

    // Configuración audios
    private SoundManager soundManager;

    // Índices de los clips de audio para cada tipo de enemigo
    private int ankleGrabberAudioIndex = 0;
    private int tortoiseBossAudioIndex = 1;
    private int cyberMonsterAudioIndex = 2;

    // Valores de Spatial Blend y Spread para cada enemigo
    private float ankleGrabberSpatialBlend = 1.0f;
    private float tortoiseBossSpatialBlend = 1.0f;
    private float cyberMonsterSpatialBlend = 1.0f;

    private float ankleGrabberSpread = 180f;
    private float tortoiseBossSpread = 180f;
    private float cyberMonsterSpread = 180f;

    // Valores de minDistance y maxDistance para cada enemigo
    private float ankleGrabberMinDistance = 1f;
    private float tortoiseBossMinDistance = 1f;
    private float cyberMonsterMinDistance = 1f;

    private float ankleGrabberMaxDistance = 1f;
    private float tortoiseBossMaxDistance = 10f;
    private float cyberMonsterMaxDistance = 5f;

    void Start()
    {

        if (enemy)
        {
            enemyTag = enemy.tag;
        }
        else
        {
            throw new System.Exception("There is no tag assigned to the enemy object");
        }

        target = GameObject.FindWithTag("Player");
        playerLife = target.GetComponent<Life>();

        if (playerLife == null)
        {
            throw new System.Exception("The object player does not have a component Life");
        }

        playerLogic = target.GetComponent<PlayerLogic>();

        if (playerLogic == null)
        {
            throw new System.Exception("The object player does not have a component PlayerLogic");    
        }

        agent = GetComponent<NavMeshAgent>();
        life = GetComponent<Life>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        // Inicializa el SoundManager
        soundManager = FindObjectOfType<SoundManager>();
        if (soundManager == null)
        {
            throw new Exception("SoundManager not found in the scene.");
        }

        PlayEnemyAudio();
    }

    void Update()
    {
        checkLife();
        chase();
        checkAttack();
        isInFrontOfThePlayer();
    }

    void isInFrontOfThePlayer()
    {
        Vector3 goForward = transform.forward;
        Vector3 targetPlayer = (GameObject.FindWithTag("Player").transform.position - transform.position).normalized;

        if (Vector3.Dot(goForward, targetPlayer) < 0.6f)
        {
            isLooking = false;
        }
        else
        {
            if (enemyTag == "AnkleGrabber")
            {
                animator.SetBool("Run", true);
            }
            if (enemyTag == "TortoiseBoss")
            {
                animator.SetBool("Walk", true);
            }
            if (enemyTag == "CyberMonster")
            {
                animator.SetBool("Run", true);
            }

            isLooking = true;
        }
    }

    void checkLife()
    {
        if (isLife0) return;
        if (life.value <= 0)
        {
            isLife0 = true;
            agent.isStopped = true;
            collider.enabled = false;

            if (enemyTag == "AnkleGrabber")
            {
                animator.SetBool("Dies", true);
                Score(50);
            }
            if (enemyTag == "TortoiseBoss")
            {
                animator.SetBool("Death", true);
                Score(100);
            }
            if (enemyTag == "CyberMonster")
            {
                animator.SetBool("Death", true);
                Score(200);
            }
            if (audioCoroutine != null)
            {
                StopCoroutine(audioCoroutine);
            }

            animator.CrossFadeInFixedTime("isLife0", 0.1f);
            Destroy(gameObject, 3f);
        }
    }

    private static void Score(int scoreSum)
    {
        var newScore = PlayerPrefs.GetInt("score", 0) + scoreSum;
        PlayerPrefs.SetInt("score", newScore);

        Debug.Log("estoy guardando en preferencias");
    }

    void chase()
    {
        if (isLife0) return;
        if (playerLogic.isLife0) return;

        agent.destination = target.transform.position;
    }

    void checkAttack()
    {
        if (isLife0) return;
        if (isAttacking) return;
        if (playerLogic.isLife0) return;
        float targetDistance = Vector3.Distance(target.transform.position, transform.position);

        if (targetDistance <= 3.0f && isLooking)
        {
            attack();
        }
    }

    void attack()
    {
        playerLife.takeDamage(damage);
        agent.speed = 0;
        agent.angularSpeed = 0;
        isAttacking = true;

        if (enemyTag == "AnkleGrabber")
        {
            animator.SetBool("CrochBite", true);
        }
        if (enemyTag == "TortoiseBoss")
        {
            animator.SetBool("Attack1", true);
        }
        if (enemyTag == "CyberMonster")
        {
            animator.SetBool("SwordAttack", true);
        }

        Invoke("restartAttack", 1.5f);
    }

    void restartAttack()
    {
        isAttacking = false;
        agent.speed = speed;
        agent.angularSpeed = angularSpeed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isLooking = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isLooking = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isLooking = false;
        }
    }

    void PlayEnemyAudio()
    {
        switch (enemyTag)
        {
            case "AnkleGrabber":
                audioCoroutine = StartCoroutine(soundManager.SelectAudioWithDelay(audioSource, ankleGrabberAudioIndex, 0.5f, ankleGrabberSpatialBlend, ankleGrabberSpread, ankleGrabberMinDistance, ankleGrabberMaxDistance, 10f));
                break;
            case "TortoiseBoss":
                audioCoroutine = StartCoroutine(soundManager.SelectAudioWithDelay(audioSource, tortoiseBossAudioIndex, 1.0f, tortoiseBossSpatialBlend, tortoiseBossSpread, tortoiseBossMinDistance, tortoiseBossMaxDistance, 5f));
                break;
            case "CyberMonster":
                audioCoroutine = StartCoroutine(soundManager.SelectAudioWithDelay(audioSource, cyberMonsterAudioIndex, 1.0f, cyberMonsterSpatialBlend, cyberMonsterSpread, cyberMonsterMinDistance, cyberMonsterMaxDistance, 5f));
                break;
            default:
                Debug.LogWarning("Unknown enemy tag: " + enemyTag);
                break;
        }
    }
}
