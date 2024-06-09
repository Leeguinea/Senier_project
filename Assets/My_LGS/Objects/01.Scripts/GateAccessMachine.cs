using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateAccessMachine : MonoBehaviour
{
    [Header("GateAccessMachine")]
    public int GateAccessMachineNum; //�ӽ� �ѹ�
    public bool Activate = false;
    public ExitDoor3 exitDoor3; // ������Ʈ ����

    [Header("Material")]
    public Material NormalActicatMaterial;  // ��� (��Ŀ�)
    public Material acticatMaterial;        // �ʷ� (Ȱ��ȭ)
    public Material unActicatMaterial;      // ���� (��Ȱ��ȭ)

    [Header("Sound Effect")]
    public AudioSource accessSuccessSound;  // ������
    public AudioSource accessFailureSound;  // ������


    void Start()
    {
        exitDoor3 = FindObjectOfType<ExitDoor3>(); // ExitDoor3�� Ŭ���� 

        if (exitDoor3 == null) // ������ �� ���� ��� ���� �޽��� ���
        {
            Debug.LogError("ExitDoor�� ã�� �� �����ϴ�!");
        }

        UpdateMachineMaterial(); // �ʱ� ������ ���� ����
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


    // Ȱ��ȭ, ��Ȱ�� ���� ���� �޼ҵ�
    public void ChangeMachineState(int num)
    {
        Activate = !Activate;
        UpdateMachineMaterial();

        if (exitDoor3 == null) // ������ �� ���� ��� ���� �޽��� ���
        {
            Debug.LogError("ExitDoor�� ã�� �� �����ϴ�!");
            return;
        }

        accessSuccessSound.Play();

    }


    public void FailChange()
    {
        accessFailureSound.volume = 2f; // ���� ������ 2��
        accessFailureSound.Play();
    }


    // Ȱ��ȭ ���ο� ���� ���� ����
    private void UpdateMachineMaterial()
    {
        if (Activate) // Ȱ��ȭ
        {
            this.gameObject.GetComponent<MeshRenderer>().material = acticatMaterial;
        }
        else // ��Ȱ��ȭ
        {
            this.gameObject.GetComponent<MeshRenderer>().material = unActicatMaterial;
        }
    }
}
