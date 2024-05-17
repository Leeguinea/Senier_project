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


    // ���� �̾��ϱ� ��ư
    public void OnContinueButtonClicked() 
    {
        Debug.Log("��ưŬ�� : ���� �̾��ϱ�");

        //pauseMenu.UnActivateMenu();
    }


    // ���� �ҷ����� ��ư
    public void OnLoadButtonClicked()  
    {
        Debug.Log("��ưŬ�� : ���� �ҷ�����");


    }


    // ���� �����ϱ� ��ư
    public void OnSaveButtonClicked() 
    {
        Debug.Log("��ưŬ�� : ���� �����ϱ�");

        
    }


    // �� ���� ���� ��ư
    public void OnNewGameClicked() 
    {
        Debug.Log("��ưŬ�� : ������ ����");

        loadScene.LoadStage1();
    }


    // ���θ޴� �̵� ��ư
    public void OnGotoMainMenuButtonClicked() 
    {
        Debug.Log("��ưŬ�� : ���θ޴��� �̵�");

        loadScene.LoadMainMenu();
    }


    // ���� ���� ��ư
    public void OnExitClicked() 
    {
        Debug.Log("��ưŬ�� : ���� ����");

        Application.Quit();
    }

}
