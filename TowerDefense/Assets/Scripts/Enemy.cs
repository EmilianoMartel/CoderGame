using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Train train;
    [SerializeField] private Animator animator;
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private int life = 100;
    private Vector3 _direction = new Vector3();

    void Update()
    {        
        Move();
    }

    private void Move()
    {
        _direction = train.transform.position - this.transform.position;

        //logic animator move
        if (_direction.magnitude > 0) animator.SetBool("Move", true);
        else animator.SetBool("Move", false);

        Vector3 directionNormalized = _direction.normalized;
        this.transform.Translate(directionNormalized * speed * Time.deltaTime);
    }

    public void ArrivedAdTrain()
    {
        Debug.Log("entraste");
        Destroy(gameObject);
    }

    public void Damaged(int damage)
    {
        life -= damage;
    }
}
