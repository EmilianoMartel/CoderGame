using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAreaDamage : MonoBehaviour
{
    //variables de daño
    [SerializeField] public float m_damagePerSecond;
    [SerializeField] public float m_actualTime;
    [SerializeField] public float m_second = 1.0f;
    [SerializeField] Enemy enemy;

    private void Update()
    {
        m_actualTime += Time.deltaTime;
    }

    public void OnTriggerEnter(Collider other)
    {
        enemy = other.GetComponent<Enemy>();
        if(enemy != null && m_actualTime >= m_second)
        {
            enemy.Damaged(m_damagePerSecond);
            m_actualTime= 0;
        }
    }

    public void Upgrade()
    {
        m_damagePerSecond += 5;
    }
}
