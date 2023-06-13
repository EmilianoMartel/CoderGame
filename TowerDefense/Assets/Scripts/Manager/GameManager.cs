using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager INSTANCE;

    [SerializeField] Enemy enemy;
    [SerializeField] Enemy enemyStrong;
    [SerializeField] int p_lifeGame = 100;

    //variables para colocar torres
    public int gold = 300;

    //variables para las waves
    [SerializeField] int p_waveEnemy = 10;    

    //Lo convertimos en un Singelton
    private void Awake()
    {
        if(INSTANCE !=null)
        {
            Destroy(this);
        }
        else
        {
            INSTANCE = this;
            DontDestroyOnLoad(this);
        }        
    }

    void Update()
    {        
        if (WaveManager.INSTANCE._currentWave == p_waveEnemy + 1)
        {
            EndGame("Win");
        }
        else if (p_lifeGame <= 0)
        {
            EndGame("Lose");
        }
        UiManager.INSTANCE.ViewGold();
        UiManager.INSTANCE.ChangeLifeTrain(p_lifeGame);
    }

    public void EndGame(string winOrLose)
    {
        if (winOrLose == "Win")
        {
            Debug.Log("You win");
        }
        else if (winOrLose == "Lose")
        {
            Debug.Log("You Louse");
        }
    }

    public void ChangeLife(int life)
    {
        p_lifeGame += life;
    }
}
