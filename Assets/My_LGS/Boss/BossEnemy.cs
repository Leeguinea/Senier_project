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

    private Animator animator;
    private CapsuleCollider enemyCollider;

    public GameObject headObject; //�Ӹ� ������Ʈ ����
    public Collider headCollider; //�Ӹ� �ݶ��̴�
    public int headShotCnt = 0;   //���� ���� ��弦 Ƚ��

    GameOverUI gameOverUI;

    bool isWalkingAnimationPlaying = false;
    public float rotationSpeed = 1.0f;

    //������ ���� ������ ���� ���� ���� ����
    public float damagePerSecond = 5;     // �ʴ� ������
    public float damageDuration = 10f;   // ��Ʈ ������ ���� �ð�
    public float damageInterval = 1f;   // ������ �ֱ�
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
        //Debug.Log("������ hp : " + bossCurrentHP);
        //Debug.Log("BossEnemy ������ ��弦 : " + headShotCnt);
        //agent.speed = 0;

        if (Time.time >= nextDamageTime)
        {
            DealDamageToEntitiesInRange();
            nextDamageTime = Time.time + damageInterval; // ���� ������ �ֱ� ����
        }

        if (headShotCnt >= 2) //��弦�� ī��Ʈ�� 5���� �Ѿ�� ����ȭ
        {         
            StartCoroutine(HeadShotBoss());
        }

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
            RotateTowardsPlayer();

            // �÷��̾���� �Ÿ� ���
            float speed = agent.velocity.sqrMagnitude;
            bool isRange = Vector3.Distance(transform.position, targetPlayer.transform.position) <= agent.stoppingDistance;
            float distance = Vector3.Distance(transform.position, targetPlayer.transform.position);
            //Debug.Log("�÷��̾�� �� ������ �Ÿ�: " + distance.ToString("F2"));

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


        // ���ӿ��� �̹��� Ȱ��ȭ 
        //gameOverUI.NoticeGameOver("DEFEAT", "�����ϰ� ���Ҵ�.");
    }

    void DealDamageToEntitiesInRange()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, transform.localScale.x * 5);

        foreach (Collider collider in colliders)
        {
            // ���� �ȿ� �ִ� �÷��̾� �� ������ �������� ��
            // ��Ʈ ������ ���� �ð� ���� �������� �ֵ��� ��
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
        animator.SetTrigger("Spat_in_Face");  //���� �Ӹ��� �°� ��� ����ȭ�Ǵ� �ִϸ��̼�        

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        headCollider.enabled = true;
        targetPlayer = GameObject.FindWithTag("Player");
        agent.isStopped = false;            
        animator.SetBool("IsWalking", true); //�ȱ� �ִϸ��̼� Ȱ��ȭ
    }


    IEnumerator BossDie()  //�ڷ�ƾ ���_���� ����ϰ� �ִϸ��̼� ������� ��� ��, �Ҹ� 
    {
        agent.isStopped = true;
        animator.SetTrigger("Dead");  //�� �״� �ִϸ��̼� ���
        enemyCollider.enabled = false;  //������ �ݶ��̴� ��Ȱ��ȭ

        yield return new WaitForSeconds(2f);  //��� �� 2�� ���

        gameObject.SetActive(false);  //�ݶ��̴� ������Ʈ �ı�(��Ȱ��ȭ)

        InitEnemyHP();  //�װ� ü�� �ʱ�ȭ       
        agent.isStopped = false;
        enemyCollider.enabled = true;  //�ݶ��̴��� �ٽ� Ȱ��ȭ 
    }


    private void InitEnemyHP() //���� ü�� �ʱ�ȭ
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
            Debug.Log("�ǰ�");
            enemyCurrentHP -= 1;   //������ HP -1

            if (other.gameObject.CompareTag("BossHead"))
            {
                Debug.Log("��弦");
                headShotCnt += 1;
            }
        }
    }
    */



}
