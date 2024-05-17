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
        if (pauseMenuUI == null)
        {
            Debug.LogError("Pause Menu UI is not assigned in the inspector!");
        }
        if (playerUI == null)
        {
            Debug.LogError("Player UI is not assigned in the inspector!");
        }

        // 초기 UI 활성화 여부 체크
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
        // Q 키를 누르면 일시 정지 상태를 토글
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


    // 일시정지 메뉴 UI활성화 메서드
    public void ActivateMenu()
    {
        pauseMenuUI.SetActive(true);
        playerUI.SetActive(false);
    }


    // 일시 정지 메뉴 UI 비활성화 메서드
    public void UnActivateMenu()
    {
        pauseMenuUI.SetActive(false);
        playerUI.SetActive(true);
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
