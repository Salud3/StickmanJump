using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TriggerSystem : MonoBehaviour
{
    public enum TypeOfObject { NONE, ENEMY, CHECKPOINT, ENEMYM }
    public TypeOfObject Type = TypeOfObject.NONE;
    public PlayerManager Player;
    private void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player = other.gameObject.GetComponent<PlayerManager>();
            Debug.Log("Colision Detect");
            switch (Type){

            case TypeOfObject.NONE:
                    Debug.Log("No Type Assigned");
                break;
            case TypeOfObject.ENEMY:
                    Debug.Log("Intentando Daño");
                Player.GettDamage();
                break;
            case TypeOfObject.ENEMYM:
                    if (!other.GetComponent<PlayerMovement>().Jumping)
                    {
                        Debug.Log("Intentando Daño");
                        Player.GettDamage();
                    }
                    break;
            case TypeOfObject.CHECKPOINT:
                Player.SetCheckPoint(this.transform);
                break;
            default:
                    Debug.Log("No Type Assigned");
                break;
        }
            
        }
    }
}
