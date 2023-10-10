using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthScript : MonoBehaviour
{
    public float heal = 1f;
    /*
       
        void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<healthManager>().GetHealPoints(heal);
                 Debug.Log("Heal"); 
            }
        }  
    */
    //This is if you want to make the collider to be on trigger

    void OnTriggerStay2D(Collider2D trigger)
    {      
                if (trigger.gameObject.CompareTag("Player"))
                {
                    trigger.gameObject.GetComponent<healthManager>().GetHealPoints(heal);
                    Debug.Log("Heal");
                }
    }
}


