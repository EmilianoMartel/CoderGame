using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager INSTANCE;

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
}
