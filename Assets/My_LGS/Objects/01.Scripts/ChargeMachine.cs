using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeMachine : MonoBehaviour
{
    [Header("ChargeMachine")]
    public Animator ChargMachine;
    public bool isBattery = false;

    [Header("Sound Effect")]
    public AudioSource gateOpenSound;   // ���� ���� �� ����� ����� �ҽ�


    // Start is called before the first frame update
    void Start()
    {
        ChargMachine = GetComponent<Animator>();
        if (ChargMachine == null)
        {
            Debug.LogError("Animator ������Ʈ�� ã�� �� �����ϴ�!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
