using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

//�÷��̾ ������Ʈ�� ��ȣ�ۿ��� �ϱ� ���� ��ũ��Ʈ
//���������� �÷��̾�� ������Ʈ�� �ݶ��̴��� �浹�ϴ��� Ȯ���ϰ� 
//�ʿ��� Ŭ������ �Լ����� �����ͼ� �� ������Ʈ��� ��ȣ�ۿ�
public class InteractObjects : MonoBehaviour
{
    [Header("Interacted Aim")]
    public GameObject InteractedAimUI;             // ���̹� UI

    [Header("Interact Setting")]
    public float interactionDistance = 5f;  // ��ȣ�ۿ� �Ÿ�
    public float interactionDuration = 2f;  // ��ȣ�ۿ� �ð�
    public AudioSource earnItemSound;

    [Header("Has Item")] // ī��Ű ���� ����
    public bool hasKey1 = false;
    public bool hasKey2 = false;
    public bool hasKey3 = false;
    public bool hasBattery = false;

    NoticeMessage noticeMessage;

    private void Awake()
    {
        noticeMessage = FindObjectOfType<NoticeMessage>();
    }

    void Start()
    {
        InteractedAimUI.SetActive(false);
    }

    // �� �����Ӵ� �ѹ��� ȣ��
    void Update()
    {
        if (Time.timeScale == 0f)
        {
            // ������ �Ͻ������� ���, Ű �Է� ����
            return;
        }

        // �÷��̾� ���̾��� �ε��� ��������
        int playerLayerIndex = LayerMask.NameToLayer("Player");

        // �÷��̾� ���̾ ������ ��� ���̾ �����ϵ��� ���̾� ����ũ ����
        int layerMask = ~(1 << playerLayerIndex);


        // ȭ�� �߾� ��ǥ ���
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        // ����ĳ��Ʈ ����
        RaycastHit hit;

        // ����ĳ��Ʈ ��� Ȯ��
        //Debug.DrawRay(ray.origin, ray.direction, Color.yellow);


        // ����ĳ��Ʈ �浹 ������Ʈ Ȯ��
        if (Physics.Raycast(ray, out hit, interactionDistance, layerMask))
        {
            Debug.Log("�浹 Ȯ�� : " + hit.collider.gameObject.name);
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red); // ����� �������� ����ĳ��Ʈ �ð������� ǥ��

            // �浹�� ������Ʈ�� ���̾� Ȯ��
            int objectLayer = hit.collider.gameObject.layer;
            string objectLayerName = LayerMask.LayerToName(objectLayer);


            if (objectLayerName == "Interactable") // ��ȣ�ۿ� ������ �±װ� �ִ� ������Ʈ�� �浹���� ��
            {
                // ��ȣ�ۿ� ������ �� ��ȣ�ۿ� UI Ȱ��ȭ
                InteractedAimUI.SetActive(true);

                // ���� ������Ʈ ��ȣ�ۿ�
                if (hit.collider.CompareTag("Paper"))
                {
                    if (Input.GetKeyDown(KeyCode.F)) // 'F'Ű�� ������ ��
                    {
                        // ��ȣ�ۿ� ������ ������Ʈ�� ��ȣ�ۿ��ϴ� �Լ� ȣ��
                        if (hit.collider.GetComponent<PaperObjects>().isInteracting == false)
                        {
                            hit.collider.GetComponent<PaperObjects>().isInteracting = true;
                        }
                        else
                        {
                            hit.collider.GetComponent<PaperObjects>().isInteracting = false;
                        }
                        
                    }
                }


                // �� ������Ʈ ��ȣ�ۿ�
                if (hit.collider.CompareTag("Door")) 
                {
                    if (Input.GetKeyDown(KeyCode.F)) // FŰ�� ���� ������Ʈ ��ȣ�ۿ�                   
                    {
                        hit.collider.transform.GetComponentInParent<OpenCloseDoor>().ChangeDoorState(); // ���� ���� �ݴ� �Լ� ����
                    }
                }


                // ���� ������Ʈ ��ȣ�ۿ�
                if (hit.collider.CompareTag("Lever")) // �����ɽ�Ʈ Lever�� �ݶ��̴� �浹 ��
                {
                    if (Input.GetKeyDown(KeyCode.F)) // FŰ�� ���� ������Ʈ ��ȣ�ۿ�                   
                    {
                        hit.collider.transform.GetComponentInParent<Lever>().ChangeLeverState(1); // ������ ���� �ݴ� �Լ� ����
                        
                        noticeMessage.DisplayNotice("���� Ȱ��ȭ");
                    }
                }


                // ��Ż ������Ʈ ��ȣ�ۿ�
                if (hit.collider.CompareTag("Potal"))
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        hit.collider.transform.GetComponentInParent<Portal>().UsePortal(); // ��Ż�� ����Ͽ� ���� ������

                    }
                }


