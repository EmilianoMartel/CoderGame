using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private int _life = 100;

    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            animator.SetBool("Move", true);
            Move();
        } else
        {
            animator.SetBool("Move", false);
        }
    }

    private void Move()
    {
        float movementX = Input.GetAxis("Horizontal");
        float movementZ = Input.GetAxis("Vertical");
        transform.Translate(movementX * _speed * Time.deltaTime, 0 ,movementZ * _speed * Time.deltaTime);
    }
}
