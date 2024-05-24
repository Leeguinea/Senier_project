using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ż��θ� Ȱ��ȭ�� ���� 'Ʈ����' Ŭ����
public class Lever : MonoBehaviour
{
    [Header("ExitLever")]
    public bool Activate = false;
    public float ActivateAngle = 60f;
    public float UnActivateAngle = -60f;
    public float smoot = 3f;

    public ExitDoor1 exitDoor1; // ������Ʈ ����
    NoticeMessage noticeMessage; //������Ʈ ����

    void Start()
    {
        noticeMessage = FindObjectOfType<NoticeMessage>(); // NoticeUI�� Ŭ����
        exitDoor1 = FindObjectOfType<ExitDoor1>(); // ExitDoor�� Ŭ���� 

        if (exitDoor1 == null) // ������ �� ���� ��� ���� �޽��� ���
        {
            Debug.LogError("ExitDoor�� ã�� �� �����ϴ�!");
        }

    }


    // ������ ���¸� Ȱ��ȭ, ��Ȱ��ȭ ��Ű�� �Լ�
    public void ChangeLeverState()
    {
        Activate = !Activate; 

        if (exitDoor1 == null) // ������ �� ���� ��� ���� �޽��� ���
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
                exitDoor1.LeverTrigerOn(1);
                GetComponent<BoxCollider>().enabled = false;
            }

            /*
            if (this.gameObject.name == "Lever2")
            {
                exitDoor.LeverTrigerOn(2);
                GetComponent<BoxCollider>().enabled = false;
            }

            if (this.gameObject.name == "Lever3")
            {
                exitDoor.LeverTrigerOn(3);
                GetComponent<BoxCollider>().enabled = false;
            }
            */
        }
        else
        {
            Quaternion targetRotation2 = Quaternion.Euler(UnActivateAngle, 0, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation2, smoot * Time.deltaTime);

            
            if (this.gameObject.name == "Lever1")
            {
                exitDoor1.LeverTrigerOff(1);
            }

            /*
            if (this.gameObject.name == "Lever2")
            {
                exitDoor.LeverTrigerOff(2);
            }

            if (this.gameObject.name == "Lever3")
            {
                exitDoor.LeverTrigerOff(3);
            }
            */

        }



    }

}