                // Ű ī�� ������ �ӽŰ� ��ȣ�ۿ�
                if (hit.collider.CompareTag("AccessMachine"))
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        if (hit.collider.name == "GateAccessMachine_01")
                        {
                            if (hasKey1 == true) // 1�� ī��Ű�� ������ �ִٸ�
                            {
                                // 1�� Űī�� ������ �ӽ��� Ȱ��ȭ
                                hit.collider.transform.GetComponent<GateAccessMachine>().ChangeMachineState(1);
                            }
                            else 
                            {
                                hit.collider.transform.GetComponent<GateAccessMachine>().FailChange(); // ������ ���
                                //Debug.Log("1�� ī��Ű ����. ������ �Ұ�.");
                            }
                        }
                        if (hit.collider.name == "GateAccessMachine_02")
                        {
                            if (hasKey2 == true) // 2�� ī��Ű�� ������ �ִٸ�
                            {
                                // 2�� Űī�� ������ �ӽ��� Ȱ��ȭ
                                hit.collider.transform.GetComponent<GateAccessMachine>().ChangeMachineState(2);
                            }
                            else 
                            {
                                hit.collider.transform.GetComponent<GateAccessMachine>().FailChange(); // ������ ���
                                //Debug.Log("2�� ī��Ű ����. ������ �Ұ�.");
                            }
                        }
                        if (hit.collider.name == "GateAccessMachine_03")
                        {
                            if (hasKey3 == true) // 3�� ī��Ű�� ������ �ִٸ�
                            {
                                // 1�� Űī�� ������ �ӽ��� Ȱ��ȭ
                                hit.collider.transform.GetComponent<GateAccessMachine>().ChangeMachineState(3);
                            }
                            else 
                            {
                                hit.collider.transform.GetComponent<GateAccessMachine>().FailChange(); // ������ ���
                                //Debug.Log("3�� ī��Ű ����. ������ �Ұ�.");
                            }
                        }

                    }
                }


                // ������ ����Ʈ
                if (hit.collider.CompareTag("ChargeMachine"))
                {
                    if (hit.collider.name == "ChargedBattery") // ������ ������Ʈ
                    {
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            hasBattery = true;
                            hit.collider.gameObject.SetActive(false); //������Ʈ�� ��Ȱ��ȭ

                            earnItemSound.Play(); // ȹ����
                        }
                    }

                    if (hit.collider.name == "ChargeMachine") // ���͸� ������ ������Ʈ
                    {
                        if (hasBattery == true) // ���͸� ���� ����
                        {
                            if (Input.GetKeyDown(KeyCode.F))
                            {
                                hit.collider.transform.GetComponent<ChargeMachine>().ChargingMachine();
                                hasBattery = false;
                            }
                        }
                        else // ���͸� �̼��� ����
                        {
                            if (Input.GetKeyDown(KeyCode.F))
                            {
                                InteractedAimUI.SetActive(false);
                            }
                        }
                    }
                }




            }
            else
            {
                // ����ĳ��Ʈ�� �浹���� ���� ��� ����� ������ �׸��� ����
                //Debug.DrawRay(ray.origin, ray.direction * interactionDistance, Color.white);

                // ��ȣ�ۿ� �Ұ����� �� ��ȣ�ۿ� UI ��Ȱ��ȭ
                InteractedAimUI.SetActive(false);
            }


         }
        else
        {
            // ���̾ �ƹ� ������Ʈ�� �浹���� ������ ��ȣ�ۿ� UI ��Ȱ��ȭ
            InteractedAimUI.SetActive(false);
        }



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            earnItemSound.Play();
        }
    }

}
