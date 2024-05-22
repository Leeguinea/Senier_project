using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    //�ʿ� �ҽ� �ҷ�����
    [Header("GameOverUI")]
    public GameObject UIBox;
    public Text UIText1;
    public Text UIText2;


    // Start is called before the first frame update
    void Start()
    {
        UIBox.SetActive(true);
        DontDestroyOnLoad(gameObject); //�� ��ȯ ��, ������Ʈ �ı��� ���� �ʵ��� ��.

        Cursor.visible = true; // ���콺 Ŀ�� Ȱ��ȭ
        Cursor.lockState = CursorLockMode.None; // Ŀ�� �����
    }



    public void NoticeGameOver(string message1, string message2)
    {
        UIText1.text = message1;
        UIText2.text = message2;
        UIBox.SetActive(true);

        Time.timeScale = 0f; //���� �Ͻ� ����
    }
}
