using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
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
    public bool isLooking;
    private string enemyTag;

    // Start is called before the first frame update
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

        target = GameObject.Find("Player");
        playerLife = target.GetComponent<Life>();

        if(playerLife == null)
        {
            throw new System.Exception("The object player does not have a component Life");
        }

        playerLogic = target.GetComponent<PlayerLogic>();

        if(playerLogic == null)
        {
            throw new System.Exception("The object player does not have a component PlayerLogic");    
        }

        agent = GetComponent<NavMeshAgent>();
        life = GetComponent<Life>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();
    }

    // Update is called once per frame
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
        Vector3 targetPlayer = (GameObject.Find("Player").transform.position - transform.position).normalized;

        if(Vector3.Dot(goForward, targetPlayer) < 0.6f)
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
        if(life.value <= 0)
        {
            isLife0 = true;
            agent.isStopped = true;
            collider.enabled = false;
            animator.CrossFadeInFixedTime("isLife0", 0.1f);
            Destroy(gameObject, 3f);
        }
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

        if(targetDistance <= 3.0 && isLooking)
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
}
