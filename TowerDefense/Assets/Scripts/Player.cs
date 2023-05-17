using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : Character
{
    [SerializeField] private float _sensitivity = 0.5f;
    [SerializeField] private float _rotationY = 0f;

    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            PlayerMove();
        } else
        {
            animator.SetBool("Move",false);
        }
        Rotation();
    }

    private void PlayerMove()
    {
        float movementX = Input.GetAxis("Horizontal");
        float movementZ = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(movementX, 0, movementZ);
        Move(direction);        
    }

    private void Rotation()
    {
        float mouseX = Input.GetAxis("Mouse X");

        _rotationY += mouseX * _sensitivity;

        transform.rotation = Quaternion.Euler(0f, _rotationY, 0f);

    }
}
