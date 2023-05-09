using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject pointView;
    [SerializeField] private Player player;
    [SerializeField] private Train train;
    [SerializeField] private Animator animator;
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private int life = 100;
    [SerializeField] private GameObject _pointDirection;
    [SerializeField] private float _rangeTrain = 100f;
    [SerializeField] private LayerMask  _layerMask;

    //variables para la direccion
    private Vector3 _direction = new Vector3();

    void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(_pointDirection.transform.position, transform.TransformDirection(Vector3.forward), out hit, _rangeTrain, _layerMask))
        {
            transform.LookAt(hit.transform.position);
        }
    }

    private void Move()
    {
        _direction = GameObject.FindGameObjectWithTag("Train").transform.position - this.transform.position;

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
        if(life <= 0)
        {            
            GameManager.INSTANCE.FlagEnemyDestroyed();
            Destroy(gameObject);
        }
    }
}