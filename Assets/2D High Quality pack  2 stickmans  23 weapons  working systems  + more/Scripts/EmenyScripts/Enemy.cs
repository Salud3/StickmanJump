using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator Anim;

    [SerializeField]
    Transform player;

    [SerializeField]
    float Range;

    [SerializeField]
    float moveSpeed;

    Rigidbody2D rb2d;

    Vector2 baseScale;

    public int health = 100;
    //public GameObject deathEffect;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        baseScale = transform.localScale;
    }

    void Update()
    {
        //distance to player
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        //print("distToPlayer:"+ distToPlayer);

        if (distToPlayer < Range)
        {
            ChasePlayer();
        }
        else
        {
            StopChasingPlayer();
        }

    }

    void ChasePlayer()
    {
        if (transform.position.x < player.position.x)
        {
            //enemy is to the left side of the player, so move right
            rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
            transform.eulerAngles = new Vector2(transform.rotation.x, 180f);
        }
        else //if(transform.position.x > player.position.x)
        {
            //enemy is to the left side of the player, so move left
            rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);
            transform.eulerAngles = new Vector2(transform.rotation.x, 0f);
        }
        GetComponent<Animator>().Play("walk_Animation");
    }

    void StopChasingPlayer()
    {
        rb2d.velocity = new Vector2(0, 0);
        GetComponent<Animator>().Play("idle_Animation");
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}