using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;  // �Ͻ� ���� �޴� UI. canvas�� �ƴ� pannel�� �־�� ��
    public GameObject playerUI;     // �÷��̾� UI.

    public bool isPaused = false; // ������ �Ͻ����� ���ο� ���� ����

    
    void Start()
    {
        ResumeGame();

        if (pauseMenuUI == null)
        {
            Debug.LogError("Pause Menu UI is not assigned in the inspector!");
        }
        if (playerUI == null)
        {
            Debug.LogError("Player UI is not assigned in the inspector!");
        }

    }
    

    void Update()
    {
        // Tab Ű�� ������ �Ͻ� ���� ���¸� ���
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

    }


    // �Ͻ����� �޴� UIȰ��ȭ �޼���
    public void ActivateMenu()
    {
        pauseMenuUI.SetActive(true);
        playerUI.SetActive(false);
        Cursor.visible = true; // ���콺 Ŀ�� Ȱ��ȭ
        Cursor.lockState = CursorLockMode.None; // Ŀ�� �����
    }


    // �Ͻ� ���� �޴� UI ��Ȱ��ȭ �޼���
    public void UnActivateMenu()
    {
        pauseMenuUI.SetActive(false);
        playerUI.SetActive(true);
        Cursor.visible = false; // ���콺 Ŀ�� ��Ȱ��ȭ
        Cursor.lockState = CursorLockMode.Locked; // Ŀ�� ����
    }


    // ������ �ٽ� �����ϴ� �޼���
    public void ResumeGame() 
    {
        UnActivateMenu();  // �Ͻ� ���� �޴� UI ��Ȱ��ȭ
        Time.timeScale = 1f;  // ���� �ð� ����ȭ
        isPaused = false;
        Debug.Log("Game Resumed");
    }


    // ������ �Ͻ� �����ϴ� �޼���
    public void PauseGame() 
    {
        ActivateMenu(); // �Ͻ����� �޴� UIȰ��ȭ
        Time.timeScale = 0f;  // ���� �ð� ����
        isPaused = true;
        Debug.Log("Game Paused");
    }


}
