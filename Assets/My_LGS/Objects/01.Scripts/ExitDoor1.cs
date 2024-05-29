using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 첫번째 게이트에 사용되는 스크립트
// (1개의 레버를 이용하여 개문)
public class ExitDoor1 : MonoBehaviour
{
    [Header("ExitDoor1")]
    public Animator Exitani1;

    [Header("Sound Effect")]
    public AudioSource gateOpenSound;   // 문이 열릴 때 재생할 오디오 소스


    void Start()
    {
        Exitani1 = GetComponent<Animator>();
        if (Exitani1 == null)
        {
            Debug.LogError("Animator 컴포넌트를 찾을 수 없습니다!");
        }
    }


    public void LeverTrigerOn(int LeverTriger_Number)
    {
        int num = LeverTriger_Number;

        if (num == 1){
            Exitani1.SetBool("LeverTriger1_isOn", true);
            //Debug.Log("레버 트리거1 활성화");

            gateOpenSound.Play();
        }

        /*
        if (num == 2){
            Exitani.SetBool("LeverTriger2_isOn", true);
            triger2 = true;
            //Debug.Log("레버 트리거2 활성화");
        }
        if (num == 3){
            Exitani.SetBool("LeverTriger3_isOn", true);
            triger3 = true;
            //Debug.Log("레버 트리거3 활성화");
        }
        */
    }


    public void LeverTrigerOff(int LeverTriger_Number)
    {
        int num = LeverTriger_Number;

        if (num == 1)
        {
            Exitani1.SetBool("LeverTriger1_isOn", false);
            //Debug.Log("레버 트리거1 활성화");
        }

        /*
        if (num == 2){
            Exitani.SetBool("LeverTriger2_isOn", false);
            triger2 = true;
            //Debug.Log("레버 트리거2 활성화");
        }
        if (num == 3){
            Exitani.SetBool("LeverTriger3_isOn", false);
            triger3 = true;
            //Debug.Log("레버 트리거3 활성화");
        }
        */
        gateOpenSound.Play();
    }


}
