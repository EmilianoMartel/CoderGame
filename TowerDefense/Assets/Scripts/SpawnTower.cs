using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Define la interfaz que incluye el método para cambiar el color
public interface IColorChangeable
{
    //esto para funcionar en el Awake o Start debes de guardar el color original
    void ChangeColor(bool action);

}
public class SpawnTower : MonoBehaviour,IColorChangeable
{

    [SerializeField] GameObject spawnPoint;
    [SerializeField] GameObject plataform;
    [SerializeField] Tower tower;
    [SerializeField] Player player;
    public bool m_active;
    private bool m_upgrade=true;
    private Color m_color;


    private void Awake()
    {
        m_color = plataform.GetComponent<MeshRenderer>().material.color;        
    }

    public void Spawn()
    {
        tower = Instantiate(tower, spawnPoint.transform.position, transform.rotation);
        GameManager.INSTANCE.gold -= 100;
        m_active = true;
        m_upgrade = false;
    }

    public void ChangeColor(bool isObserved)
    {
        if (isObserved)
        {
            plataform.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        else
        {
            plataform.GetComponent<MeshRenderer>().material.color = m_color;
        }
    }
}