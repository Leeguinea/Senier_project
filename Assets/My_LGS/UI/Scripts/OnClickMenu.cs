using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClickMenu : MonoBehaviour
{
    public PauseMenu pauseMenu;
    public SaveLoadManager saveLoadManager;


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

        saveLoadManager.LoadGame();
    }


    // ���� �����ϱ� ��ư
    public void OnSaveButtonClicked() 
    {
        Debug.Log("��ưŬ�� : ���� �����ϱ�");

        saveLoadManager.SaveGame();
    }


    // �� ���� ���� ��ư
    public void OnNewGameClicked() 
    {
        Debug.Log("��ưŬ�� : ������ ����");

        
    }


    // ���θ޴� �̵� ��ư
    public void OnGotoMainMenuButtonClicked() 
    {
        Debug.Log("��ưŬ�� : ���θ޴��� �̵�");

        SceneManager.LoadScene("MainMenu");
    }


    // ���� ���� ��ư
    public void OnExitClicked() 
    {
        Debug.Log("��ưŬ�� : ���� ����");

        Application.Quit();
    }

}
