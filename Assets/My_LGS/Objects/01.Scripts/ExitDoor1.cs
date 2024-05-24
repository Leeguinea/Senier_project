using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ù��° ����Ʈ�� ���Ǵ� ��ũ��Ʈ
// (1���� ������ �̿��Ͽ� ����)
public class ExitDoor1 : MonoBehaviour
{
    [Header("ExitDoor1")]
    public Animator Exitani1;
    public bool triger1 = false;
    //public bool triger2 = false;
    //public bool triger3 = false;


    void Start()
    {
        Exitani1 = GetComponent<Animator>();
        if (Exitani1 == null)
        {
            Debug.LogError("Animator ������Ʈ�� ã�� �� �����ϴ�!");
        }
    }


    void Update()
    {
        
    }


    public void LeverTrigerOn(int LeverTriger_Number)
    {
        int num = LeverTriger_Number;

        if (num == 1){
            Exitani1.SetBool("LeverTriger1_isOn", true);
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

    public void LeverTrigerOff(int LeverTriger_Number)
    {
        int num = LeverTriger_Number;

        if (num == 1){
            Exitani1.SetBool("LeverTriger1_isOn", false);
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
