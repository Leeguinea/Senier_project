using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    
   public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }


    public void LoadStage1()
    {
        SceneManager.LoadScene("stage1");
    }


    public void LoadStage2()
    {
        SceneManager.LoadScene("stage2");
    }


    public void LoadStage3()
    {
        SceneManager.LoadScene("stage3");
    }


    public void LoadEndingScene()
    {
        SceneManager.LoadScene("EndingScene");
    }

}
