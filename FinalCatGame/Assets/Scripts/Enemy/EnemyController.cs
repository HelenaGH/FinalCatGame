using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms;

public class EnemyController : MonoBehaviour
{

    #region Public Variables
    public float attackDistance; //minimum distance required for an attack
    public float moveSpeed; //movement speed of the enemy
    public float timer; // timer for cooldown between the attacks
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange;
    public GameObject hotZone;
    public GameObject triggerArea;
    #endregion

    #region Private Variables
    private Animator anim;
    private float distance; //storing the distance between an enemy and the player
    private bool attackMode;
    private bool cooling;
    private float intTimer;
    #endregion

    bool die = false;
    Rigidbody2D rb;

    private void Start()
    {

    }

    private void Awake()
    {
        intTimer = timer; //store the initial value of the timer
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            triggerArea.SetActive(false);
            target = collider.transform;
            inRange = true;
            hotZone.SetActive(true);
        }
    }

    private void Update()
    {
      
        //what happens when The Player is detected:
        if(inRange)
        {
            anim.SetBool("CanWalk", false);
            EnemyLogic();
            //it's stopping enemies walking and attacking animations
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            inRange = false;
            hotZone.SetActive(false);
            triggerArea.SetActive(true);
        }
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);// calculating the distance between an enemy and the player

        if(distance > attackDistance)//the enemy will start moving toward the player
        {
            Move();
            StopAttack();
        }
        else if(attackDistance >= distance && cooling == false)
        {
            Attack();
        }

        if (cooling)
        {
            Cooldown();
            anim.SetBool("attack", false);
        }
    }

    void Move()
    {
        anim.SetBool("CanWalk", true);
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        timer = intTimer; //reseting the timer
        attackMode = true;
        anim.SetBool("CanWalk", false);
        anim.SetBool("attack", true);
    }

    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("attack", false);

    }

    public void TriggerCooling()
    {
        cooling = true;
    }

    void Cooldown()
    {
        timer -= Time.deltaTime;
        if(timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }

    //public UnityEvent Die; 

    public void Hurt()
    {
        die = true;
        anim.SetBool("IsDead", true);
    }

}
