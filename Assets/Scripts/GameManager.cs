using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public enum TypeOfObject{ Gem, Key, Star, Coin, Trophy, Vida }

    public int Vidas { get { return vidas; } }
    public int Score { get { return score; } }
    public int Trophies { get { return trophies; } }
    public int Gems { get { return gems; } }
    public int Key { get { return key; } }
    public int Star { get { return star; } }
    public int Coins { get { return coins; } }

    private int vidas = 3;

    private int score;

    [SerializeField] private int trophies;
    [SerializeField] private int gems;
    [SerializeField] private int coins;
    [SerializeField] private int key;
    [SerializeField] private int star;

    public GameObject Pantallalose;
    public GameObject PantallaNextLevel;
    public GameObject PantallaWin;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("Mas de un GM en escena");
            Destroy(gameObject);
        }
    }
    public void Resett()
    {
        vidas = 3; trophies = 0; gems = 0; key = 0; coins =0; star = 0; score = 0;
    }
    public bool BKey()
    {
        return key > 0;
    }
    public  void SumScore(int score)
    {
        this.score += score;
    }

    public void damage()
    {
        vidas--;
    }

    public void Lose()
    {
        Debug.Log("Lose");
        Instantiate(Pantallalose, Vector3.zero, new Quaternion(0,0,0,0));
    }

    public void Heal()
    {
        vidas++;
    }

    public void sumObject(TypeOfObject @object){
    
        switch (@object)
        {
            case TypeOfObject.Coin: coins++; score += 150; break;
            case TypeOfObject.Trophy: trophies++; score += 200; break;
            case TypeOfObject.Gem: gems++; score += 250; break;
            case TypeOfObject.Key: key++; score += 300; break;
            case TypeOfObject.Star: star++; score += 500; break;
            case TypeOfObject.Vida: Heal(); break;
       }

    }

    //Aqui son cargas de scena
    public void Loadscene()
    {
        Invoke("sceneman", 1f);
    }
    private void sceneman()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(SceneLoad(nextSceneIndex));

    }
    public IEnumerator SceneLoad(int sceneIndex)
    {

        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneIndex);
        AudioManager.Instance.musicSource.Stop();
        yield return new WaitForSeconds(0.1f);
        AudioManager.Instance.ChargeMusicLevel();

    }
}
