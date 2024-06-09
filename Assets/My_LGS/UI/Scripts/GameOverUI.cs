using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    //필요 소스 불러오기
    [Header("GameOverUI")]
    public GameObject UIBox;
    public Text UIText1;
    public Text UIText2;


    // Start is called before the first frame update
    void Start()
    {
        UIBox.SetActive(true);
        DontDestroyOnLoad(gameObject); //씬 전환 시, 오브젝트 파괴가 되지 않도록 함.

        Cursor.visible = true; // 마우스 커서 활성화
        Cursor.lockState = CursorLockMode.None; // 커서 비고정
    }



    public void NoticeGameOver(string message1, string message2)
    {
        UIText1.text = message1;
        UIText2.text = message2;
        UIBox.SetActive(true);

        Time.timeScale = 0f; //게임 일시 정지
    }
}
