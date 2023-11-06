using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    public GameManager.TypeOfObject type;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.sumObject(type);
            switch (type)
            {
                case GameManager.TypeOfObject.Gem:
                    AudioManager.Instance.PlaySounds("GemKey");
                    break;
                case GameManager.TypeOfObject.Key:
                    AudioManager.Instance.PlaySounds("Button");
                    break;
                case GameManager.TypeOfObject.Star:
                    AudioManager.Instance.PlaySounds("GemKey");
                    break;
                case GameManager.TypeOfObject.Coin:
                    AudioManager.Instance.PlaySounds("Coin");
                    break;
                case GameManager.TypeOfObject.Trophy:
                    AudioManager.Instance.PlaySounds("GemKey");
                    break;
                case GameManager.TypeOfObject.Vida:
                    AudioManager.Instance.PlaySounds("Button");
                    break;
                default:
                    break;
            }
            Destroy(gameObject);
        }
    }

}
