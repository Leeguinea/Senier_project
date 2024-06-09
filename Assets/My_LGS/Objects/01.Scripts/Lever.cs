using System;
using UnityEngine;

// 탈출로를 활성화를 위한 '트리거' 클래스
public class Lever : MonoBehaviour
{
    [Header("ExitLever")]
    public bool Activate = false;
    public float ActivateAngle = 60f;
    public float UnActivateAngle = -60f;
    public float smooth = 3f;

    [Header("Sound Effect")]
    public AudioSource LeverSound;  // 성공음

    [Header("Gate1")]
    public ExitDoor1 exitDoor1; // 컴포넌트 선언

    private NoticeMessage noticeMessage; // 컴포넌트 선언

    private Quaternion targetRotation;

    void Start()
    {
        noticeMessage = FindObjectOfType<NoticeMessage>(); // NoticeUI의 클래스
        exitDoor1 = FindObjectOfType<ExitDoor1>(); // ExitDoor의 클래스 

        if (exitDoor1 == null) // 참조할 수 없는 경우 오류 메시지 출력
        {
            Debug.LogError("ExitDoor를 찾을 수 없습니다!");
        }

        targetRotation = transform.localRotation;
    }

    void Update()
    {
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
    }

    // 레버의 상태를 활성화, 비활성화 시키는 함수
    public void ChangeLeverState(int LeverNum)
    {
        Activate = !Activate;

        if (exitDoor1 == null) // 참조할 수 없는 경우 오류 메시지 출력
        {
            Debug.LogError("ExitDoor를 찾을 수 없습니다!");
            return;
        }

        if (Activate)
        {
            LeverTriggerOn(LeverNum);
        }
        else
        {
            LeverTriggerOff(LeverNum);
        }
    }

    private void LeverTriggerOn(int LeverNum)
    {
        targetRotation = Quaternion.Euler(ActivateAngle, 0, 0);
        exitDoor1.LeverTrigerOn(LeverNum);
        GetComponent<BoxCollider>().enabled = false;
        LeverSound.Play();
    }

    private void LeverTriggerOff(int LeverNum)
    {
        targetRotation = Quaternion.Euler(UnActivateAngle, 0, 0);
        exitDoor1.LeverTrigerOff(LeverNum); // 탈출문을 닫는 함수 호출
        GetComponent<BoxCollider>().enabled = true;
        LeverSound.Play();
    }
}
