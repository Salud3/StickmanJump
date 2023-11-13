using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformInteraction : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="platform")
        {
            gameObject.transform.SetParent(other.gameObject.transform);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "platform")
        {
            gameObject.transform.SetParent(null);
        }
    }

}
