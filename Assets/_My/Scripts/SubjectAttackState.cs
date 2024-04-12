using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubjectAttackState : StateMachineBehaviour
{
    Transform player;
    UnityEngine.AI.NavMeshAgent agent;

    public float stopAttackingDistance = 2.5f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //sound
        if (SoundManager.instance.SubjectChannel.isPlaying == false)
        {
            SoundManager.instance.SubjectChannel.clip = SoundManager.instance.SubjectAttack;
            SoundManager.instance.SubjectChannel.PlayDelayed(1f);
        }


        // LookAtPlayer 메소드 직접 구현
        Vector3 direction = player.position - animator.transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        animator.transform.rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);

        // -- 공격 중지 확인 -- //
        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);

        if (distanceFromPlayer > stopAttackingDistance)
        {
            animator.SetBool("isAttacking", false);
        }
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 direction = player.position - agent.transform.position;
        agent.transform.rotation = Quaternion.LookRotation(direction);

        var yRotation = agent.transform.eulerAngles.y;
        agent.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
