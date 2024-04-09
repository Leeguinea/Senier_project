using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SubjectChaseState : StateMachineBehaviour
{
    NavMeshAgent agent;
    Transform player;

    public float chaseSpeed = 6f;

    public float stopChasingDistance = 21;
    public float attackingDistance = 2.5f; // 올바른 자료형으로 수정

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // -- 초기 -- //

        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();

        agent.speed = chaseSpeed;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(player.position); // 문법 오류 수정
        animator.transform.LookAt(player);

        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);

        // -- 추적 중지 확인 -- //
        if (distanceFromPlayer > stopChasingDistance)
        {
            animator.SetBool("isChasing", false);
        }

        // -- 공격 상태 확인 -- //
        if (distanceFromPlayer < attackingDistance)
        {
            animator.SetBool("isAttacking", true);
        }

    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(animator.transform.position); // 문법 오류 수정
    }
}
