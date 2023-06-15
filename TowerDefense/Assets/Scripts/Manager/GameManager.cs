using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager INSTANCE;
    [SerializeField] public int p_lifeGame = 100;

    //variables para colocar torres
    public int gold = 300;

    //variable victoria
    [SerializeField] public bool p_victory;

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

    public void EndGame(string winOrLose)
    {
        p_lifeGame = 100;
        if (winOrLose == "Win")
        {
            p_victory = true;
        }
        else if (winOrLose == "Lose")
        {
            p_victory = false;
        }
        SceneManager.LoadScene("EndScene");
    }

    public void ChangeLife(int life)
    {
        p_lifeGame += life;
    }
}
