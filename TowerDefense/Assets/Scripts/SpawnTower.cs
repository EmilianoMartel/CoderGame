using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnTower : MonoBehaviour
{
    [SerializeField] GameObject spawnPoint;
    [SerializeField] Tower tower;
    [SerializeField] Player player;
    private bool active;

    // Update is called once per frame
    void Update()
    {
        Vector3 distance = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
        if (distance.magnitude < 2 && Input.GetKeyDown(KeyCode.E) && active == false && GameManager.INSTANCE.gold >= 100)
        {
            Spawn();
            GameManager.INSTANCE.gold -= 100;
        }
    }

    private void Spawn()
    {
        Instantiate(tower, spawnPoint.transform.position, transform.rotation);
        active= true;
    }

    private void Upgrate()
    {
        
    }
}
