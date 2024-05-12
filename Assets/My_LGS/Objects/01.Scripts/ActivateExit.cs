using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ż��θ� Ȱ��ȭ�� ���� 'Ʈ����' Ŭ����
public class ActivateExit : MonoBehaviour
{
    [Header("ExitLever")]
    public bool Activate = false;
    public float ActivateAngle = 60f;
    public float UnActivateAngle = -60f;
    public float smoot = 3f;

    public ExitDoor exitDoor; // ������Ʈ ����
    NoticeMessage noticeMessage; //������Ʈ ����

    void Start()
    {
        noticeMessage = FindObjectOfType<NoticeMessage>(); // NoticeUI�� Ŭ����
        exitDoor = FindObjectOfType<ExitDoor>(); // ExitDoor�� Ŭ���� 

        if (exitDoor == null) // ������ �� ���� ��� ���� �޽��� ���
        {
            Debug.LogError("ExitDoor�� ã�� �� �����ϴ�!");
        }

    }


    // ������ ���¸� Ȱ��ȭ, ��Ȱ��ȭ ��Ű�� �Լ�
    public void ChangeLeverState()
    {
        Activate = !Activate; 

        if (exitDoor == null) // ������ �� ���� ��� ���� �޽��� ���
        {
            Debug.LogError("ExitDoor�� ã�� �� �����ϴ�!");
            return;
        }

    }


    void Update()
    {
        // Ȱ��ȭ�ȴٸ� ������ �����ش�.
        // �ش� ������ ������Ʈ ������ Ȯ���Ͽ�, ExitDoorŬ������ �Լ��� �ش��ϴ� Ż�⹮ �ִϸ��̼��� ������ Ȱ��ȭ ��Ų��. 
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

