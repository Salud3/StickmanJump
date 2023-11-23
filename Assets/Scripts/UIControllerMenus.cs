using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControllerMenus : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Resset()
    {
        GameManager.Instance.Resett();
    }
    public void LoadSceneNL()
    {
        GameManager.Instance.Loadscene();
    }
    public void SceneHome()
    {
        StartCoroutine(GameManager.Instance.SceneLoad(0));
    }
}
