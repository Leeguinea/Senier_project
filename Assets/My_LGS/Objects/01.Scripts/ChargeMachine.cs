using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class ChargeMachine : MonoBehaviour
{
    [Header("ChargeMachine")]
    public Animator ChargMachineAnimator;

    [Header("Sound Effect")]
    public AudioSource chageBatterySound;   // ���� ���� �� ����� ����� �ҽ�

    [Header("Gate2")]
    public ExitDoor2 exitDoor2;

    public bool isCharging = false;

    // Start is called before the first frame update
    void Start()
    {
        ChargMachineAnimator = GetComponent<Animator>();
        if (ChargMachineAnimator == null)
        {
            Debug.LogError("Animator ������Ʈ�� ã�� �� �����ϴ�!");
        }

        exitDoor2 = FindObjectOfType<ExitDoor2>(); // ExitDoor�� Ŭ����
        if (exitDoor2 == null)
        {
            //Debug.Log("exitDoor2 ������Ʈ�� ã�� �� �����ϴ�!");
        }

    }


    public void ChargingMachine()
    {
        StartCoroutine(ChageBatteryAni());
    }

    IEnumerator ChageBatteryAni()
    {
        Debug.Log("�ִϸ����� on");
        ChargMachineAnimator.SetTrigger("ChargeBattery");

        // �ִϸ��̼� ���°� ����� ������ ���
        //yield return new WaitForSeconds(0.1f);

        // ���� �ִϸ��̼� ���� ������ ������
        AnimatorStateInfo stateInfo = ChargMachineAnimator.GetCurrentAnimatorStateInfo(0);

        // �ִϸ��̼� ���°� ����Ǿ����� Ȯ���ϰ�, �Ϸ�� ������ ���
        yield return new WaitForSeconds(stateInfo.length);
    }
    public void StartExitDoor2()
    {
        exitDoor2.RechargeTrigerOn(1);
        isCharging = true;
    }


    public void PlayChangeSound()
    {
        if (chageBatterySound != null)
        {
            chageBatterySound.Play();
        }

    }


}
