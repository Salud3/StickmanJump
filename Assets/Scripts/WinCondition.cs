using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    public GameObject fondoN;
    public GameObject Obstacle;
    public Dialogue dialogue;
    public int GemAtStartScene;
    public int GemsObjetive;



    private void Start()
    {
        GemAtStartScene = GameManager.Instance.Gems;

    }
    public bool canPass;
    public bool canPss;
    private void Update()
    {
        if (GameManager.Instance.BKey())
        {
            Obstacle.SetActive(false);
        }

        if (GameManager.Instance.Gems - GemAtStartScene >= GemsObjetive)
        {
            Obstacle.SetActive(false);
        }
        if (dialogue != null)
        {

        if (dialogue.completeText && canPass && !canPss)
        {
            ActionsPass();
        }

        }
    }

    public void ActionsPass()
    {
        canPss = true;
        fondoN.GetComponent<Animator>().SetTrigger("Fadein");
        Invoke("Ins", 1.5f);
        FindFirstObjectByType<PlayerMovement>().CanMove = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {

            if (dialogue != null)
            {
                canPass = true;
            }
            else
            {
                ActionsPass();

            }

        }

        /*fondoN.GetComponent<Animator>().SetTrigger("Fadein");
        Invoke("Ins", 1.5f);
        other.GetComponent<PlayerMovement>().CanMove = false;*/
    }
    private void Ins()
    {
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings)
        {
            Instantiate(GameManager.Instance.PantallaWin);

        }
        else
        {

            Instantiate(GameManager.Instance.PantallaNextLevel);
        }

    }
}
