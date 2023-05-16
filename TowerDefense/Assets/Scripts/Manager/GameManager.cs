using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager INSTANCE;

    [SerializeField] GameObject spawnPoint;
    [SerializeField] Enemy enemy;
    [SerializeField] Enemy enemyStrong;
    [SerializeField] int p_lifeGame = 100;

    //variables para colocar torres
    public int gold = 300;

    //variables para las waves
    [SerializeField] int p_waveEnemy = 10;
    [SerializeField] int p_timeRest = 30;
    private float _currentTime = 0;
    private int _currentWave = 1;    
    private bool _activateWave = false;
    private List<Enemy> _enemyList = new List<Enemy>();

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
        _currentTime += Time.deltaTime;
        if ((_currentTime >= p_timeRest) && (_activateWave == false))
        {
            StartCoroutine(Waves());
            _currentTime = 0;
        }
        if (_currentWave == p_waveEnemy + 1)
        {
            EndGame("Win");
        }
        else if (p_lifeGame <= 0)
        {
            EndGame("Lose");
        }
        UiManager.INSTANCE.ViewGold();
    }

    private IEnumerator Waves()
    {
        _activateWave= true;
        for (int i = 0; i < _currentWave+1; i++)
        {
            _enemyList.Add(Instantiate(enemy, spawnPoint.transform.position, transform.rotation));
            yield return new WaitForSeconds(1.5f);
            if(i/2 == 0)
            {
                _enemyList.Add(Instantiate(enemyStrong, spawnPoint.transform.position, transform.rotation));
                yield return new WaitForSeconds(1.5f);
            }
        }
    }

    private void ChangeWave()
    {
        _currentWave++;
        _activateWave= false;
        _currentTime = 0;
    }

    public void FlagEnemyDestroyed(Enemy enemy)
    {
        for (int i = 0; i < _enemyList.Count; i++)
        {
            if (_enemyList[i] == enemy) 
            { 
                _enemyList.RemoveAt(i);
                break; 
            }
        }        
        if(_enemyList.Count == 0)
        {
            ChangeWave();
        }
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
