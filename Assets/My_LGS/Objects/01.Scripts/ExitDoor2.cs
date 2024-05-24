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

        /*
        if (num == 2){
            Exitani.SetBool("LeverTriger2_isOn", true);
            triger2 = true;
            //Debug.Log("���� Ʈ����2 Ȱ��ȭ");
        }
        if (num == 3){
            Exitani.SetBool("LeverTriger3_isOn", true);
            triger3 = true;
            //Debug.Log("���� Ʈ����3 Ȱ��ȭ");
        }
        */
    }

    public void RechargeTrigerOff(int LeverTriger_Number)
    {
        int num = LeverTriger_Number;

        if (num == 1){
            Exitani2.SetBool("RechargeTriger1_isOn", false);
            triger1 = false;
            //Debug.Log("���� Ʈ����1 ��Ȱ��ȭ");
        }

        /*
        if (num == 2){
            Exitani.SetBool("LeverTriger2_isOn", false);
            triger2 = false;
            //Debug.Log("���� Ʈ����2 ��Ȱ��ȭ");
        }
        if (num == 3){
            Exitani.SetBool("LeverTriger3_isOn", false);
            triger3 = false;
            //Debug.Log("���� Ʈ����3 ��Ȱ��ȭ");
        }
        */
    }

}
