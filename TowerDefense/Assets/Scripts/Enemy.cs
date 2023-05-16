using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public delegate void EnemyEvent(Enemy enemy);
public class Enemy : MonoBehaviour
{
    public GameObject pointView;
    public EnemyEvent enemyEvent;
    [SerializeField] private Player player;
    [SerializeField] private Train train;
    [SerializeField] private Animator animator;
    [SerializeField] private float speed = 1f;
    [SerializeField] private int life = 100;
    [SerializeField] private GameObject _pointDirection;
    [SerializeField] private float _rangeTrain = 1f;
    [SerializeField] private LayerMask _layerMask;

    //variables cuando muere
    public static event EnemyEvent OnEnemyDeath;

    //variables para la direccion
    private Vector3 _direction = new Vector3();
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
        Move();
    }

    private void Move()
    {
        Vector3 destination = enemyWay.GetWaipointPosition(currentIndex);
        transform.position += (destination - transform.position).normalized * speed * Time.deltaTime;
        if (Vector3.Distance(transform.position, destination) <= 1)
        {
            currentIndex++;
        }
        Vector3 foward = (new Vector3(destination.x,transform.position.y,destination.z)-transform.position).normalized;
        transform.forward = foward;
        animator.SetBool("Move", true);
    }

    public void ArrivedAdTrain()
    {
        Destroy(gameObject);
    }

    public void Damaged(int damage)
    {
        life -= damage;
        if (life <= 0)
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
        GameManager.INSTANCE.gold += 50;
        GameManager.INSTANCE.FlagEnemyDestroyed(this);
        Destroy(gameObject);
    }

    public void OnTrain()
    {
        GameManager.INSTANCE.ChangeLife(-10);
        EnemyKilled(this);
    }
}