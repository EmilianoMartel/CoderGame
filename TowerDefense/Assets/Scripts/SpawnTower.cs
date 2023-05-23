using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Define la interfaz que incluye el método para cambiar el color
public interface IColorChangeable
{
    //esto para funcionar en el Awake o Start debes de guardar el color original
    void ChangeColor(bool isObserved);

}
public class SpawnTower : MonoBehaviour,IColorChangeable
{

    [SerializeField] GameObject spawnPoint;
    [SerializeField] GameObject plataform;
    [SerializeField] Tower tower;
    [SerializeField] Player player;
    private bool m_active;
    private bool m_upgrade=true;
    private Color m_color;


    private void Awake()
    {
        m_color = plataform.GetComponent<MeshRenderer>().material.color;        
    }

    private void Spawn()
    {
        tower = Instantiate(tower, spawnPoint.transform.position, transform.rotation);
        GameManager.INSTANCE.gold -= 100;
        m_active = true;
        m_upgrade = false;
    }

    private void CanSpawn()
    {
        
        if (Input.GetKeyUp(KeyCode.E) && m_active == false && GameManager.INSTANCE.gold >= 100)
        {
            Spawn();
            Collider collider = GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = false;
            }
        }
    }

    public void ChangeColor(bool isObserved)
    {
        if (isObserved)
        {
            plataform.GetComponent<MeshRenderer>().material.color = Color.red;
            CanSpawn();
        }
        else
        {
            plataform.GetComponent<MeshRenderer>().material.color = m_color;
        }
    }
}