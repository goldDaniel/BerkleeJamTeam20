using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   
    public void goToStart(){
        SceneManager.LoadScene("SampleScene");
    }
    /*
    public void quitGame(){
        Application.Quit();
    }
    
    public void goToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    */
}