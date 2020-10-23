using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAGRO : MonoBehaviour
{
    [SerializeField]
    Transform player; //serialized field means exposing it in the Unity's Inspector window (Enemy Inspector)

    [SerializeField]
    float agroRange;

    [SerializeField]
    float moveSpeed;

    Rigidbody2D rb2d;
    Animator animE;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animE = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //distance to player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        //transform.position is a reference to enemy's position

        if (distanceToPlayer < agroRange)
        {
            ChasePlayer();
        } 
        else
        {
            StopChase();
        }
    }

    private void ChasePlayer()
    {
        animE.SetBool("CanWalk", true);
        if (transform.position.x < player.position.x) //enemy is to the left of the player, hence moving right
        {
            rb2d.velocity = new Vector2(moveSpeed, 0);
            transform.localScale = new Vector2(2, 2);
        } else
        {
            rb2d.velocity = new Vector2(-moveSpeed, 0); //enemy is to the right
            transform.localScale = new Vector2(-2, 2);// flipping an enemy
        }
    }

    private void StopChase()
    {
        animE.SetBool("CanWalk", false);
        rb2d.velocity = Vector2.zero;
    }

}
