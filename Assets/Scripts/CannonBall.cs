using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour

{
    public Rigidbody rb;
    public float life = 5f;
    public Vector3 Direction;
    public float Velocity;
    public Animator animator;

    void Awake()
    {
        Destroy(gameObject, life);
        animator = GetComponent<Animator>();
        animator.SetBool("Roading", true);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + Direction * Time.deltaTime * Velocity);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerManager>().GettDamage();

        }
    }
}
