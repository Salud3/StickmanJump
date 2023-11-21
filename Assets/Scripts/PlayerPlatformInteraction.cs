using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformInteraction : MonoBehaviour {

    public PhysicMaterial zero;
    public PhysicMaterial normal;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            other.gameObject.transform.SetParent(gameObject.transform);
            other.GetComponent<CapsuleCollider>().material = normal;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerMovement>().onMovil = false;
            other.GetComponent<CapsuleCollider>().material = zero;
            other.gameObject.transform.SetParent(null);
        }
    }

}
