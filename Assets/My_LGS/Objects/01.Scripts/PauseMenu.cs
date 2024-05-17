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
        if (pauseMenuUI == null)
        {
            Debug.LogError("Pause Menu UI is not assigned in the inspector!");
        }
        if (playerUI == null)
        {
            Debug.LogError("Player UI is not assigned in the inspector!");
        }

        // �ʱ� UI Ȱ��ȭ ���� üũ
        if (isPaused)
        {
            ActivateMenu();
        }
        else
        {
            pauseMenuUI.SetActive(false);
            playerUI.SetActive(true);
        }
    }
    

    void Update()
    {
        // Q Ű�� ������ �Ͻ� ���� ���¸� ���
        if (Input.GetKeyDown(KeyCode.Q))
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
    }


    // �Ͻ� ���� �޴� UI ��Ȱ��ȭ �޼���
    public void UnActivateMenu()
    {
        pauseMenuUI.SetActive(false);
        playerUI.SetActive(true);
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
