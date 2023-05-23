using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tower : MonoBehaviour, IColorChangeable
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
    [SerializeField] Enemy enemy;
    [SerializeField] LayerMask enemyMask;
    [SerializeField] GameObject pointShoot;
    [SerializeField] GameObject towerPoint;
    private Vector3 _directionShoot => new Vector3(pointShoot.transform.position.x - towerPoint.transform.position.x, 0f , pointShoot.transform.position.z - towerPoint.transform.position.z);
    
    //variables para el daño
    private float actualTime=5;
    [SerializeField] float m_shootingSpeed = 5;
    [SerializeField] float m_damage = 1;

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
                    enemy = hit.transform.GetComponent<Enemy>();
                    Shoot();
                    break;
                }
                else
                {
                    animator.SetBool("Shoot", false);
                    break;
                }
            case State.Shoot:
                Shoot();
                Vector3 distance = enemy.transform.position - transform.position;
                if(distance.magnitude > range || enemy == null)
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

    private void OnEnable()
    {
        //activamos el evento OnEnemyDeath del enemigo
        Enemy.OnEnemyDeath += OnEnemyDeathHandler;
    }

    private void OnDisable()
    {
        //desactivamos el evento OnEnemyDeath del enemigo
        Enemy.OnEnemyDeath -= OnEnemyDeathHandler;
    }

    private void OnEnemyDeathHandler(Enemy enemy)
    {
        if (enemy == this.enemy)
        {
            //si el enemigo muerto es el que estamos apuntando, volvemos al estado Idle
            pointShoot.transform.position = _originTransform;
            towerPoint.transform.position = _originTowerPoint;
            pointShoot.transform.rotation = _originRotation;
            towerPoint.transform.rotation = _originTowerRotation;
            GameManager.INSTANCE.gold += 50;
            animator.SetBool("Shoot", false);
            state = State.Idle;
        }
    }

    private void CoolDown()
    {
        actualTime += Time.deltaTime;
    }

    public void Upgrade()
    {
        m_shootingSpeed -= 0.2f;
        m_damage += 2;
    }

    private void Shoot()
    {
        if(actualTime > m_shootingSpeed)
        {
            animator.SetBool("Shoot", true);
            state = State.Shoot;
            enemy.Damaged(m_damage);
            transform.LookAt(enemy.transform.position);
            actualTime = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(pointShoot.transform.position, _directionShoot);
    }

    public void ChangeColor(bool isObserved)
    {
        throw new System.NotImplementedException();
    }
}
