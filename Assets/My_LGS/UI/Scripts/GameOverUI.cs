using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    //필요 소스 불러오기
    [Header("GameOverUI")]
    public GameObject UIBox;
    public Text UIText1;
    public Text UIText2;


    //private bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        UIBox.SetActive(true);
        DontDestroyOnLoad(gameObject); //씬 전환 시, 오브젝트 파괴가 되지 않도록 함.
    }


    /*
    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f; //게임 멈춤
    }

    public void RestartGame()
    {
        // 게임 오버 화면을 숨기고 게임을 재시작
        isGameOver = false;
        Time.timeScale = 1f; //게임 시간을 다시 시작
    }
    */

    public void NoticeGameOver(string message1, string message2)
    {
        UIText1.text = message1;
        UIText2.text = message2;
        UIBox.SetActive(true);

        Time.timeScale = 0f; //게임 일시 정지
    }
}
