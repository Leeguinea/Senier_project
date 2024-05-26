using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����° ����Ʈ�� ���Ǵ� ��ũ��Ʈ
// (3���� Űī�带 �̿��Ͽ� ����)
public class ExitDoor3 : MonoBehaviour
{
    [Header("ExitDoor3")]
    public Animator Exitani3;           // �ִϸ�����
    public bool triger1 = false;        // Ʈ���� �ӽ� 1
    public bool triger2 = false;        // Ʈ���� �ӽ� 2
    public bool triger3 = false;        // Ʈ���� �ӽ� 3

    [Header("Sound Effect")]
    public AudioSource gateOpenSound;   // ���� ���� �� ����� ����� �ҽ�

    private bool isGateOpened = false;  // ���� ���ȴ��� Ȯ��
    

    void Start()
    {
        Exitani3 = GetComponent<Animator>();
        if (Exitani3 == null)
        {
            Debug.LogError("Animator ������Ʈ�� ã�� �� �����ϴ�!");
        }
    }


    void Update()
    {
        if (triger1 && triger2 && triger3 && !isGateOpened)
        {
            OpenDoor();
        }
    }


    public void KeyCardTrigerOn(int KeyCardTriger_Number)
    {
        int num = KeyCardTriger_Number;

        if (num == 1){
            Exitani3.SetBool("KeyCardTriger1_isOn", true);
            triger1 = true;
            //Debug.Log("���� Ʈ����1 Ȱ��ȭ");
        }
      
        if (num == 2){
            Exitani3.SetBool("KeyCardTriger2_isOn", true);
            triger2 = true;
            //Debug.Log("���� Ʈ����2 Ȱ��ȭ");
        }
        if (num == 3){
            Exitani3.SetBool("KeyCardTriger3_isOn", true);
            triger3 = true;
            //Debug.Log("���� Ʈ����3 Ȱ��ȭ");
        }
       
    }


    public void KeyCardTrigerOff(int KeyCardTriger_Number)
    {
        int num = KeyCardTriger_Number;

        if (num == 1)
        {
            Exitani3.SetBool("KeyCardTriger1_isOff", false);
            triger1 = false;
            //Debug.Log("���� Ʈ����1 ��Ȱ��ȭ");
        }

        if (num == 2)
        {
            Exitani3.SetBool("KeyCardTriger2_isOff", false);
            triger2 = false;
            //Debug.Log("���� Ʈ����2 ��Ȱ��ȭ");
        }
        if (num == 3)
        {
            Exitani3.SetBool("KeyCardTriger3_isOff", false);
            triger3 = false;
            //Debug.Log("���� Ʈ����3 ��Ȱ��ȭ");
        }
    }


    private void OpenDoor()
    {
        // �� ���� ���� ����
        isGateOpened = true;
        if (gateOpenSound != null)
        {
            gateOpenSound.Play();
        }

        Debug.Log("����° ����Ʈ ����");
    }
}
