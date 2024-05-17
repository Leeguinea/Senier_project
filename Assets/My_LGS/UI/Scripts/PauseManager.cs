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
        Time.timeScale = 0f; // ���� �ð��� ����
        isPaused = true;
        CameraFixer.FixCamera();

        Cursor.lockState = CursorLockMode.None; // ���콺 Ŀ�� ��� ����
        Cursor.visible = true; // ���콺 Ŀ�� ǥ��

    }


    public static void ResumeGame()
    {
        Time.timeScale = 1f; // ���� �ð��� ������� ����
        isPaused = false;
        CameraFixer.UnfixCamera(); // CameraFixer Ŭ������ ���� �޼��� ȣ��

        Cursor.lockState = CursorLockMode.Locked; // ���콺 Ŀ�� ���
        Cursor.visible = false; // ���콺 Ŀ�� �����

    }


    public static bool IsPaused
    {
        get { return isPaused; }
    }
}
