using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Processors;

public class BossEnemy : MonoBehaviour
{
    [SerializeField]
    private int enemyMaxHP = 10000;
    public int enemyCurrentHP = 0;

    private UnityEngine.AI.NavMeshAgent agent;
    private GameObject targetPlayer;
    private float targetDelay = 0.5f;  //update 시간.

    private Animator animator;
    private CapsuleCollider enemyCollider;

    GameOverUI gameOverUI;

    bool isWalkingAnimationPlaying = false;
    public float rotationSpeed = 1.0f;

    void Start()
    {
        gameOverUI = FindObjectOfType<GameOverUI>();

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
        enemyCollider = GetComponent<CapsuleCollider>();

        targetPlayer = GameObject.FindWithTag("Player");

        InitEnemyHP();
    }


    void Update()
    {
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
            RotateTowardsPlayer();

            // 플레이어와의 거리 계산
            float speed = agent.velocity.sqrMagnitude;
            bool isRange = Vector3.Distance(transform.position, targetPlayer.transform.position) <= agent.stoppingDistance;
            float distance = Vector3.Distance(transform.position, targetPlayer.transform.position);
            Debug.Log("플레이어와 적 사이의 거리: " + distance.ToString("F2"));

            if (isRange | distance < 1.0f)  //적의 Nav Mesh Agent > Stopping Distance거리에 도달하면 공격
            {
                //KillPlayer();
            }
            else //그렇지 않으면 이동
            {
                animator.SetFloat("WalkingSpeed", speed);
                StartCoroutine(BossWalking());
            }

            targetDelay = 0;
        }
    }


    void KillPlayer() //플레이어 즉사 및 게임오버
    {
        // 플레이어를 즉사시키는 애니메이션


        // 게임오버 이미지 활성화 
        gameOverUI.NoticeGameOver("DEFEAT", "실패하고 말았다.");
    }


    IEnumerator BossWalking()  //코루틴 사용_적이 걷는 애니메이션 
    {
        if (!isWalkingAnimationPlaying)
        {
            animator.SetBool("IsWalking", true);
            isWalkingAnimationPlaying = true;
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        }

        isWalkingAnimationPlaying = false;
    }


    IEnumerator EnemyDisableTem()  //코루틴 사용_적이 무력화되고 애니메이션 실행
    {
        agent.speed = 0;
        animator.SetTrigger("Spat_in_Face");  //적이 머리를 맞고 잠시 무력화되는 애니메이션

        yield return new WaitForSeconds(5f);  //5초 대기

   
        agent.speed = 1;
    }


    IEnumerator BossDie()  //코루틴 사용_적이 사망하고 애니메이션 실행까지 대기 후, 소멸 
    {
        agent.speed = 0;
        animator.SetTrigger("Dead");  //적 죽는 애니메이션 재생
        enemyCollider.enabled = false;  //죽으면 콜라이더 비활성화

        yield return new WaitForSeconds(2f);  //사망 후 2초 대기

        gameObject.SetActive(false);  //콜라이더 컴포넌트 파괴(비활성화)

        InitEnemyHP();  //적이 죽고 체력 초기화       
        agent.speed = 1;
        enemyCollider.enabled = true;  //콜라이더도 다시 활성화 
    }


    private void InitEnemyHP() //보스 체력 초기화
    {
        enemyCurrentHP = enemyMaxHP;
    }

    void RotateTowardsPlayer()
    {
        Vector3 targetDirection = (targetPlayer.transform.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
