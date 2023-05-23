using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Data/WaveData")]
public class WaveData : ScriptableObject
{
    [SerializeField] Enemy enemy;
    [SerializeField] Enemy enemyStrong;
    [SerializeField] EnemyAttacker enemyAttacker;
    [SerializeField] Enemy[] enemyTotalList;

    //variables para las waves
    [SerializeField] int p_waveEnemy;
    [SerializeField] int p_timeRest;
}
