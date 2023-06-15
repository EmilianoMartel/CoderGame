using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//evento cuando muere para avisar a las torretas
public delegate void EnemyEvent(Enemy enemy);

public class Enemy : Character
{
    [SerializeField] private Player player;
    [SerializeField] private Train train;
    [SerializeField] private GameObject _pointDirection;
    [SerializeField] private float _rangeTrain = 1f;
    [SerializeField] private LayerMask m_layerMask;

    //variables cuando muere
    public static event EnemyEvent OnEnemyDeath;

    //variables para la direccion
    [SerializeField] private EnemyWay enemyWay;
    private int currentIndex;

    //variables para el movimiento
    protected bool m_Move = true;

    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(_pointDirection.transform.position, transform.TransformDirection(Vector3.forward), out hit, _rangeTrain, m_layerMask))
        {
            OnTrain();
        }
    }

    private void Start()
    {
        train = GameObject.FindObjectOfType<Train>();
        enemyWay = GameObject.FindObjectOfType<EnemyWay>();
        currentIndex = 0;
        m_currentLife = characterData.maxLife;
    }

    private void Update()
    {
        if(currentIndex + 1 <= enemyWay.Points.Length && m_Move == true) 
        {
            Vector3 destination = enemyWay.GetWaipointPosition(currentIndex);
            Move(destination);
        }
    }

    protected override void Move(Vector3 destination)
    {
        transform.position += (destination - transform.position).normalized * characterData.speedMovement * Time.deltaTime;
        if (Vector3.Distance(transform.position, destination) <= 1)
        {
            currentIndex++;
        }
        Vector3 foward = (new Vector3(destination.x,transform.position.y,destination.z)-transform.position).normalized;
        transform.forward = foward;
        animator.SetBool("Move", true);
    }

    public override void Damaged(float damage)
    {
        if (!m_damageFeedBack) StartCoroutine(FeedBackDamage());
        if (m_currentLife < 0)
        {
            EnemyKilled();
        }
    }

    public void EnemyKilled()
    {
        GameManager.INSTANCE.gold += 50;
        animator.SetBool("Move", false);
        m_Move = false;
        animator.SetBool("Die", true);
        OnEnemyDeath?.Invoke(this);
    }

    public void OnTrain()
    {
        GameManager.INSTANCE.ChangeLife(-10);
        OnEnemyDeath?.Invoke(this);
        Destroy(gameObject);
    }
}