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
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private int life = 100;
    [SerializeField] private GameObject _pointDirection;
    [SerializeField] private float _rangeTrain = 1f;
    [SerializeField] private LayerMask  _layerMask;

    //variables cuando muere
    public static event EnemyEvent OnEnemyDeath;

    //variables para la direccion
    private Vector3 _direction = new Vector3();

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
    }

    private void Update()
    {
        transform.LookAt(train.transform.position);
        Move();
    }

    private void Move()
    {
        _direction = transform.position - train.transform.position;

        //logic animator move
        if (_direction.magnitude > 0) animator.SetBool("Move", true);
        else animator.SetBool("Move", false);

        Vector3 directionNormalized = _direction.normalized;
        this.transform.Translate(directionNormalized * speed * Time.deltaTime);
    }

    public void ArrivedAdTrain()
    {
        Debug.Log("entraste");
        Destroy(gameObject);
    }

    public void Damaged(int damage)
    {
        life -= damage;
        if(life <= 0)
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
        Destroy(gameObject);
    }

    public void OnTrain()
    {
        GameManager.INSTANCE.ChangeLife(-10);
        EnemyKilled(this);
    }
}