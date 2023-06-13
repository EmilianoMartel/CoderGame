using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacker : Enemy
{
    //variable movimiento ataque
    private Vector3 m_AttackPoint = Vector3.zero;
    private Character character;

    void FixedUpdate()
    {
        if (m_Move == true)
        {
            DetectAttackPoint();
        }
        else
        {
            SearchAttackPoint();
        }
    }

    //funciona
    private void DetectAttackPoint()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10f, characterData.attackPointMask);

        foreach (Collider hitCollider in hitColliders)
        {
            if(hitCollider.transform.GetComponent<Character>() != null)
            {
                character = hitCollider.transform.GetComponent<Character>();
                Debug.Log(character.gameObject.layer);
                Debug.Log(characterData.attackPointMask);
                if (characterData.attackPointMask != character.gameObject.layer)
                {
                    attackCharacter = hitCollider.transform.GetComponent<Character>();
                    m_Move = false;
                    Debug.Log("Loveo");
                    break;
                }
                else
                {
                    Debug.Log("no lo veo");
                }
            }
        }
    }

    private void SearchAttackPoint()
    {
        Debug.Log(character.transform.position);
        Vector3 direction = (character.transform.position - gameObject.transform.position).normalized;
        base.Move(direction);
        animator.SetBool("Move", true);
    }
}