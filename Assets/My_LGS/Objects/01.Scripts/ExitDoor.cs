using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 탈출문에 사용되는 스크립트
// 탈출문을 여는 트리거들 작동 여부를 확인하는 함수가 있는 클래스
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
            Debug.LogError("Animator 컴포넌트를 찾을 수 없습니다!");
        }
    }

    public void LeverTrigerOn(int LeverTriger_Number)
    {
        int num = LeverTriger_Number;

        if (num == 1){
            Exitani.SetBool("LeverTriger1_isOn", true);
            triger1 = true;
            //Debug.Log("레버 트리거1 활성화");
        }
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
    }

    public void LeverTrigerOff(int LeverTriger_Number)
    {
        int num = LeverTriger_Number;

        if (num == 1){
            Exitani.SetBool("LeverTriger1_isOn", false);
            triger1 = false;
            //Debug.Log("레버 트리거1 비활성화");
        }
        if (num == 2){
            Exitani.SetBool("LeverTriger2_isOn", false);
            triger2 = false;
            //Debug.Log("레버 트리거2 비활성화");
        }
        if (num == 3){
            Exitani.SetBool("LeverTriger3_isOn", false);
            triger3 = false;
            //Debug.Log("레버 트리거3 비활성화");
        }
    }

}
