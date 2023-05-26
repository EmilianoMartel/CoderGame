using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UIElements;


public class Player : Character
{
    [SerializeField] private float _sensitivity = 0.5f;
    [SerializeField] private float _rotationY = 0f;

    [SerializeField] private LayerMask m_spawnTowerMask;
    [SerializeField] private int m_rangeLayer;
    private SpawnTower spawn;


    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            PlayerMove();
        }
        else
        {
            animator.SetBool("Move", false);
        }
        Rotation();
    }

    private void FixedUpdate()
    {
        CheckSpawnTowerCollision();
        if (Input.GetMouseButtonUp(0))
        {
            Attack();            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.TransformDirection(Vector3.forward));
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
    private void CheckSpawnTowerCollision()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, m_rangeLayer, m_spawnTowerMask))
        {
            var hitSpawnTower = hit.transform.GetComponent<SpawnTower>();
            if (hitSpawnTower != null)
            {
                spawn = hitSpawnTower;
                spawn.ChangeColor(true);
            }
            if (Input.GetKey(KeyCode.E) && spawn.m_active == false && GameManager.INSTANCE.gold >= 100)
            {
                spawn.Spawn();
                var collider = GetComponent<Collider>();
                if (collider != null)
                {
                    collider.enabled = false;
                }
            }
        }
        else
        {
            if (spawn != null)
            {
                spawn.ChangeColor(false);
            }
        }
    }
}
