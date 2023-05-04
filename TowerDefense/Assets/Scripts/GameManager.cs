using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject spawnPoint;
    [SerializeField] Enemy enemy;
    [SerializeField] int waveEnemy = 10;
    [SerializeField] int timeRest = 30;
    private int gold = 0;
    private float actualTime = 0;
    private int actualWave = 1;
    private bool activateWave = false;

    void Update()
    {
        actualTime += Time.deltaTime;
        if (actualTime >= timeRest && activateWave == false)
        {
            Waves();
            actualTime = 0;
        }
    }

    private void Waves()
    {
        activateWave= true;
        for (int i = 0; i < actualWave; i++)
        {
            Instantiate(enemy, spawnPoint.transform.position, transform.rotation);
        }
    }
}
