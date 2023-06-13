using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//evento cambio de wave
public delegate void WaveEvent();

public class WaveManager : MonoBehaviour
{
    public static WaveManager INSTANCE;
    public WaveEvent waveEvent;

    [SerializeField] private WaveData waveData;
    [SerializeField] GameObject spawnPoint;

    public float _currentTime = 0;
    public int _currentWave = 1;
    private bool _activateWave = false;
    private List<Enemy> _enemyList = new List<Enemy>();

    //Lo convertimos en un Singelton
    private void Awake()
    {
        if (INSTANCE != null)
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
        if(_activateWave == false)
        {
            WaveTimer();
            if ((_currentTime >= waveData.p_timeRest))
            {
                StartCoroutine(Waves());
                _currentTime = 0;
            }
        }
    }

    private IEnumerator Waves()
    {
        _activateWave = true;
        for (int i = 0; i < _currentWave + 1; i++)
        {
            _enemyList.Add(Instantiate(waveData.enemyTotalList[i], spawnPoint.transform.position, transform.rotation));
            yield return new WaitForSeconds(1.5f);
        }
    }

    public void ChangeWave()
    {
        _currentWave++;
        _activateWave = false;
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
        if (_enemyList.Count == 0)
        {
            ChangeWave();
        }
    }

    public void WaveTimer()
    {
        float percent = _currentTime / waveData.p_timeRest;
        UiManager.INSTANCE.ChangeTimer(percent);
    }

    public void OnEnable()
    {
        Enemy.OnEnemyDeath += FlagEnemyDestroyed;
    }
}
