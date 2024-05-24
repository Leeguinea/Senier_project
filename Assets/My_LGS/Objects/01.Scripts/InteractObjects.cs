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
    public GameObject aimingUI;             // ���̹� UI
    public float interactionDistance = 5f;  // ��ȣ�ۿ� �Ÿ�
    public float interactionDuration = 2f;  // ��ȣ�ۿ� �ð�

    private Color interactionColor = new Vector4(0.5f, 0.5f, 0.5f, 1.0f);  // ��ȣ�ۿ� ������ ���� ����
    private Color originalColor = new Vector4(1, 1, 1, 1); // ���� ����

    // ī��Ű ���� ����
    public bool haskey1 = false;
    public bool haskey2 = false;
    public bool haskey3 = false;

    void Start()
    {
        //originalColor = aimingUI.GetComponent<RawImage>().color; // ���̹� UI�� ���� ���� ����
        //Debug.Log(originalColor);
    }

    // �� �����Ӵ� �ѹ��� ȣ��
    void Update()
    {
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
                // ��ȣ�ۿ� ������ ���� �������� ����
                aimingUI.GetComponent<RawImage>().color = interactionColor;

                // ���� ������Ʈ ��ȣ�ۿ�
                if (hit.collider.CompareTag("Paper"))
                {
                    if (Input.GetKeyDown(KeyCode.F)) // 'F'Ű�� ������ ��
                    {
                        // ��ȣ�ۿ� ������ ������Ʈ�� ��ȣ�ۿ��ϴ� �Լ� ȣ��
                        hit.collider.GetComponent<PaperObjects>().isInteracting = true;
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
                        hit.collider.transform.GetComponentInParent<Lever>().ChangeLeverState(); // ������ ���� �ݴ� �Լ� ����
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
                            if (haskey1 == true) // 1�� ī��Ű�� ������ �ִٸ�
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
                            if (haskey2 == true) // 2�� ī��Ű�� ������ �ִٸ�
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
                            if (haskey3 == true) // 3�� ī��Ű�� ������ �ִٸ�
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

            }
            else
            {
                // ����ĳ��Ʈ�� �浹���� ���� ��� ����� ������ �׸��� ����
                //Debug.DrawRay(ray.origin, ray.direction * interactionDistance, Color.white);

                // ��ȣ�ۿ� �Ұ����� ���� ���� �������� ����
                aimingUI.GetComponent<RawImage>().color = originalColor;
            }


         }
        else
        {
            // ���̾ �ƹ� ������Ʈ�� �浹���� ������ ���� �������� ����
            aimingUI.GetComponent<RawImage>().color = originalColor;
        }



    }



}
