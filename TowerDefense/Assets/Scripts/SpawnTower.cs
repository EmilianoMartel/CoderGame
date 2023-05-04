using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnTower : MonoBehaviour
{
    [SerializeField] GameObject spawnPoint;
    [SerializeField] Tower tower;
    [SerializeField] Player player;

    // Update is called once per frame
    void Update()
    {
        Vector3 distance = player.transform.position - transform.position;
        if (distance.magnitude < 2 && Input.GetKeyDown(KeyCode.E)) Spawn();
    }

    private void Spawn()
    {
        Instantiate(tower, spawnPoint.transform.position, transform.rotation);
    }
}
