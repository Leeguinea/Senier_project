using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private static bool isPaused = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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

    public static void PauseGame()
    {
        Time.timeScale = 0f; // 게임 시간을 정지
        isPaused = true;
        CameraFixer.FixCamera();

        Cursor.lockState = CursorLockMode.None; // 마우스 커서 잠금 해제
        Cursor.visible = true; // 마우스 커서 표시

    }


    public static void ResumeGame()
    {
        Time.timeScale = 1f; // 게임 시간을 원래대로 돌림
        isPaused = false;
        CameraFixer.UnfixCamera(); // CameraFixer 클래스의 정적 메서드 호출

        Cursor.lockState = CursorLockMode.Locked; // 마우스 커서 잠금
        Cursor.visible = false; // 마우스 커서 숨기기

    }


    public static bool IsPaused
    {
        get { return isPaused; }
    }
}
