using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 첫번째 게이트에 사용되는 스크립트
// (충전지를 이용하여 개문)
public class ExitDoor2 : MonoBehaviour
{
    [Header("ExitDoor2")]
    public Animator Exitani2;

    [Header("Sound Effect")]
    public AudioSource gateOpenSound;   // 문이 열릴 때 재생할 오디오 소스

    void Start()
    {
        Exitani2 = GetComponent<Animator>();
        if (Exitani2 == null)
        {
            Debug.LogError("Animator 컴포넌트를 찾을 수 없습니다!");
        }
    }



    public void RechargeTrigerOn(int RechargeTriger_Number)
    {
        int num = RechargeTriger_Number;

        if (num == 1){
            Exitani2.SetBool("RechargeTriger1_isOn", true);
            //Debug.Log("충전지 머신 트리거1 활성화");
        }
        gateOpenSound.Play();
    }


}
