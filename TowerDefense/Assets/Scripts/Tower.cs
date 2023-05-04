using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    [SerializeField] int distance = 10;
    [SerializeField] int shootingSpeed = 5;
    [SerializeField] Vector3 enemyDistance = new Vector3();
    private float actualTime=0;


    // Update is called once per frame
    void Update()
    {
        CoolDown();
        enemyDistance = transform.position - enemy.transform.position;
        if (actualTime >= shootingSpeed)
        {
            if (enemyDistance.magnitude <= distance) Attack();
            actualTime = 0;
        }        
    }

    private void Attack()
    {
        Debug.Log("Atacando");
        enemy.Damaged(10);
    }

    private void CoolDown()
    {
        actualTime += Time.deltaTime;

    }

    private void Upgrade()
    {

    }
}
