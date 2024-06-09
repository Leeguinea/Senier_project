using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 세번째 게이트에 사용되는 스크립트
// (3개의 키카드를 이용하여 개문)
public class ExitDoor3 : MonoBehaviour
{
    [Header("ExitDoor3")]
    public Animator Exitani3;           // 애니메이터
    public bool triger1 = false;        // 트리거 머신 1
    public bool triger2 = false;        // 트리거 머신 2
    public bool triger3 = false;        // 트리거 머신 3

    [Header("Sound Effect")]
    public AudioSource gateOpenSound;   // 문이 열릴 때 재생할 오디오 소스

    private bool isGateOpened = false;  // 문이 열렸는지 확인
    

    void Start()
    {
        Exitani3 = GetComponent<Animator>();
        if (Exitani3 == null)
        {
            Debug.LogError("Animator 컴포넌트를 찾을 수 없습니다!");
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
            //Debug.Log("레버 트리거1 활성화");
        }
      
        if (num == 2){
            Exitani3.SetBool("KeyCardTriger2_isOn", true);
            triger2 = true;
            //Debug.Log("레버 트리거2 활성화");
        }
        if (num == 3){
            Exitani3.SetBool("KeyCardTriger3_isOn", true);
            triger3 = true;
            //Debug.Log("레버 트리거3 활성화");
        }
       
    }


    public void KeyCardTrigerOff(int KeyCardTriger_Number)
    {
        int num = KeyCardTriger_Number;

        if (num == 1)
        {
            Exitani3.SetBool("KeyCardTriger1_isOff", false);
            triger1 = false;
            //Debug.Log("레버 트리거1 비활성화");
        }

        if (num == 2)
        {
            Exitani3.SetBool("KeyCardTriger2_isOff", false);
            triger2 = false;
            //Debug.Log("레버 트리거2 비활성화");
        }
        if (num == 3)
        {
            Exitani3.SetBool("KeyCardTriger3_isOff", false);
            triger3 = false;
            //Debug.Log("레버 트리거3 비활성화");
        }
    }


    private void OpenDoor()
    {
        // 문 여는 로직 구현
        isGateOpened = true;
        if (gateOpenSound != null)
        {
            gateOpenSound.Play();
        }

        Debug.Log("세번째 게이트 개문");
    }
}
