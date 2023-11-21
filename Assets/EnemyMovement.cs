using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject player;
    public LayerMask playerMask;
    public float Range;
    public Animator animator;

    public bool alert;
    public bool canRecieveDamage;
    public bool canDoDamage;
    public bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        animator = transform.GetComponentInChildren<Animator>();
        canRecieveDamage = true;
        canDoDamage = true;
        canMove = true;
    }

    public float vel;
    void Update()
    {
        if (canRecieveDamage)
        {
            alert = Physics.CheckSphere(transform.position,Range,playerMask);
        }

        if (alert && canMove)
        {
            Vector3 Playerpos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            transform.LookAt(Playerpos);
            transform.position = Vector3.MoveTowards(transform.position, Playerpos, vel * Time.deltaTime);
            animator.SetBool("Walking",true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && player.GetComponent<PlayerMovement>().Jumping)
        {
            canDoDamage = false;
            canRecieveDamage = false;
            animator.SetBool("Walking", false);
            animator.SetTrigger("Die");
            AudioManager.Instance.PlaySounds("Button");
            Destroy(gameObject, 0.35f);

        }

        if(other.tag == "Limit")
        {
            canMove = false;
            StartCoroutine(sreset());
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,Range);
    }
    public Transform Respawn;

    IEnumerator sreset()
    {
        yield return new WaitForSeconds (5f);
        transform.position = Respawn.position;
    }
}
