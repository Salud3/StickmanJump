using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public GameObject fondoN;
    public GameObject Puerta;

    private void Start()
    {
        

    }

    private void Update()
    {
        if (GameManager.Instance.BKey())
        {
            Puerta.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        fondoN.GetComponent<Animator>().SetTrigger("Fadein");
        Instantiate(GameManager.Instance.PantallaWin);
        other.GetComponent<PlayerMovement>().CanMove = false;
        //GameManager.Instance.Loadscene();
    }
}
