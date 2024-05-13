using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubjectIdieState : StateMachineBehaviour
{
    float timer;
    public float idleTime = 0f; // ���� ���°� ���ӵǴ� �ð�

    Transform player;

    public float detectionAreaRadius = 18f;  //Ž�� �Ÿ�(���߿� ����)

    //���� ���� ����
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    //���� ����
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // -- ���� ���� ���� -- //

        timer += Time.deltaTime;
        if (timer > idleTime)
        {
            animator.SetBool("isPatrolling", true);
        }

        // -- ���� ���� ���� -- //

        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);
        if (distanceFromPlayer < detectionAreaRadius)   //���� �����ϴ� �������̸� ���� ���·� ���� 
        {
            animator.SetBool("isChasing", true);
        }
    }
}
