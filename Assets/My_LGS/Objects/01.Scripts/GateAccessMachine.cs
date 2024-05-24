using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateAccessMachine : MonoBehaviour
{
    [Header("GateAccessMachine")]
    public int GateAccessMachineNum; //머신 넘버
    public bool Activate = false;
    public ExitDoor3 exitDoor3; // 컴포넌트 선언

    [Header("Material")]
    public Material NormalActicatMaterial;  // 노랑 (장식용)
    public Material acticatMaterial;        // 초록 (활성화)
    public Material unActicatMaterial;      // 빨강 (비활성화)

    [Header("Sound Effect")]
    public AudioSource accessSuccessSound;  // 성공음
    public AudioSource accessFailureSound;  // 실패음


    void Start()
    {
        exitDoor3 = FindObjectOfType<ExitDoor3>(); // ExitDoor3의 클래스 

        if (exitDoor3 == null) // 참조할 수 없는 경우 오류 메시지 출력
        {
            Debug.LogError("ExitDoor를 찾을 수 없습니다!");
        }

        UpdateMachineMaterial(); // 초기 상태의 재질 설정
    }


    void Update()
    {
        if (Activate)
        {
            if (this.gameObject.name == "GateAccessMachine_01")
            {
                exitDoor3.KeyCardTrigerOn(1);
                GetComponent<BoxCollider>().enabled = false;
                //Debug.Log("Gate Access Machine 01 Access");

            }

            if (this.gameObject.name == "GateAccessMachine_02")
            {
                exitDoor3.KeyCardTrigerOn(2);
                GetComponent<BoxCollider>().enabled = false;
                //Debug.Log("Gate Access Machine 02 Access");

            }

            if (this.gameObject.name == "GateAccessMachine_03")
            {
                exitDoor3.KeyCardTrigerOn(3);
                GetComponent<BoxCollider>().enabled = false;
                //Debug.Log("Gate Access Machine 03 Access");

            }

        }
    }


    // 활성화, 비활성 상태 변경 메소드
    public void ChangeMachineState(int num)
    {
        Activate = !Activate;
        UpdateMachineMaterial();

        if (exitDoor3 == null) // 참조할 수 없는 경우 오류 메시지 출력
        {
            Debug.LogError("ExitDoor를 찾을 수 없습니다!");
            return;
        }

        accessSuccessSound.Play();

    }


    public void FailChange()
    {
        accessFailureSound.volume = 2f; // 현재 볼륨의 2배
        accessFailureSound.Play();
    }


    // 활성화 여부에 따라 색상 변경
    private void UpdateMachineMaterial()
    {
        if (Activate) // 활성화
        {
            this.gameObject.GetComponent<MeshRenderer>().material = acticatMaterial;
        }
        else // 비활성화
        {
            this.gameObject.GetComponent<MeshRenderer>().material = unActicatMaterial;
        }
    }
}
