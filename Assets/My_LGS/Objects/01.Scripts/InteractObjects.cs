using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

//�÷��̾ ������Ʈ�� ��ȣ�ۿ��� �ϱ� ���� ��ũ��Ʈ
//���������� �÷��̾�� ������Ʈ�� �ݶ��̴��� �浹�ϴ��� Ȯ���ϰ� 
//�ʿ��� Ŭ������ �Լ����� �����ͼ� �� ������Ʈ��� ��ȣ�ۿ�
public class InteractScripts : MonoBehaviour
{
    public float interactDiastance = 5f;    //��ȣ�ۿ� �Ÿ�
    NoticeMessage noticeMessage; //������Ʈ ����
    GameObject nearObject; //������ �ִ� ������Ʈ�� ���� ����

    // ���� �� ù��° ������ ������Ʈ ������ ȣ��
    void Start()
    {
        noticeMessage = FindObjectOfType<NoticeMessage>();
    }

    // �� �����Ӵ� �ѹ��� ȣ��
    void Update()
    {
        /*
         Ray ray = new Ray(transform.position, transform.forward); // �����ɽ�Ʈ
         RaycastHit hit;

         if (Physics.Raycast(ray, out hit, interactDiastance))
         {
             if (hit.collider.CompareTag("Door")) // �����ɽ�Ʈ Door�� �ݶ��̴� �浹 ��
             {
                 //Debug.Log("�� �浹 Ȯ��");
                 //noticeMessage.Notice("FŰ�� ���� ��ȣ�ۿ�");

                 if (Input.GetKeyDown(KeyCode.F)) // FŰ�� ���� ������Ʈ ��ȣ�ۿ�                   
                 {
                     hit.collider.transform.GetComponentInParent<OpenCloseDoor>().ChangeDoorState(); // ���� ���� �ݴ� �Լ� ����
                 }
             }

             if (hit.collider.CompareTag("Lever")) // �����ɽ�Ʈ Lever�� �ݶ��̴� �浹 ��
             {
                 //Debug.Log("���� �浹 Ȯ��");
                 //noticeMessage.Notice("FŰ�� ���� ��ȣ�ۿ�");

                 if (Input.GetKeyDown(KeyCode.F)) // FŰ�� ���� ������Ʈ ��ȣ�ۿ�                   
                 {
                     hit.collider.transform.GetComponentInParent<ActivateExit>().ChangeLeverState(); // ������ ���� �ݴ� �Լ� ����
                     noticeMessage.Notice("���� Ȱ��ȭ");
                 }
             }

             if (hit.collider.CompareTag("Potal")) // �����ɽ�Ʈ�� Potal�� �ݶ��̴� �浹 ��
             {
                 //Debug.Log("��Ż �浹 Ȯ��");
                 hit.collider.transform.GetComponentInParent<LoadScene>().LoadEndingScene(); // ���������� �̵�
                 //noticeMessage.Notice("��Ż Ȱ��ȭ");
             }

             if (hit.collider.CompareTag("Heal"))
             {
                 Debug.Log("ȸ��Ŷ �浹 Ȯ��");
                 if (Input.GetKeyDown(KeyCode.F))
                 {
                     playerStatus.Heal(10);
                     Debug.Log("ü�� ȸ��");
                 }
             }
         }
        */

        

    }

    //������ �ִ� ������Ʈ Ȯ��
    void OnTriggerEnterStay(Collider other)
    {
        if(other.tag == "Heal")
        {

        }
    }
    void OnTriggerEnterExit(Collider other)
    {

    }

}
