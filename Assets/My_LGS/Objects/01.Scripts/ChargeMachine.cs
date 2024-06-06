using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class ChargeMachine : MonoBehaviour
{
    [Header("ChargeMachine")]
    public Animator ChargMachineAnimator;

    [Header("Sound Effect")]
    public AudioSource chageBatterySound;   // 문이 열릴 때 재생할 오디오 소스

    [Header("Gate2")]
    public ExitDoor2 exitDoor2;

    public bool isCharging = false;

    // Start is called before the first frame update
    void Start()
    {
        ChargMachineAnimator = GetComponent<Animator>();
        if (ChargMachineAnimator == null)
        {
            Debug.LogError("Animator 컴포넌트를 찾을 수 없습니다!");
        }

        exitDoor2 = FindObjectOfType<ExitDoor2>(); // ExitDoor의 클래스
        if (exitDoor2 == null)
        {
            //Debug.Log("exitDoor2 컴포넌트를 찾을 수 없습니다!");
        }

    }


    public void ChargingMachine()
    {
        StartCoroutine(ChageBatteryAni());
    }

    IEnumerator ChageBatteryAni()
    {
        Debug.Log("애니메이터 on");
        ChargMachineAnimator.SetTrigger("ChargeBattery");

        // 애니메이션 상태가 변경될 때까지 대기
        //yield return new WaitForSeconds(0.1f);

        // 현재 애니메이션 상태 정보를 가져옴
        AnimatorStateInfo stateInfo = ChargMachineAnimator.GetCurrentAnimatorStateInfo(0);

        // 애니메이션 상태가 변경되었는지 확인하고, 완료될 때까지 대기
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
