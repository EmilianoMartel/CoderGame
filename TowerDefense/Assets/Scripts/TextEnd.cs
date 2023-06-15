using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextEnd : MonoBehaviour
{
    [SerializeField] public TMPro.TMP_Text endGame;

    // Start is called before the first frame update
    void Start()
    {
        UiManager.INSTANCE.endGame = endGame;
    }

    // Update is called once per frame
    void Update()
    {
        UiManager.INSTANCE.EndGame();
    }
}
