using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClickMenu : MonoBehaviour
{
    public PauseMenu pauseMenu;
    public LoadScene loadScene;
    public GameObject LoadSceneManager;

    void Start()
    {
        loadScene = LoadSceneManager.GetComponent<LoadScene>();

        if (loadScene == null)
        {
            Debug.LogError("LoadScene component is not assigned.");
        }

    }


    // 게임 이어하기 버튼
    public void OnContinueButtonClicked() 
    {
        Debug.Log("버튼클릭 : 게임 이어하기");

        //pauseMenu.UnActivateMenu();
    }


    // 게임 불러오기 버튼
    public void OnLoadButtonClicked()  
    {
        Debug.Log("버튼클릭 : 게임 불러오기");


    }


    // 게임 저장하기 버튼
    public void OnSaveButtonClicked() 
    {
        Debug.Log("버튼클릭 : 게임 저장하기");

        
    }


    // 새 게임 시작 버튼
    public void OnNewGameClicked() 
    {
        Debug.Log("버튼클릭 : 새게임 시작");

        loadScene.LoadStage1();
    }


    // 메인메뉴 이동 버튼
    public void OnGotoMainMenuButtonClicked() 
    {
        Debug.Log("버튼클릭 : 메인메뉴로 이동");

        loadScene.LoadMainMenu();
    }


    // 게임 종료 버튼
    public void OnExitClicked() 
    {
        Debug.Log("버튼클릭 : 게임 종료");

        Application.Quit();
    }

}
