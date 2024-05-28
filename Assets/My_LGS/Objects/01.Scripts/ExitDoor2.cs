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
    public bool triger1 = false;
    //public bool triger2 = false;
    //public bool triger3 = false;

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


    void Update()
    {
        
    }


    public void RechargeTrigerOn(int LeverTriger_Number)
    {
        int num = LeverTriger_Number;

        if (num == 1){
            Exitani2.SetBool("RechargeTriger1_isOn", true);
            triger1 = true;
            //Debug.Log("레버 트리거1 활성화");
        }
    }


}
