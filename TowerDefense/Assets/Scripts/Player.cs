using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;


public class Player : MonoBehaviour
{
    private Camera _camera;

    [SerializeField] public float speedMovement = 1.0f;

    [SerializeField] private LayerMask m_spawnTowerMask;
    [SerializeField] private int m_rangeLayer;
    private SpawnTower spawn;

    private void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        PlayerMove();
        if(spawn != null) SpawnOrUpgradeTower();
        ClickMouseDraw();
    }

    private void PlayerMove()
    {
        float movementX = Input.GetAxis("Horizontal");
        float movementZ = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(movementX, 0, movementZ);
        transform.Translate(direction * speedMovement * Time.deltaTime); ;
    }

    private void CheckSpawnTowerCollision(Ray ray)
    {                
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, m_rangeLayer, m_spawnTowerMask))
        {
            SpawnTower hitSpawnTower = hit.transform.GetComponent<SpawnTower>();
            if (hitSpawnTower != null)
            {
                if(spawn == null)
                {
                    spawn = hitSpawnTower;
                    spawn.ChangeColor(true);
                }
                else
                {
                    spawn.ChangeColor(false);
                    spawn = hitSpawnTower;
                    spawn.ChangeColor(true);
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

    private void ClickMouseDraw()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
        if (Input.GetMouseButtonDown(0))
        {
            CheckSpawnTowerCollision(_camera.ScreenPointToRay(Input.mousePosition));    
        }
    }

    private void SpawnOrUpgradeTower()
    {
        if (Input.GetKeyDown(KeyCode.E) && spawn.m_active == false && GameManager.INSTANCE.gold >= 100)
        {
            spawn.SpawnShootTower();
        }else if (Input.GetKeyDown(KeyCode.E) && spawn.m_active == true && GameManager.INSTANCE.gold >= 50)
        {
            spawn.UpgradeTowerShooter();
        }else if (Input.GetKeyDown(KeyCode.Q) && spawn.m_active == false && GameManager.INSTANCE.gold >= 50)
        {
            spawn.SpawnAreaTower();
        }
        else if(Input.GetKeyDown(KeyCode.Q) && spawn.m_active == true && GameManager.INSTANCE.gold >= 25)
        {
            spawn.UpgradeAreaShooter();
        }
    }
}
