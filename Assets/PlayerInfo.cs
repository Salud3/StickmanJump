using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    public int score;
    public int level;
    public int vidas;

    public PlayerInfo(int score, int level, int vidas)
    {
        this.vidas = vidas;
        this.level = level;
        this.score = score;
    }
}
