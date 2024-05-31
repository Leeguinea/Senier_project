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


    // ���� �̾��ϱ� ��ư
    public void OnContinueButtonClicked() 
    {
        Debug.Log("��ưŬ�� : ���� �̾��ϱ�");

        pauseMenu.UnActivateMenu();
    }


    // ���� �ҷ����� ��ư
    public void OnLoadButtonClicked()  
    {
        Debug.Log("��ưŬ�� : ���� �ҷ�����");

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


    // ���� �����ϱ� ��ư
    public void OnSaveButtonClicked() 
    {
        Debug.Log("��ưŬ�� : ���� �����ϱ�");

        if (saveLoadManager != null)
        {
            saveLoadManager.SaveData();
        }
        else
        {
            Debug.LogError("SaveLoadManager is not assigned.");
        }
    }


    // �� ���� ���� ��ư
    public void OnNewGameClicked() 
    {
        Debug.Log("��ưŬ�� : ������ ����");

        if (saveLoadManager != null)
        {
            saveLoadManager.DeleteSaveFile(); // ���̺� ���� ����
        }
        else
        {
            Debug.LogError("SaveLoadManager is not assigned.");
        }

        SceneManager.LoadScene("Stage1"); // �������� 1������ �̵�
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
