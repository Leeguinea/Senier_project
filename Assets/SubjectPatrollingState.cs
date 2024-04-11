using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SubjectPatrollingState : StateMachineBehaviour
{
    float timer;
    public float patrollingTime = 10f;

    Transform player;
    NavMeshAgent agent;

    public float detectionArea = 18f;
    public float patrolSpeed = 2f; //추격전 속도

    List<Transform> waypointList = new List<Transform>();

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // -- 초기 -- //
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();

        agent.speed = patrolSpeed;
        timer = 0;
        // -- First Waypoint로 복귀 -- //

        GameObject waypointCluster = GameObject.FindGameObjectWithTag("Waypoints");
        foreach (Transform t in waypointCluster.transform)
        {
            waypointList.Add(t);
        }

        Vector3 nextPosition = waypointList[Random.Range(0, waypointList.Count)].position;
        agent.SetDestination(nextPosition);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // -- 웨이포잉ㄴ트 도착, 다음 웨이포인트로 이동 -- //
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(waypointList[Random.Range(0, waypointList.Count)].position);
        }

        // -- 유휴 상태로 돌아감 -- //
        timer += Time.deltaTime;
        if (timer > patrollingTime)
        {
            animator.SetBool("isPatrolling", false);
        }

        // -- 추적 상태 도입 -- //
        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);
        if (distanceFromPlayer < detectionArea) //적이 감지하는 영역이면 추적
        {
            animator.SetBool("isChasing", true);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
    }
}



/*
public class SubjectPatrollingState : StateMachineBehaviour
{
    float timer;
    public float patrollingTime = 10f;

    Transform player;
    NavMeshAgent agent;

    public float detectionArea = 18f;
    public float patrolSpeed = 2f;  //추격전_속도

    List<Transform> waypointList = new List<Transform>();

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // -- 초기 -- //

        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();

        agent.speed = patrolSpeed;
        timer = 0;

        // -- First Waypoint로 복귀 -- //

        GameObject waypointCluster = Gameobject.FindGameObjectWithTag("Waypoints");
        foreach (Transform t in waypointCluster.transform)
        {
            waypointList.Add(t);
        }

        Vector3 nextPosition = waypointList[Random.Range(0, waypointList.Count)].position;
        agent.SetDestination(nextPosition);
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // -- 웨이포인트 도착, 다음 웨이포인트로 이동 -- //
        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(waypointList[Random.Range(0, waypointList.Count)].postion);
        }

        // -- 유휴 상태로 돌아감 -- //

        timer += timer.deltaTime;
        if(timer > patrollingTime)
        {
            animator.SetBool("isPatrolling", false);
        }

        // -- 추적 상태 도입 -- //

        float distanceFromPlayer = Vector3 Distance(player position, animator transform position);
        if (distanceFromPlayer < detectionArea)   //적이 감지하는 영역안이면 추적 상태로 도입 
        {
            animator.SetBool("isChasing", true);
        }

    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // -- stop the agent -- //
        agent.SetDestination(agent.transform.position);
    }
}
*/