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
            animator.SetTrigger("Die");
            this.gameObject.GetComponent<TriggerSystem>().Dontdamage = true;
            Destroy(gameObject, 1.35f);

        }
    }
}
