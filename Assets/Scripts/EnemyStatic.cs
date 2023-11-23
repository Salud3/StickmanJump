using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatic : MonoBehaviour
{
    public Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.GetComponent<PlayerMovement>().Dashing)
        {
            this.gameObject.GetComponent<TriggerSystem>().Dontdamage = true;
            GetComponent<CapsuleCollider>().enabled = false;
            animator.SetTrigger("Die");
            Destroy(gameObject, 0.50f);

        }
    }
}
