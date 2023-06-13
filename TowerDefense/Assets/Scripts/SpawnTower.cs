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
    [SerializeField] TowerAreaDamage areaTower;
    public bool m_active = false;
    public bool m_selected;
    private bool m_canUpgrade=true;
    private Color m_color;

    private void Start()
    {
        m_active = false;
    }

    private void Awake()
    {
        m_color = plataform.GetComponent<MeshRenderer>().material.color;        
    }

    public void SpawnShootTower()
    {
        tower = Instantiate(tower, spawnPoint.transform.position, transform.rotation);
        GameManager.INSTANCE.gold -= 100;
        m_active = true;
        m_canUpgrade = false;
    }

    public void SpawnAreaTower()
    {
        areaTower = Instantiate(areaTower, spawnPoint.transform.position, transform.rotation);
        GameManager.INSTANCE.gold -= 50;
        m_active = true;
        m_canUpgrade = false;
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

    public void UpgradeTowerShooter()
    {
        if (m_canUpgrade == false)
        {
            tower.Upgrade();
            GameManager.INSTANCE.gold -= 50;
            m_canUpgrade = true;
        }
    }

    public void UpgradeAreaShooter()
    {
        if (m_canUpgrade == false)
        {
            areaTower.Upgrade();
            GameManager.INSTANCE.gold -= 25;
            m_canUpgrade = true;
        }
    }
}