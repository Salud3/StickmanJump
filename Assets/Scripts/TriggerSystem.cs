using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TriggerSystem : MonoBehaviour
{
    public enum TypeOfObject { NONE, ENEMY, CHECKPOINT, ENEMYM, CANNON }
    public TypeOfObject Type = TypeOfObject.NONE;
    public PlayerManager Player;

    public bool Dodamage;
    public bool Dontdamage;
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
                case TypeOfObject.CANNON:

                    if (other.GetComponent<PlayerMovement>().Jumping)
                    {
                        Debug.Log("Intentando Daño");
                        GetComponent<CannonLaunch>().Dies();
                        AudioManager.Instance.PlaySounds("Button");
                    } 

                    break;
            case TypeOfObject.ENEMY:
                    if (!Dodamage && !Dontdamage)
                    {
                        Debug.Log("Intentando Daño");
                        Player.GettDamage();
                    }else if(Dodamage && !other.GetComponent<PlayerMovement>().Dashing)
                    {
                        Debug.Log("Intentando Daño");
                        Player.GettDamage();
                    }
                    else if (Dontdamage)
                    {
                        Debug.Log("No hice daño");
                    }
                    else
                    {
                        Debug.Log("No hice daño");
                    }

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
