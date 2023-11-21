using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    public GameObject fondoN;
    public GameObject Obstacle;
    public int GemAtStartScene;
    public int GemsObjetive;



    private void Start()
    {
        GemAtStartScene = GameManager.Instance.Gems;

    }

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
    }

    private void OnTriggerEnter(Collider other)
    {
        fondoN.GetComponent<Animator>().SetTrigger("Fadein");
        Invoke("Ins", 1.5f);
        other.GetComponent<PlayerMovement>().CanMove = false;
        //GameManager.Instance.Loadscene();
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
