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



    public void RechargeTrigerOn(int RechargeTriger_Number)
    {
        int num = RechargeTriger_Number;

        if (num == 1){
            Exitani2.SetBool("RechargeTriger1_isOn", true);
            //Debug.Log("������ �ӽ� Ʈ����1 Ȱ��ȭ");
        }
        gateOpenSound.Play();
    }


}
