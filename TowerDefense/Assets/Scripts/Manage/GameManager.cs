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
    [SerializeField] int p_lifeGame = 100;

    //variables para colocar torres
    private int gold = 0;

    //variables para las waves
    [SerializeField] int p_waveEnemy = 10;
    [SerializeField] int p_timeRest = 30;
    private float _currentTime = 0;
    private int _currentWave = 1;    
    private bool _activateWave = false;
    public int enemyKilled = 0;

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
        if (_currentTime >= p_timeRest && _activateWave == false)
        {
            StartCoroutine(Waves());
            _currentTime = 0;
        }
    }

    private IEnumerator Waves()
    {
        _activateWave= true;
        for (int i = 0; i < _currentWave+1; i++)
        {
            Instantiate(enemy, spawnPoint.transform.position, transform.rotation);
            yield return new WaitForSeconds(1f);
        }
    }

    private void ChangeWave()
    {
        _currentWave++;
        _activateWave= false;
        enemyKilled = 0;
    }

    public void FlagEnemyDestroyed()
    {
        enemyKilled++;
        if(enemyKilled == _currentWave)
        {
            ChangeWave();
        }
    }

    public void EndGame()
    {
        if (_currentWave == p_waveEnemy + 1)
        {
            Debug.Log("You win");
        }
        else if (p_lifeGame <= 0)
        {
            Debug.Log("You Louse");
        }
    }

    
}
