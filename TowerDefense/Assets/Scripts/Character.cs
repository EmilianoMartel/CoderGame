using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    //variables para el ataque
    [SerializeField] protected Character attackCharacter;
    [SerializeField] protected GameObject m_pointView;

    //variables originales y del feedback
    [SerializeField] private ParticleSystem m_damageParticle;
    [SerializeField] private GameObject m_bodyDamage;
    private Color m_color;

    protected bool m_damageFeedBack = false;

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, m_groundDistance, m_groundMask))
        {
            transform.position = hit.point + Vector3.down * 0.1f;
        }
    }

    private void Start()
    {
        m_color = m_bodyDamage.GetComponent<SkinnedMeshRenderer>().material.color;
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
        if (!m_damageFeedBack) StartCoroutine(FeedBackDamage());
        m_currentLife -= damage;
        if (m_currentLife < 0)
        {
            animator.SetBool("Move", false);
            animator.SetBool("Die", true);
        }
    }

    protected void Attack()
    {
        RaycastHit hit;
        if (Physics.Raycast(m_pointView.transform.position, m_pointView.transform.TransformDirection(Vector3.forward), out hit, characterData.distanceAttack, characterData.attackPointMask))
        {
            if (hit.transform.GetComponent<Character>())
            {
                attackCharacter = hit.transform.GetComponent<Character>();
                attackCharacter.Damaged(characterData.damage);
                animator.SetBool("AttackBool", true);
            }
        }
    }

    private void AttackAnimation()
    {
        animator.SetBool("AttackBool", false);
    }

    protected void Death()
    {        
        Destroy(gameObject);
    }

    protected IEnumerator FeedBackDamage()
    {
        m_damageFeedBack = true;
        m_bodyDamage.GetComponent<SkinnedMeshRenderer>().material.color = Color.red;
        m_damageParticle.Play();
        yield return new WaitForSeconds(0.50f);
        m_bodyDamage.GetComponent<SkinnedMeshRenderer>().material.color = m_color;
        m_damageParticle.Stop();
        m_damageFeedBack = false;
    }
}
