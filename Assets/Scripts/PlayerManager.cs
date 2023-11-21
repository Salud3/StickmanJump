using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int vidas;
    public bool key;
    public bool gettingDamage;
    public Transform Checkpoint;
    public GameObject[] VidaImg = new GameObject[3];
    private void Start()
    {
        vidas = GameManager.Instance.Vidas;
    }

    private void FixedUpdate()
    {
        if(vidas <= 0)
        {
            GameManager.Instance.Lose();    
        }
    }
    void Update()
    {
        switch (vidas)
        {
            case 1:
                VidaImg[0].gameObject.SetActive(true);
                VidaImg[1].gameObject.SetActive(false);
                VidaImg[2].gameObject.SetActive(false);
                break;
            case 2:
                VidaImg[0].gameObject.SetActive(false);
                VidaImg[1].gameObject.SetActive(true);
                VidaImg[2].gameObject.SetActive(true);
                break;
            case 3:
                VidaImg[0].gameObject.SetActive(true);
                VidaImg[1].gameObject.SetActive(true);
                VidaImg[2].gameObject.SetActive(true);
                break;
        }
    }

    public void SetCheckPoint(Transform checkpoint)
    {
        Checkpoint = checkpoint;
    }

    public void GettDamage()
    {
        if (gettingDamage) { 
        FindFirstObjectByType<PlayerMovement>().CanMove = false;
        gettingDamage = false;
        this.GetComponent<Animator>().SetBool("Dmg",true);
        AudioManager.Instance.PlaySounds("Die");

        StartCoroutine(GetDamage());

        }
    }
    public void Regenerate()
    {
        this.transform.position = Checkpoint.position;
        FindFirstObjectByType<PlayerMovement>().CanMove = true;

    }
    private IEnumerator GetDamage()
    {
        vidas--;
        Debug.Log("Daño recibido");
        yield return new WaitForSeconds(.60f);
        this.GetComponent<Animator>().SetBool("Dmg",false);
        Invoke("Regenerate", 0.3f);

        yield return new WaitForSeconds(2.7f);
        gettingDamage = true;

    }
}
