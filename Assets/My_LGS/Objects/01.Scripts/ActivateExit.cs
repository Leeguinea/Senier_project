using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 탈출로를 활성화를 위한 '트리거' 클래스
public class ActivateExit : MonoBehaviour
{
    [Header("ExitLever")]
    public bool Activate = false;
    public float ActivateAngle = 60f;
    public float UnActivateAngle = -60f;
    public float smoot = 3f;

    public ExitDoor exitDoor; // 컴포넌트 선언
    NoticeMessage noticeMessage; //컴포넌트 선언

    void Start()
    {
        noticeMessage = FindObjectOfType<NoticeMessage>(); // NoticeUI의 클래스
        exitDoor = FindObjectOfType<ExitDoor>(); // ExitDoor의 클래스 

        if (exitDoor == null) // 참조할 수 없는 경우 오류 메시지 출력
        {
            Debug.LogError("ExitDoor를 찾을 수 없습니다!");
        }

    }


    // 레버의 상태를 활성화, 비활성화 시키는 함수
    public void ChangeLeverState()
    {
        Activate = !Activate; 

        if (exitDoor == null) // 참조할 수 없는 경우 오류 메시지 출력
        {
            Debug.LogError("ExitDoor를 찾을 수 없습니다!");
            return;
        }

    }


    void Update()
    {
        // 활성화된다면 레버를 돌려준다.
        // 해당 레버의 오브젝트 네임을 확인하여, ExitDoor클래스의 함수로 해당하는 탈출문 애니메이션의 조건을 활성화 시킨다. 
        if (Activate)
        {
            Quaternion targetRotation = Quaternion.Euler(ActivateAngle, 0, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smoot * Time.deltaTime);

            if (this.gameObject.name == "Lever1")
            {
                exitDoor.LeverTrigerOn(1);
            }

            if (this.gameObject.name == "Lever2")
            {
                exitDoor.LeverTrigerOn(2);
            }

            if (this.gameObject.name == "Lever3")
            {
                exitDoor.LeverTrigerOn(3);
            }

        }
        else
        {
            Quaternion targetRotation2 = Quaternion.Euler(UnActivateAngle, 0, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation2, smoot * Time.deltaTime);

            if (this.gameObject.name == "Lever1")
            {
                exitDoor.LeverTrigerOff(1);
            }

            if (this.gameObject.name == "Lever2")
            {
                exitDoor.LeverTrigerOff(2);
            }

            if (this.gameObject.name == "Lever3")
            {
                exitDoor.LeverTrigerOff(3);
            }

        }



    }

}

