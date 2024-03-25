using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int enemyMaxHP = 5;
    public int enemyCurrentHP = 0;

    private UnityEngine.AI.NavMeshAgent agent;
    private Animator animator;

    private GameObject targetPlayer;
    private float targetDelay = 0.5f;  //update 시간.

    private CapsuleCollider enemyCollider;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
        enemyCollider =  GetComponent<CapsuleCollider>(); 

        targetPlayer = GameObject.FindWithTag("Player");

        InitEnemyHP();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyCurrentHP <= 0)  //적 사망 조건
        {
            StartCoroutine(EnemyDie());
            return;
        }

        if (targetPlayer != null)  //적이 플레이어를 바라보게 만듦
        {
            float maxDelay = 0.5f;
            targetDelay += Time.deltaTime;

            if (targetDelay < maxDelay)  //업데이트 함수 탈출.
            {
                return;
            }

            agent.destination = targetPlayer.transform.position; //적의 목적지는 플레이어
            transform.LookAt(targetPlayer.transform.position); //플레이어를 바라보게 함

            bool isRange = Vector3.Distance(transform.position, targetPlayer.transform.position) <= agent.stoppingDistance;  //플레이어와의 거리 계산

            if (isRange)  //적의 Nav Mesh Agent > Stopping Distance거리에 도달하면 공격
            {
                animator.SetTrigger("Attack");
            }
            else
            {
                //그렇지 않으면 이동
            }
            {
                animator.SetFloat("MoveSpeed", agent.velocity.magnitude);
            }

            targetDelay = 0;
        }
    }

    private void InitEnemyHP()
    {
        enemyCurrentHP = enemyMaxHP;
    }

    IEnumerator EnemyDie()  //코루틴 사용_적이 사망하고 애니메이션 실행까지 대기 후, 소멸 
    {
        agent.speed = 0;
        animator.SetTrigger("Dead");  //적 죽는 애니메이션 재생
        enemyCollider.enabled = false;  //죽으면 콜라이더 비활성화

        yield return new WaitForSeconds(2f);  //사망 후 3 초 대기
        //Destroy(gameObject);  //오브젝트 파괴
        gameObject.SetActive(false);  //콜라이더 컴포넌트 파괴(비활성화)
        InitEnemyHP();  //적이 죽고 체력 초기화 
        agent.speed = 1;
        enemyCollider.enabled = true;  //콜라이더도 다시 활성화 
    }
}
