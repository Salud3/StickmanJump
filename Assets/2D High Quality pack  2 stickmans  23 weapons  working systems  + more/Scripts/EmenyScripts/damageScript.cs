using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageScript : MonoBehaviour
{
    public float damage = 0.1f;

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<healthManager>().TakeDamage(damage);
            Debug.Log("Damage");
        }
    }
}



