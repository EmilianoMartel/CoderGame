using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager INSTANCE;
    [SerializeField] private TMPro.TMP_Text gold;
    [SerializeField] private Image trainLife;

    //Lo convertimos en un Singelton
    private void Awake()
    {
        if (INSTANCE != null)
        {
            Destroy(this);
        }
        else
        {
            INSTANCE = this;
            DontDestroyOnLoad(this);
        }
    }
    public void Start()
    {
        if(gold == null)
        {
            gold = GameObject.Find("GoldText")?.GetComponent<TMPro.TMP_Text>();
        }
        if (trainLife == null)
        {
            trainLife = GameObject.Find("Active")?.GetComponent <Image>();
        }
    }

    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void Close()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void ViewGold()
    {
        gold.text = GameManager.INSTANCE.gold.ToString();
    }

    public void ChangeLifeTrain(float life)
    {
        float percent = life / 100;
        trainLife.fillAmount = percent;
    }
}