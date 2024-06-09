using System;
using UnityEngine;

// Ż��θ� Ȱ��ȭ�� ���� 'Ʈ����' Ŭ����
public class Lever : MonoBehaviour
{
    [Header("ExitLever")]
    public bool Activate = false;
    public float ActivateAngle = 60f;
    public float UnActivateAngle = -60f;
    public float smooth = 3f;

    [Header("Sound Effect")]
    public AudioSource LeverSound;  // ������

    [Header("Gate1")]
    public ExitDoor1 exitDoor1; // ������Ʈ ����

    private NoticeMessage noticeMessage; // ������Ʈ ����

    private Quaternion targetRotation;

    void Start()
    {
        noticeMessage = FindObjectOfType<NoticeMessage>(); // NoticeUI�� Ŭ����
        exitDoor1 = FindObjectOfType<ExitDoor1>(); // ExitDoor�� Ŭ���� 

        if (exitDoor1 == null) // ������ �� ���� ��� ���� �޽��� ���
        {
            Debug.LogError("ExitDoor�� ã�� �� �����ϴ�!");
        }

        targetRotation = transform.localRotation;
    }

    void Update()
    {
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
    }

    // ������ ���¸� Ȱ��ȭ, ��Ȱ��ȭ ��Ű�� �Լ�
    public void ChangeLeverState(int LeverNum)
    {
        Activate = !Activate;

        if (exitDoor1 == null) // ������ �� ���� ��� ���� �޽��� ���
        {
            Debug.LogError("ExitDoor�� ã�� �� �����ϴ�!");
            return;
        }

        if (Activate)
        {
            LeverTriggerOn(LeverNum);
        }
        else
        {
            LeverTriggerOff(LeverNum);
        }
    }

    private void LeverTriggerOn(int LeverNum)
    {
        targetRotation = Quaternion.Euler(ActivateAngle, 0, 0);
        exitDoor1.LeverTrigerOn(LeverNum);
        GetComponent<BoxCollider>().enabled = false;
        LeverSound.Play();
    }

    private void LeverTriggerOff(int LeverNum)
    {
        targetRotation = Quaternion.Euler(UnActivateAngle, 0, 0);
        exitDoor1.LeverTrigerOff(LeverNum); // Ż�⹮�� �ݴ� �Լ� ȣ��
        GetComponent<BoxCollider>().enabled = true;
        LeverSound.Play();
    }
}
