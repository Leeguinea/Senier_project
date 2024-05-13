using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Processors;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.EventSystems.EventTrigger;

public class BossEnemy : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent agent;
    private GameObject targetPlayer;
    private float targetDelay = 0.5f;  //update 시간.

    private Animator animator;
    private CapsuleCollider enemyCollider;

    public GameObject headObject; //머리 오브젝트 변수
    public Collider headCollider; //머리 콜라이더
    public int headShotCnt = 0;   //보스 몹의 헤드샷 횟수

    GameOverUI gameOverUI;

    bool isWalkingAnimationPlaying = false;
    public float rotationSpeed = 1.0f;

    //보스의 일정 범위의 지속 딜에 관한 변수
    public float damagePerSecond = 5;     // 초당 데미지
    public float damageDuration = 10f;   // 도트 데미지 지속 시간
    public float damageInterval = 1f;   // 데미지 주기
    private float nextDamageTime;

    Enemy enemyComponent; 

    [SerializeField]
    private int bossMaxHP = 10000;
    public int bossCurrentHP = 10000;


    void Start()
    {
        gameOverUI = FindObjectOfType<GameOverUI>();

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
        enemyCollider = GetComponent<CapsuleCollider>();
        headCollider = headObject.GetComponent<CapsuleCollider>();

        targetPlayer = GameObject.FindWithTag("Player");
        enemyComponent = GetComponent<Enemy>();

        nextDamageTime = Time.time;

        InitEnemyHP();
    }


    void Update()
    {
        //Debug.Log("보스의 hp : " + bossCurrentHP);
        //Debug.Log("BossEnemy 보스의 헤드샷 : " + headShotCnt);
        //agent.speed = 0;

        if (Time.time >= nextDamageTime)
        {
            DealDamageToEntitiesInRange();
            nextDamageTime = Time.time + damageInterval; // 다음 데미지 주기 설정
        }

        if (headShotCnt >= 2) //헤드샷의 카운트가 5번을 넘어가면 무력화
        {         
            StartCoroutine(HeadShotBoss());
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
            RotateTowardsPlayer();

            // 플레이어와의 거리 계산
            float speed = agent.velocity.sqrMagnitude;
            bool isRange = Vector3.Distance(transform.position, targetPlayer.transform.position) <= agent.stoppingDistance;
            float distance = Vector3.Distance(transform.position, targetPlayer.transform.position);
            //Debug.Log("플레이어와 적 사이의 거리: " + distance.ToString("F2"));

            //적의 Nav Mesh Agent > Stopping Distance거리에 도달하면 공격
            if (isRange | distance < 1.0f)  
            {
                //BossKillPlayer();
            }
            else //그렇지 않으면 이동
            {
                animator.SetFloat("WalkingSpeed", speed);
                StartCoroutine(BossWalking());
            }

            targetDelay = 0;
        }

    }


    void BossKillPlayer() //플레이어 즉사 및 게임오버
    {
        // 플레이어를 즉사시키는 애니메이션


        // 게임오버 이미지 활성화 
        //gameOverUI.NoticeGameOver("DEFEAT", "실패하고 말았다.");
    }

    void DealDamageToEntitiesInRange()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, transform.localScale.x * 5);

        foreach (Collider collider in colliders)
        {
            // 범위 안에 있는 플레이어 및 적에게 데미지를 줌
            // 도트 데미지 지속 시간 동안 데미지를 주도록 함
            if (collider.CompareTag("Enemy"))
            {
                collider.gameObject.GetComponent<Enemy>().enemyCurrentHP -= ((int)(damagePerSecond * damageInterval));
                StartCoroutine(ApplyDamageOverTime(collider.gameObject));
            }

            if (collider.CompareTag("Player"))
            {
                collider.gameObject.GetComponent<PlayerHealthBar>().TakeDamage(damagePerSecond * damageInterval);
                StartCoroutine(ApplyDamageOverTime(collider.gameObject));
            }
        }
    }

    IEnumerator ApplyDamageOverTime(GameObject entity)
    {
        float endTime = Time.time + damageDuration;
        while (Time.time < endTime)
        {
            // 범위 안에 있는 플레이어 및 적에게 지속적으로 데미지를 줌
            if (LayerMask.LayerToName(entity.layer) == "Enemy")
            {
                entity.GetComponent<Enemy>().enemyCurrentHP -= ((int)(damagePerSecond * damageInterval));
            }
            if (LayerMask.LayerToName(entity.layer) == "Player")
            {
                entity.GetComponent<PlayerHealthBar>().TakeDamage(damagePerSecond * damageInterval);
            }
            yield return null;
        }

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


    IEnumerator HeadShotBoss()  //코루틴 사용_적이 무력화되고 애니메이션 실행
    {
        headShotCnt = 0; //헤드샷 횟수 초기화 
        headCollider.enabled = false;
        targetPlayer = null;
        agent.isStopped = true;
        animator.SetBool("IsWalking", false); //걷기 애니메이션 중지    
        animator.SetTrigger("Spat_in_Face");  //적이 머리를 맞고 잠시 무력화되는 애니메이션        

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        headCollider.enabled = true;
        targetPlayer = GameObject.FindWithTag("Player");
        agent.isStopped = false;            
        animator.SetBool("IsWalking", true); //걷기 애니메이션 활성화
    }


    IEnumerator BossDie()  //코루틴 사용_적이 사망하고 애니메이션 실행까지 대기 후, 소멸 
    {
        agent.isStopped = true;
        animator.SetTrigger("Dead");  //적 죽는 애니메이션 재생
        enemyCollider.enabled = false;  //죽으면 콜라이더 비활성화

        yield return new WaitForSeconds(2f);  //사망 후 2초 대기

        gameObject.SetActive(false);  //콜라이더 컴포넌트 파괴(비활성화)

        InitEnemyHP();  //죽고 체력 초기화       
        agent.isStopped = false;
        enemyCollider.enabled = true;  //콜라이더도 다시 활성화 
    }


    private void InitEnemyHP() //보스 체력 초기화
    {
        bossCurrentHP = bossMaxHP;
    }

    void RotateTowardsPlayer()
    {
        Vector3 targetDirection = (targetPlayer.transform.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }


    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Debug.Log("피격");
            enemyCurrentHP -= 1;   //맞으면 HP -1

            if (other.gameObject.CompareTag("BossHead"))
            {
                Debug.Log("헤드샷");
                headShotCnt += 1;
            }
        }
    }
    */



}
