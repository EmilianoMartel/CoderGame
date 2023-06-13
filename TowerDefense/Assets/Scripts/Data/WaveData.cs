using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Data/WaveData")]
public class WaveData : ScriptableObject
{
    [SerializeField] public Enemy enemyStrong;
    [SerializeField] public Enemy enemy;
    [SerializeField] public EnemyAttacker enemyAttacker;
    [SerializeField] public Enemy[] enemyTotalList;

    //variables para las waves
    [SerializeField] public int p_waveEnemy;
    [SerializeField] public int p_timeRest;
}
