using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ż�⹮�� ���Ǵ� ��ũ��Ʈ
// Ż�⹮�� ���� Ʈ���ŵ� �۵� ���θ� Ȯ���ϴ� �Լ��� �ִ� Ŭ����
public class ExitDoor : MonoBehaviour
{
    [Header("ExitAnimaion")]
    public Animator Exitani;
    public bool triger1 = false;
    public bool triger2 = false;
    public bool triger3 = false;


    // Start is called before the first frame update
    void Start()
    {
        Exitani = GetComponent<Animator>();
        if (Exitani == null)
        {
            Debug.LogError("Animator ������Ʈ�� ã�� �� �����ϴ�!");
        }
    }

    public void LeverTrigerOn(int LeverTriger_Number)
    {
        int num = LeverTriger_Number;

        if (num == 1){
            Exitani.SetBool("LeverTriger1_isOn", true);
            triger1 = true;
            //Debug.Log("���� Ʈ����1 Ȱ��ȭ");
        }
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
    }

    public void LeverTrigerOff(int LeverTriger_Number)
    {
        int num = LeverTriger_Number;

        if (num == 1){
            Exitani.SetBool("LeverTriger1_isOn", false);
            triger1 = false;
            //Debug.Log("���� Ʈ����1 ��Ȱ��ȭ");
        }
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
    }

}
