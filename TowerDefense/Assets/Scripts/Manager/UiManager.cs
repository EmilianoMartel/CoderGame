using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager INSTANCE;
    [SerializeField] public TMPro.TMP_Text gold;
    [SerializeField] public Image trainLife;
    [SerializeField] public Image waveTimer;

    //variables endgame
    [SerializeField] public TMPro.TMP_Text endGame;

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
        if (gold == null)
        {
            gold = GameObject.Find("GoldText")?.GetComponent<TMPro.TMP_Text>();
        }
        if (trainLife == null)
        {
            trainLife = GameObject.Find("Active")?.GetComponent<Image>();
        }
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

    public void ChangeTimer(float percent)
    {
        if (waveTimer != null)
        {
            if (percent >= 1)
            {
                waveTimer.enabled = false;
            }
            else
            {
                waveTimer.enabled = true;
                waveTimer.fillAmount = percent;
            }
        }
    }

    public void EndGame()
    {
        if (GameManager.INSTANCE.p_victory)
        {
            endGame.text = "You Win";
        }else
        {
            endGame.text = "You Lose";
        }
    }
}
