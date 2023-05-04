using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] float range = 10f;
    [SerializeField] int shootingSpeed = 5;
    [SerializeField] Vector3 enemyDistance = new Vector3();
    [SerializeField] LayerMask enemyMask;
    private float actualTime=0;

    void Update()
    {
        CoolDown();
    }

    private void FixedUpdate()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward), out hit, range, enemyMask))
        {
            hit.transform.GetComponent<Enemy>().Damaged(10);
            actualTime = 0;
        }
    }

    private void CoolDown()
    {
        actualTime += Time.deltaTime;
    }

    private void Upgrade()
    {

    }
    

}
