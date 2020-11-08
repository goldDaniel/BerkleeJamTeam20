using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public LevelGenerator lg;
    public Text timerText;

    private float countdown;

    void Start()
    { 
        Time.timeScale = 1;
    }

    void Update()
    {
        timerText.text = lg.GetLevelTime().ToString();
    }
}