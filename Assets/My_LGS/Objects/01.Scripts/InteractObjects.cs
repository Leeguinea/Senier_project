using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

//플레이어가 오블젝트와 상호작용을 하기 위한 스크립트
//지속적으로 플레이어와 오브젝트의 콜라이더가 충돌하는지 확인하고 
//필요한 클래스의 함수들을 가져와서 각 오브젝트들과 상호작용
public class InteractScripts : MonoBehaviour
{
    public float interactDiastance = 5f;    //상호작용 거리
    NoticeMessage noticeMessage; //컴포넌트 선언
    GameObject nearObject; //가까이 있는 오브젝트에 관한 변수

    // 시작 시 첫번째 프레임 업데이트 이전에 호출
    void Start()
    {
        noticeMessage = FindObjectOfType<NoticeMessage>();
    }

    // 매 프레임당 한번씩 호출
    void Update()
    {
        /*
         Ray ray = new Ray(transform.position, transform.forward); // 레이케스트
         RaycastHit hit;

         if (Physics.Raycast(ray, out hit, interactDiastance))
         {
             if (hit.collider.CompareTag("Door")) // 레이케스트 Door의 콜라이더 충돌 시
             {
                 //Debug.Log("문 충돌 확인");
                 //noticeMessage.Notice("F키를 눌러 상호작용");

                 if (Input.GetKeyDown(KeyCode.F)) // F키를 눌러 오브젝트 상호작용                   
                 {
                     hit.collider.transform.GetComponentInParent<OpenCloseDoor>().ChangeDoorState(); // 문을 열고 닫는 함수 실행
                 }
             }

             if (hit.collider.CompareTag("Lever")) // 레이케스트 Lever의 콜라이더 충돌 시
             {
                 //Debug.Log("레버 충돌 확인");
                 //noticeMessage.Notice("F키를 눌러 상호작용");

                 if (Input.GetKeyDown(KeyCode.F)) // F키를 눌러 오브젝트 상호작용                   
                 {
                     hit.collider.transform.GetComponentInParent<ActivateExit>().ChangeLeverState(); // 레버를 열고 닫는 함수 실행
                     noticeMessage.Notice("레버 활성화");
                 }
             }

             if (hit.collider.CompareTag("Potal")) // 레이케스트가 Potal의 콜라이더 충돌 시
             {
                 //Debug.Log("포탈 충돌 확인");
                 hit.collider.transform.GetComponentInParent<LoadScene>().LoadEndingScene(); // 엔딩씬으로 이동
                 //noticeMessage.Notice("포탈 활성화");
             }

             if (hit.collider.CompareTag("Heal"))
             {
                 Debug.Log("회복킷 충돌 확인");
                 if (Input.GetKeyDown(KeyCode.F))
                 {
                     playerStatus.Heal(10);
                     Debug.Log("체력 회복");
                 }
             }
         }
        */

        

    }

    //가까이 있는 오브젝트 확인
    void OnTriggerEnterStay(Collider other)
    {
        if(other.tag == "Heal")
        {

        }
    }
    void OnTriggerEnterExit(Collider other)
    {

    }

}
