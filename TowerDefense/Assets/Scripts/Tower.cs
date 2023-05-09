using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tower : MonoBehaviour
{
    //variables maquina de estado
    public enum State
    {
        Idle,
        Shoot
    }
    [SerializeField] private State state;
        
    [SerializeField] Animator animator;

    //variables para el raycast
    [SerializeField] float range = 10f;
    [SerializeField] LayerMask enemyMask;
    [SerializeField] GameObject pointShoot;
    [SerializeField] GameObject towerPoint;
    private Vector3 _directionShoot => new Vector3(pointShoot.transform.position.x - towerPoint.transform.position.x, 0f , pointShoot.transform.position.z - towerPoint.transform.position.z);
    
    //variables para el shoot rate
    private float actualTime=5;
    [SerializeField] int shootingSpeed = 5;

    //variables estado original
    private Vector3 _originTransform = new Vector3();
    private Vector3 _originTowerPoint = new Vector3();
    private Quaternion _originRotation;
    private Quaternion _originTowerRotation;

    private void Awake()
    {
        _originTransform = pointShoot.transform.position;
        _originTowerPoint = towerPoint.transform.position;
        _originRotation = pointShoot.transform.rotation;
        _originTowerRotation = towerPoint.transform.rotation;
    }

    void Update()
    {
        CoolDown();
    }

    private void FixedUpdate()
    {
        RaycastHit hit;

        switch (state)
        {
            case State.Idle:
                if (Physics.Raycast(pointShoot.transform.position, _directionShoot, out hit, range, enemyMask))
                {
                    Debug.Log("lo veo");
                    Shoot(hit);
                    break;
                }
                else
                {
                    animator.SetBool("Shoot", false);
                    break;
                }
            case State.Shoot:
                if (Physics.Raycast(pointShoot.transform.position, _directionShoot, out hit, range, enemyMask))
                {
                    Shoot(hit);
                    break;
                }
                else if(Physics.Raycast(pointShoot.transform.position, _directionShoot, out hit, range+1, enemyMask))
                {
                    pointShoot.transform.position = _originTransform;
                    towerPoint.transform.position = _originTowerPoint;
                    pointShoot.transform.rotation = _originRotation;
                    towerPoint.transform.rotation = _originTowerRotation;
                    animator.SetBool("Shoot", false);
                    state = State.Idle;
                    break;
                }
                break;
        }        
    }

    private void CoolDown()
    {
        actualTime += Time.deltaTime;
    }

    private void Upgrade()
    {

    }

    private void Shoot(RaycastHit hit)
    {
        animator.SetBool("Shoot",true);
        state= State.Shoot;
        hit.transform.GetComponent<Enemy>().Damaged(1);
        transform.LookAt(hit.transform.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(pointShoot.transform.position, _directionShoot);
    }
}
