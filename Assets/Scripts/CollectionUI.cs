using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CollectionUI : MonoBehaviour
{
    public TextMeshProUGUI coins;
    public TextMeshProUGUI Trophies;
    public TextMeshProUGUI Gems;
    public TextMeshProUGUI Key;
    public TextMeshProUGUI Score;

    public bool isKey;

    private void Start()
    {
        
    }

    private void Update()
    {
        coins.text = ": " + GameManager.Instance.Coins.ToString();
        Trophies.text = ": " + GameManager.Instance.Trophies.ToString();
        Gems.text = ": " + GameManager.Instance.Gems.ToString();
        Score.text = "Score : " + GameManager.Instance.Score.ToString();

        if (isKey)
        {
            if (GameManager.Instance.BKey() && SceneManager.GetActiveScene().buildIndex == 1)
            {
                Key.text = "o";
                Key.color = Color.green;
            }
            else if (!GameManager.Instance.BKey() && SceneManager.GetActiveScene().buildIndex == 1)
            {
                Key.text = "-";
                Key.color = Color.red;
            }
            else
            {
                Key.text = "o";
                Key.color = Color.green;
                Key.gameObject.transform.parent.gameObject.SetActive(false);
            }
        }
        
    }

}
