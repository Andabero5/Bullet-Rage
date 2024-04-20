using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.AI;

public class AnkleGrabberLogic : MonoBehaviour
{
    private GameObject target;
    private NavMeshAgent agent;
    private Life life;
    private Animator animator;
    private Collider collider;
    private Life playerLife;
    private PlayerLogic playerLogic;
    public bool isLife0 = false;
    public bool isAttacking = false;
    public float speed = 1.0f;
    public float angularSpeed = 120;
    public float damage = 25;
    public Transform other;
    public bool isLooking;

    // Start is called before the first frame update
    void Start()
    {
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
            isLooking = true;
        }
    }

    void checkLife()
    {
        if (isLife0) return 0;
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

        if(targetDistance <= 2.0 && isLooking)
        {
            attack();
        }
    }

    void attack()
    {
        playerLife.takeDamage(damage);
    }
}
