using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public delegate void EnemyEvent(Enemy enemy);
public class Enemy : Character
{
    public GameObject pointView;
    public EnemyEvent enemyEvent;
    [SerializeField] private Player player;
    [SerializeField] private Train train;
    [SerializeField] private GameObject _pointDirection;
    [SerializeField] private float _rangeTrain = 1f;
    [SerializeField] private LayerMask _layerMask;

    //variables cuando muere
    public static event EnemyEvent OnEnemyDeath;

    //variables para la direccion
    [SerializeField] private EnemyWay enemyWay;
    private int currentIndex;

    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(_pointDirection.transform.position, transform.TransformDirection(Vector3.forward), out hit, _rangeTrain, _layerMask))
        {
            OnTrain();
        }
    }

    private void Start()
    {
        train = GameObject.FindObjectOfType<Train>();
        enemyWay = GameObject.FindObjectOfType<EnemyWay>();
        currentIndex = 0;
    }

    private void Update()
    {
        Vector3 destination = enemyWay.GetWaipointPosition(currentIndex);
        Move(destination);
    }

    protected override void Move(Vector3 destination)
    {        
        transform.position += (destination - transform.position).normalized * _speedMovement * Time.deltaTime;
        if (Vector3.Distance(transform.position, destination) <= 1)
        {
            currentIndex++;
        }
        Vector3 foward = (new Vector3(destination.x,transform.position.y,destination.z)-transform.position).normalized;
        transform.forward = foward;
        animator.SetBool("Move", true);
    }

    public override void Damaged(int damage)
    {
        _currentLife -= damage;
        if (_currentLife <= 0)
        {
            EnemyKilled(this);
        }
    }

    public void EnemyKilled(Enemy enemy)
    {
        //llamamos al evento OnEnemyDeath para notificar a la torre de que este enemigo ha muerto
        if (OnEnemyDeath != null)
        {
            OnEnemyDeath(this);
        }        
        GameManager.INSTANCE.FlagEnemyDestroyed(this);
        Death();
    }

    public void OnTrain()
    {
        GameManager.INSTANCE.ChangeLife(-10);
        EnemyKilled(this);
    }
}