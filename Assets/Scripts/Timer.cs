using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public LevelGenerator lg;
    public Text timerText;

    void Start()
    {
        StartCoroutine(loadTimer(lg.levelTime));
        Time.timeScale = 1;
    }

    IEnumerator loadTimer(float totalTime)
    {
        float countdown = totalTime;
        Debug.Log(countdown);
        while (countdown >= 0)
        {
            yield return new WaitForSeconds(1);
            timerText.text = countdown.ToString();
            countdown--;
        }
        
        //reload scene if fail, load next level if win
    }

}