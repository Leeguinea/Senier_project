using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;  // 일시 정지 메뉴 UI. canvas가 아닌 pannel을 넣어야 함
    public GameObject playerUI;     // 플레이어 UI.

    public bool isPaused = false; // 게임의 일시정지 여부에 대한 변수

    
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
        // Tab 키를 누르면 일시 정지 상태를 토글
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


    // 일시정지 메뉴 UI활성화 메서드
    public void ActivateMenu()
    {
        pauseMenuUI.SetActive(true);
        playerUI.SetActive(false);
        Cursor.visible = true; // 마우스 커서 활성화
        Cursor.lockState = CursorLockMode.None; // 커서 비고정
    }


    // 일시 정지 메뉴 UI 비활성화 메서드
    public void UnActivateMenu()
    {
        pauseMenuUI.SetActive(false);
        playerUI.SetActive(true);
        Cursor.visible = false; // 마우스 커서 비활성화
        Cursor.lockState = CursorLockMode.Locked; // 커서 고정
    }


    // 게임을 다시 실행하는 메서드
    public void ResumeGame() 
    {
        UnActivateMenu();  // 일시 정지 메뉴 UI 비활성화
        Time.timeScale = 1f;  // 게임 시간 정상화
        isPaused = false;
        Debug.Log("Game Resumed");
    }


    // 게임을 일시 정지하는 메서드
    public void PauseGame() 
    {
        ActivateMenu(); // 일시정지 메뉴 UI활성화
        Time.timeScale = 0f;  // 게임 시간 멈춤
        isPaused = true;
        Debug.Log("Game Paused");
    }


}
