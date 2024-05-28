using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ù��° ����Ʈ�� ���Ǵ� ��ũ��Ʈ
// (�������� �̿��Ͽ� ����)
public class ExitDoor2 : MonoBehaviour
{
    [Header("ExitDoor2")]
    public Animator Exitani2;
    public bool triger1 = false;
    //public bool triger2 = false;
    //public bool triger3 = false;

    [Header("Sound Effect")]
    public AudioSource gateOpenSound;   // ���� ���� �� ����� ����� �ҽ�

    void Start()
    {
        Exitani2 = GetComponent<Animator>();
        if (Exitani2 == null)
        {
            Debug.LogError("Animator ������Ʈ�� ã�� �� �����ϴ�!");
        }
    }


    void Update()
    {
        
    }


    public void RechargeTrigerOn(int LeverTriger_Number)
    {
        int num = LeverTriger_Number;

        if (num == 1){
            Exitani2.SetBool("RechargeTriger1_isOn", true);
            triger1 = true;
            //Debug.Log("���� Ʈ����1 Ȱ��ȭ");
        }
    }


}
