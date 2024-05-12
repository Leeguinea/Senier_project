using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubjectIdieState : StateMachineBehaviour
{
    float timer;
    public float idleTime = 0f; // 유휴 상태가 지속되는 시간

    Transform player;

    public float detectionAreaRadius = 18f;  //탐지 거리(나중에 수정)

    //유휴 상태 시작
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    //유휴 상태
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // -- 순찰 상태 도입 -- //

        timer += Time.deltaTime;
        if (timer > idleTime)
        {
            animator.SetBool("isPatrolling", true);
        }

        // -- 추적 상태 도입 -- //

        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);
        if (distanceFromPlayer < detectionAreaRadius)   //적이 감지하는 영역안이면 추적 상태로 도입 
        {
            animator.SetBool("isChasing", true);
        }
    }
}
