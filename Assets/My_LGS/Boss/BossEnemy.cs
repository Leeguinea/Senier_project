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

    [Header("Boss Object and Animation")]
    public CapsuleCollider bodyCollider;    //몸 콜라이더
    public GameObject headObject;           //머리 오브젝트 변수
    public Collider headCollider;           //머리 콜라이더
    public int headShotCnt = 0;             //보스 몹의 헤드샷 횟수
    private Animator animator;

    //[Header("Boss Move")]
    //public float rotationSpeed = 1.0f;
    private bool isBossDyingAnimaion = false;
    private bool isWalkingAnimationPlaying = false;

    [Header("Boss Fire")] //보스의 일정 범위의 지속 딜에 관한 변수
    public GameObject effectObject; //이펙트
    public float damagePerSecond = 1;   // 초당 데미지
    public float damageDuration = 2f;  // 도트 데미지 지속 시간
    public float damageInterval = 3f;   // 데미지 주기
    public float damageRange = 5.0f;
    private float nextDamageTime;

    [Header("Boss HP")]
    public int bossCurrentHP = 10000;
    private int bossMaxHP = 10000;



    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider>();
        headCollider = headObject.GetComponent<CapsuleCollider>();

        targetPlayer = GameObject.FindWithTag("Player");

        nextDamageTime = Time.time;

        InitEnemyHP();
        StartCoroutine(DealDamagePeriodically());
    }


    void Update()
    {
        /*
        if (Time.time >= nextDamageTime)
        {
            DealDamageToEntitiesInRange();
            nextDamageTime = Time.time + damageInterval; // 다음 데미지 주기 설정
        }
        */
        
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

            // 플레이어와의 거리 계산
            float speed = agent.velocity.sqrMagnitude;
            bool isRange = Vector3.Distance(transform.position, targetPlayer.transform.position) <= agent.stoppingDistance;
            float distance = Vector3.Distance(transform.position, targetPlayer.transform.position);

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


    }
    IEnumerator DealDamagePeriodically()
    {
        while (true)
        {
            DealDamageToEntitiesInRange();
            yield return new WaitForSeconds(damageInterval);
        }
    }


    void DealDamageToEntitiesInRange()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, transform.localScale.x * damageRange);

        foreach (Collider collider in colliders)
        {
            // 범위 안에 있는 플레이어 및 적에게 데미지를 줌
            // 도트 데미지 지속 시간 동안 데미지를 주도록 함
            if (collider.CompareTag("Enemy"))
            {
                collider.gameObject.GetComponent<Subject>().TakeDamage((int)(damagePerSecond * damageInterval));
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
        animator.SetTrigger("HeadShotTriger");  //적이 머리를 맞고 잠시 무력화되는 애니메이션        

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        headCollider.enabled = true;
        targetPlayer = GameObject.FindWithTag("Player");
        agent.isStopped = false;            
        animator.SetBool("IsWalking", true); //걷기 애니메이션 활성화
    }


    void BossDie()
    {
        isBossDyingAnimaion = false;
        Debug.Log("보스 사망 함수" + isBossDyingAnimaion);

        StartCoroutine(BossDying());
        InitEnemyHP(); //체력 초기화
    }

    IEnumerator BossDying()  //코루틴 사용_적이 사망하고 애니메이션 실행까지 대기 후, 소멸 
    {

        bodyCollider.enabled = false;           //콜라이더 비활성화
        headCollider.enabled = false;           //헤드 콜라이더 비활성
        targetPlayer = null;                    //타켓 비활성
        agent.isStopped = true;                 //이동중지
        animator.SetBool("IsWalking", false);   //걷기 애니메이션 중지

        effectObject.SetActive(false);

        animator.SetBool("isDyingBack", true);  //죽는 애니메이션 재생
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); //애니메이션 재생을 지연
        animator.SetBool("isDyingBack", false);

        animator.SetBool("isDying", true);      //누워있는 애니메이션 재생
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); //애니메이션 재생을 지연
        animator.SetBool("isDying", false);

        effectObject.SetActive(true);

        animator.SetBool("isGettingUp", true);  //일어나는 애니메이션 재생
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); //애니메이션 재생을 지연
        animator.SetBool("isGettingUp", false);

        InitEnemyHP(); //체력 초기화

        bodyCollider.enabled = true;                     //콜라이더도 활성화
        headCollider.enabled = true;                     //헤드 콜라이더 활성화
        targetPlayer = GameObject.FindWithTag("Player"); //타겟 플레이어
        agent.isStopped = false;                         //이동시작
        animator.SetBool("IsWalking", true);             //걷기 애니메이션 재생
    }


    private void InitEnemyHP() //보스 체력 초기화
    {
        bossCurrentHP = bossMaxHP;
    }


    public void TakeDamage(int damage)
    {
        bossCurrentHP = bossCurrentHP - damage;

        Debug.Log("피격");
        Debug.Log("헤드샷 카운트 : ");
        Debug.Log("보스의 hp : " + bossCurrentHP);

        if (bossCurrentHP <= 0)
        {
            if (isBossDyingAnimaion == true)  //보스의 HP가 0이하로 떨어질 시
            {
                BossDie();
            }
        }

        if (headShotCnt >= 5) //헤드샷의 카운트가 5번을 넘어가면 무력화
        {
            StartCoroutine(HeadShotBoss());
        }
    }
    private void OnDrawGizmos()
    {
        // Gizmo로 데미지 범위를 시각화
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRange);
    }

}
