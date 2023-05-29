using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacker : Enemy
{
    //variable movimiento ataque
    private Vector3 m_AttackPoint = Vector3.zero;

    void FixedUpdate()
    {
        if (m_Move == true)
        {
            DetectAttackPioint();
        }
        else
        {
            Debug.Log("ataco torreta");
            Attack();
        }
    }

    //funciona
    private void DetectAttackPioint()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10f, characterData.attackPointMask);

        foreach (Collider hitCollider in hitColliders)
        {
            if(hitCollider.transform.GetComponent<Character>() != null)
            {
                Character character = hitCollider.transform.GetComponent<Character>();
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


}
