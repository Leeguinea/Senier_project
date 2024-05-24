using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

//플레이어가 오블젝트와 상호작용을 하기 위한 스크립트
//지속적으로 플레이어와 오브젝트의 콜라이더가 충돌하는지 확인하고 
//필요한 클래스의 함수들을 가져와서 각 오브젝트들과 상호작용
public class InteractObjects : MonoBehaviour
{
    public GameObject aimingUI;             // 에이밍 UI
    public float interactionDistance = 5f;  // 상호작용 거리
    public float interactionDuration = 2f;  // 상호작용 시간

    private Color interactionColor = new Vector4(0.5f, 0.5f, 0.5f, 1.0f);  // 상호작용 가능할 때의 색깔
    private Color originalColor = new Vector4(1, 1, 1, 1); // 원래 색깔

    // 카드키 소유 여부
    public bool haskey1 = false;
    public bool haskey2 = false;
    public bool haskey3 = false;

    void Start()
    {
        //originalColor = aimingUI.GetComponent<RawImage>().color; // 에이밍 UI의 원래 색상 저장
        //Debug.Log(originalColor);
    }

    // 매 프레임당 한번씩 호출
    void Update()
    {
        // 플레이어 레이어의 인덱스 가져오기
        int playerLayerIndex = LayerMask.NameToLayer("Player");

        // 플레이어 레이어를 제외한 모든 레이어를 포함하도록 레이어 마스크 설정
        int layerMask = ~(1 << playerLayerIndex);


        // 화면 중앙 좌표 계산
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        // 레이캐스트 생성
        RaycastHit hit;

        // 레이캐스트 경로 확인
        //Debug.DrawRay(ray.origin, ray.direction, Color.yellow);


        // 레이캐스트 충돌 오브젝트 확인
        if (Physics.Raycast(ray, out hit, interactionDistance, layerMask))
        {
            Debug.Log("충돌 확인 : " + hit.collider.gameObject.name);
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red); // 디버그 라인으로 레이캐스트 시각적으로 표시

            // 충돌한 오브젝트의 레이어 확인
            int objectLayer = hit.collider.gameObject.layer;
            string objectLayerName = LayerMask.LayerToName(objectLayer);


            if (objectLayerName == "Interactable") // 상호작용 가능한 태그가 있는 오브젝트와 충돌했을 때
            {
                // 상호작용 가능할 때의 색상으로 변경
                aimingUI.GetComponent<RawImage>().color = interactionColor;

                // 문서 오브젝트 상호작용
                if (hit.collider.CompareTag("Paper"))
                {
                    if (Input.GetKeyDown(KeyCode.F)) // 'F'키를 눌렀을 때
                    {
                        // 상호작용 가능한 오브젝트와 상호작용하는 함수 호출
                        hit.collider.GetComponent<PaperObjects>().isInteracting = true;
                    }
                }


                // 문 오브젝트 상호작용
                if (hit.collider.CompareTag("Door")) 
                {
                    if (Input.GetKeyDown(KeyCode.F)) // F키를 눌러 오브젝트 상호작용                   
                    {
                        hit.collider.transform.GetComponentInParent<OpenCloseDoor>().ChangeDoorState(); // 문을 열고 닫는 함수 실행
                    }
                }


                // 레버 오브젝트 상호작용
                if (hit.collider.CompareTag("Lever")) // 레이케스트 Lever의 콜라이더 충돌 시
                {
                    if (Input.GetKeyDown(KeyCode.F)) // F키를 눌러 오브젝트 상호작용                   
                    {
                        hit.collider.transform.GetComponentInParent<Lever>().ChangeLeverState(); // 레버를 열고 닫는 함수 실행
                    }
                }


                // 포탈 오브젝트 상호작용
                if (hit.collider.CompareTag("Potal"))
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        hit.collider.transform.GetComponentInParent<Portal>().UsePortal(); // 포탈을 사용하여 다음 씬으로

                    }
                }


                // 키 카드 엑세스 머신과 상호작용
                if (hit.collider.CompareTag("AccessMachine"))
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        if (hit.collider.name == "GateAccessMachine_01")
                        {
                            if (haskey1 == true) // 1번 카드키를 가지고 있다면
                            {
                                // 1번 키카드 엑세스 머신을 활성화
                                hit.collider.transform.GetComponent<GateAccessMachine>().ChangeMachineState(1);
                            }
                            else 
                            {
                                hit.collider.transform.GetComponent<GateAccessMachine>().FailChange(); // 실패음 재생
                                //Debug.Log("1번 카드키 없음. 엑세스 불가.");
                            }
                        }
                        if (hit.collider.name == "GateAccessMachine_02")
                        {
                            if (haskey2 == true) // 2번 카드키를 가지고 있다면
                            {
                                // 2번 키카드 엑세스 머신을 활성화
                                hit.collider.transform.GetComponent<GateAccessMachine>().ChangeMachineState(2);
                            }
                            else 
                            {
                                hit.collider.transform.GetComponent<GateAccessMachine>().FailChange(); // 실패음 재생
                                //Debug.Log("2번 카드키 없음. 엑세스 불가.");
                            }
                        }
                        if (hit.collider.name == "GateAccessMachine_03")
                        {
                            if (haskey3 == true) // 3번 카드키를 가지고 있다면
                            {
                                // 1번 키카드 엑세스 머신을 활성화
                                hit.collider.transform.GetComponent<GateAccessMachine>().ChangeMachineState(3);
                            }
                            else 
                            {
                                hit.collider.transform.GetComponent<GateAccessMachine>().FailChange(); // 실패음 재생
                                //Debug.Log("3번 카드키 없음. 엑세스 불가.");
                            }
                        }

                    }
                }

            }
            else
            {
                // 레이캐스트가 충돌하지 않은 경우 디버그 라인을 그리지 않음
                //Debug.DrawRay(ray.origin, ray.direction * interactionDistance, Color.white);

                // 상호작용 불가능할 때의 원래 색상으로 변경
                aimingUI.GetComponent<RawImage>().color = originalColor;
            }


         }
        else
        {
            // 레이어에 아무 오브젝트도 충돌하지 않으면 원래 색상으로 변경
            aimingUI.GetComponent<RawImage>().color = originalColor;
        }



    }



}
