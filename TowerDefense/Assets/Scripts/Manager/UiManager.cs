using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public static UiManager INSTANCE;
    [SerializeField] private TMPro.TMP_Text gold;

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
}