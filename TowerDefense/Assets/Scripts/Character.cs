using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected CharacterData characterData;
    [SerializeField] protected Animator animator;
    [SerializeField] protected float m_currentLife;

    //variables para que no se mueva del piso
    [SerializeField] protected int m_groundDistance;
    [SerializeField] protected LayerMask m_groundMask;

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, m_groundDistance, m_groundMask))
        {
            transform.position = hit.point + Vector3.up * 0.1f;
        }
    }

    private void Start()
    {
        Debug.Log(characterData.maxLife);
        m_currentLife = characterData.maxLife;
    }

    protected virtual void Move(Vector3 direction)
    {
        transform.Translate(direction * characterData.speedMovement * Time.deltaTime);
        animator.SetBool("Move", true);
    }

    protected void Heald(int heald)
    {
        m_currentLife += heald;
    }

    public virtual void Damaged(float damage)
    {
        m_currentLife -= damage;
        if (m_currentLife < 0)
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
