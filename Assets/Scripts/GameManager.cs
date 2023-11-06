using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum TypeOfObject{ Gem, Key, Star, Coin, Trophy, Vida }
    public static GameManager Instance { get; private set; }

    public int Vidas { get { return vidas; } }
    public int Score { get { return score; } }

    private int vidas = 3;

    private int score;

    [SerializeField] private int trophies;
    [SerializeField] private int gems;
    [SerializeField] private int Coins;
    [SerializeField] private int key;
    [SerializeField] private int Star;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("Mas de un GM en escena");
            Destroy(this);
        }
    }
    public  void SumScore(int score)
    {
        this.score += score;
    }

    public void damage()
    {
        vidas--;
    }

    public void Heal()
    {
        vidas++;
    }

    public void sumObject(TypeOfObject @object){
    
        switch (@object)
        {
            case TypeOfObject.Coin: Coins++; score += 150; break;
            case TypeOfObject.Trophy: trophies++; score += 200; break;
            case TypeOfObject.Gem: gems++; score += 250; break;
            case TypeOfObject.Key: key++; score += 300; break;
            case TypeOfObject.Star: Star++; score += 500; break;
       }

    }


}
