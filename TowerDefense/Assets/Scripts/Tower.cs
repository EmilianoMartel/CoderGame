using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

//tower puede heredar de Character??
public class Tower : Character, IColorChangeable
{
    //variables maquina de estado
    public enum State
    {
        Idle,
        Shoot
    }

    [SerializeField] private State state;

    [SerializeField] public float life = 100f;

    //variables para el daño
    private float actualTime=5;
    [SerializeField] float m_shootingSpeed = 5;

    //variables estado original
    [SerializeField] GameObject towerPoint;
    private Vector3 _originTransform = new Vector3();
    private Vector3 _originTowerPoint = new Vector3();
    private Quaternion _originRotation;
    private Quaternion _originTowerRotation;

    private void Start()
    {
        _originTransform = m_pointView.transform.position;
        _originTowerPoint = towerPoint.transform.position;
        _originRotation = m_pointView.transform.rotation;
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
                if (Physics.Raycast(m_pointView.transform.position, m_pointView.transform.TransformDirection(Vector3.forward), out hit, characterData.distanceAttack, characterData.attackPointMask))
                {
                    attackCharacter = hit.transform.GetComponent<Enemy>();
                    Shoot();
                    break;
                }
                else
                {
                    animator.SetBool("AttackBool", false);
                    break;
                }
            case State.Shoot:
                Shoot();
                Vector3 distance = attackCharacter.transform.position - transform.position;
                if(distance.magnitude > characterData.distanceAttack || attackCharacter == null)
                {
                    m_pointView.transform.position = _originTransform;
                    towerPoint.transform.position = _originTowerPoint;
                    m_pointView.transform.rotation = _originRotation;
                    towerPoint.transform.rotation = _originTowerRotation;
                    animator.SetBool("AttackBool", false);
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
        if (enemy == this.attackCharacter)
        {
            //si el enemigo muerto es el que estamos apuntando, volvemos al estado Idle
            m_pointView.transform.position = _originTransform;
            towerPoint.transform.position = _originTowerPoint;
            m_pointView.transform.rotation = _originRotation;
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
        characterData.damage += 2;
    }

    private void Shoot()
    {
        if(actualTime > m_shootingSpeed)
        {
            Attack();
            animator.SetBool("AttackBool", true);
            state = State.Shoot;            
            transform.LookAt(attackCharacter.transform.position);
            actualTime = 0;
        }
    }

    public void ChangeColor(bool isObserved)
    {
        throw new System.NotImplementedException();
    }
}
