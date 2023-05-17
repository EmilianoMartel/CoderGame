using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    //variables iniciales
    [SerializeField] protected Animator animator;
    [SerializeField] protected float _speedMovement = 1.0f;
    [SerializeField] private int _maxLife = 100;

    [SerializeField] protected int _currentLife;

    private void Start()
    {
        _currentLife = _maxLife;
    }

    protected virtual void Move(Vector3 direction)
    {
        transform.Translate(direction * _speedMovement * Time.deltaTime);
        animator.SetBool("Move", true);
    }

    protected void Heald(int heald)
    {
        _currentLife += heald;
    }

    public virtual void Damaged(int damage)
    {
        _currentLife -= damage;
        if (_currentLife <= 0)
        {
            Death();
        }
    }

    protected void Attack()
    {

    }

    protected void Death()
    {
        animator.SetBool("Die",true);
        Destroy(gameObject);
    }
}
