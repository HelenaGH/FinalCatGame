using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public bool isInRange;
    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    //bool crouch = false;

    //public Image[] hearts;

    public GameObject Heart, HeartI, HeartII;

    public float Invincible = 2; //how long is the player gonna be invincible after getting hurt, set to 2 seconds
    public int health = 3;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetKeyDown("x"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    /*public void OnCrouching(bool IsCrouching)
    {
        animator.SetBool("IsCrouching", IsCrouching);
    }
    */
    void FixedUpdate()
    {
        // character movement
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    void Hurt()
    {
        health--;
        switch (health)
        {
            case 3:
                Heart.gameObject.SetActive(true);
                HeartI.gameObject.SetActive(true);
                HeartII.gameObject.SetActive(true);
                break;

            case 2:
                Heart.gameObject.SetActive(true);
                HeartI.gameObject.SetActive(true);
                HeartII.gameObject.SetActive(false);
                break;

            case 1:
                Heart.gameObject.SetActive(true);
                HeartI.gameObject.SetActive(false);
                HeartII.gameObject.SetActive(false);
                break;

            case 0:
                Heart.gameObject.SetActive(true);
                HeartI.gameObject.SetActive(true);
                HeartII.gameObject.SetActive(false);
                SceneManager.LoadScene("GameOver");
                break;
        }

        if (health <= 0)
        {
            //Application.LoadLevel(Application.loadedLevel);
            //hearts = GetComponent<Health>().hearts;
        }
        else
            TriggerHurt(Invincible);
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        BasicEnemy enemy = collision.collider.GetComponent<BasicEnemy>();
        if (enemy != null)
        {
            foreach(ContactPoint2D point in collision.contacts)
            {
                if(point.normal.y >= 0.9f)
                {
                    enemy.Hurt();
                }
                else
                {
                    Hurt();
                }
            }
        }
    }
    
    public void TriggerHurt(float hurtTime)
    {
        StartCoroutine(hurtBlink(hurtTime));
    }

    IEnumerator hurtBlink(float hurtTime)
    {
        //we want to ignore the collision between enemies and the player
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        int playerLayer = LayerMask.NameToLayer("Player");
        Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer);//by default, the third parameter is bool true, 'ignore each other'

        //start looping hurt animation
        animator.SetLayerWeight(1, 1);

        //wait for Invincible to end
        yield return new WaitForSeconds(hurtTime);

        //reenable the collision (between the player and enemies)
        Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer, false);//false means 'don't ignore each other'
        animator.SetLayerWeight(1, 0);
    }
}