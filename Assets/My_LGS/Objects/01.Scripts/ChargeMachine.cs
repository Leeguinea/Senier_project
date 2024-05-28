using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeMachine : MonoBehaviour
{
    [Header("ChargeMachine")]
    public Animator ChargMachine;
    public bool isBattery = false;

    [Header("Sound Effect")]
    public AudioSource gateOpenSound;   // 문이 열릴 때 재생할 오디오 소스


    // Start is called before the first frame update
    void Start()
    {
        ChargMachine = GetComponent<Animator>();
        if (ChargMachine == null)
        {
            Debug.LogError("Animator 컴포넌트를 찾을 수 없습니다!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
