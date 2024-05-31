using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClickMenu : MonoBehaviour
{
    public SaveLoadManager saveLoadManager;
    PauseMenu pauseMenu;

    private void Start()
    {
        if (saveLoadManager == null)
        {
            saveLoadManager = FindObjectOfType<SaveLoadManager>();
            if (saveLoadManager == null)
            {
                Debug.LogError("SaveLoadManager is not found in the scene.");
            }
        }

        pauseMenu = FindObjectOfType<PauseMenu>();
    }


    // 게임 이어하기 버튼
    public void OnContinueButtonClicked() 
    {
        Debug.Log("버튼클릭 : 게임 이어하기");

        pauseMenu.UnActivateMenu();
    }


    // 게임 불러오기 버튼
    public void OnLoadButtonClicked()  
    {
        Debug.Log("버튼클릭 : 게임 불러오기");

        if (saveLoadManager != null)
        {
            saveLoadManager.LoadLastSavedScene();
            saveLoadManager.LoadPlayerData();
        }
        else
        {
            Debug.LogError("SaveLoadManager is not assigned.");
        }
    }


    // 게임 저장하기 버튼
    public void OnSaveButtonClicked() 
    {
        Debug.Log("버튼클릭 : 게임 저장하기");

        if (saveLoadManager != null)
        {
            saveLoadManager.SaveData();
        }
        else
        {
            Debug.LogError("SaveLoadManager is not assigned.");
        }
    }


    // 새 게임 시작 버튼
    public void OnNewGameClicked() 
    {
        Debug.Log("버튼클릭 : 새게임 시작");

        if (saveLoadManager != null)
        {
            saveLoadManager.DeleteSaveFile(); // 세이브 파일 삭제
        }
        else
        {
            Debug.LogError("SaveLoadManager is not assigned.");
        }

        SceneManager.LoadScene("Stage1"); // 스테이지 1씬으로 이동
    }


    // 메인메뉴 이동 버튼
    public void OnGotoMainMenuButtonClicked() 
    {
        Debug.Log("버튼클릭 : 메인메뉴로 이동");

        SceneManager.LoadScene("MainMenu");
    }


    // 게임 종료 버튼
    public void OnExitClicked() 
    {
        Debug.Log("버튼클릭 : 게임 종료");

        Application.Quit();
    }

}
