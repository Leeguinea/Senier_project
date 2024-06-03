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
    private float targetDelay = 0.5f;  //update �ð�.
    public int damage = 1;  //�÷��̾�� �ִ� damage

    [Header("Boss Object and Animation")]
    public CapsuleCollider bodyCollider;    //�� �ݶ��̴�
    public GameObject headObject;           //�Ӹ� ������Ʈ ����
    public Collider headCollider;           //�Ӹ� �ݶ��̴�
    public int headShotCnt = 0;             //���� ���� ��弦 Ƚ��
    private Animator animator;

    //[Header("Boss Move")]
    //public float rotationSpeed = 1.0f;
    private bool isBossDyingAnimaion = false;
    private bool isWalkingAnimationPlaying = false;

    [Header("Boss Fire")] //������ ���� ������ ���� ���� ���� ����
    public GameObject effectObject; //����Ʈ
    public float damagePerSecond = 1;   // �ʴ� ������
    public float damageDuration = 2f;  // ��Ʈ ������ ���� �ð�
    public float damageInterval = 3f;   // ������ �ֱ�
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
            nextDamageTime = Time.time + damageInterval; // ���� ������ �ֱ� ����
        }
        */
        
        if (targetPlayer != null)  //���� �÷��̾ �ٶ󺸰� ����
        {
            float maxDelay = 0.5f;
            targetDelay += Time.deltaTime;

            if (targetDelay < maxDelay)  //������Ʈ �Լ� Ż��.
            {
                return;
            }

            agent.destination = targetPlayer.transform.position; //���� �������� �÷��̾�
            transform.LookAt(targetPlayer.transform.position); //�÷��̾ �ٶ󺸰� ��

            // �÷��̾���� �Ÿ� ���
            float speed = agent.velocity.sqrMagnitude;
            bool isRange = Vector3.Distance(transform.position, targetPlayer.transform.position) <= agent.stoppingDistance;
            float distance = Vector3.Distance(transform.position, targetPlayer.transform.position);

            //���� Nav Mesh Agent > Stopping Distance�Ÿ��� �����ϸ� ����
            if (isRange | distance < 1.0f)  
            {
                //BossKillPlayer();
            }
            else //�׷��� ������ �̵�
            {
                animator.SetFloat("WalkingSpeed", speed);
                StartCoroutine(BossWalking());
            }

            targetDelay = 0;
        }

    }


    void BossKillPlayer() //�÷��̾� ��� �� ���ӿ���
    {
        // �÷��̾ ����Ű�� �ִϸ��̼�


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
            // ���� �ȿ� �ִ� �÷��̾� �� ������ �������� ��
            // ��Ʈ ������ ���� �ð� ���� �������� �ֵ��� ��
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
            // ���� �ȿ� �ִ� �÷��̾� �� ������ ���������� �������� ��
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


    IEnumerator BossWalking()  //�ڷ�ƾ ���_���� �ȴ� �ִϸ��̼� 
    {
        if (!isWalkingAnimationPlaying)
        {
            animator.SetBool("IsWalking", true);
            isWalkingAnimationPlaying = true;
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        }

        isWalkingAnimationPlaying = false;
    }


    IEnumerator HeadShotBoss()  //�ڷ�ƾ ���_���� ����ȭ�ǰ� �ִϸ��̼� ����
    {
        headShotCnt = 0; //��弦 Ƚ�� �ʱ�ȭ 
        headCollider.enabled = false;
        targetPlayer = null;
        agent.isStopped = true;
        animator.SetBool("IsWalking", false); //�ȱ� �ִϸ��̼� ����    
        animator.SetTrigger("HeadShotTriger");  //���� �Ӹ��� �°� ��� ����ȭ�Ǵ� �ִϸ��̼�        

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        headCollider.enabled = true;
        targetPlayer = GameObject.FindWithTag("Player");
        agent.isStopped = false;            
        animator.SetBool("IsWalking", true); //�ȱ� �ִϸ��̼� Ȱ��ȭ
    }


    void BossDie()
    {
        isBossDyingAnimaion = false;
        Debug.Log("���� ��� �Լ�" + isBossDyingAnimaion);

        StartCoroutine(BossDying());
        InitEnemyHP(); //ü�� �ʱ�ȭ
    }

    IEnumerator BossDying()  //�ڷ�ƾ ���_���� ����ϰ� �ִϸ��̼� ������� ��� ��, �Ҹ� 
    {

        bodyCollider.enabled = false;           //�ݶ��̴� ��Ȱ��ȭ
        headCollider.enabled = false;           //��� �ݶ��̴� ��Ȱ��
        targetPlayer = null;                    //Ÿ�� ��Ȱ��
        agent.isStopped = true;                 //�̵�����
        animator.SetBool("IsWalking", false);   //�ȱ� �ִϸ��̼� ����

        effectObject.SetActive(false);

        animator.SetBool("isDyingBack", true);  //�״� �ִϸ��̼� ���
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); //�ִϸ��̼� ����� ����
        animator.SetBool("isDyingBack", false);

        animator.SetBool("isDying", true);      //�����ִ� �ִϸ��̼� ���
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); //�ִϸ��̼� ����� ����
        animator.SetBool("isDying", false);

        effectObject.SetActive(true);

        animator.SetBool("isGettingUp", true);  //�Ͼ�� �ִϸ��̼� ���
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); //�ִϸ��̼� ����� ����
        animator.SetBool("isGettingUp", false);

        InitEnemyHP(); //ü�� �ʱ�ȭ

        bodyCollider.enabled = true;                     //�ݶ��̴��� Ȱ��ȭ
        headCollider.enabled = true;                     //��� �ݶ��̴� Ȱ��ȭ
        targetPlayer = GameObject.FindWithTag("Player"); //Ÿ�� �÷��̾�
        agent.isStopped = false;                         //�̵�����
        animator.SetBool("IsWalking", true);             //�ȱ� �ִϸ��̼� ���
    }


    private void InitEnemyHP() //���� ü�� �ʱ�ȭ
    {
        bossCurrentHP = bossMaxHP;
    }


    public void TakeDamage(int damage)
    {
        bossCurrentHP = bossCurrentHP - damage;

        Debug.Log("�ǰ�");
        Debug.Log("��弦 ī��Ʈ : ");
        Debug.Log("������ hp : " + bossCurrentHP);

        if (bossCurrentHP <= 0)
        {
            if (isBossDyingAnimaion == true)  //������ HP�� 0���Ϸ� ������ ��
            {
                BossDie();
            }
        }

        if (headShotCnt >= 5) //��弦�� ī��Ʈ�� 5���� �Ѿ�� ����ȭ
        {
            StartCoroutine(HeadShotBoss());
        }
    }
    private void OnDrawGizmos()
    {
        // Gizmo�� ������ ������ �ð�ȭ
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRange);
    }

}
