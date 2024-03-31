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
    private float targetDelay = 0.5f;  //update �ð�.

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
            Debug.Log("�÷��̾�� �� ������ �Ÿ�: " + distance.ToString("F2"));

            if (isRange | distance < 1.0f)  //���� Nav Mesh Agent > Stopping Distance�Ÿ��� �����ϸ� ����
            {
                //KillPlayer();
            }
            else //�׷��� ������ �̵�
            {
                animator.SetFloat("WalkingSpeed", speed);
                StartCoroutine(BossWalking());
            }

            targetDelay = 0;
        }
    }


    void KillPlayer() //�÷��̾� ��� �� ���ӿ���
    {
        // �÷��̾ ����Ű�� �ִϸ��̼�


        // ���ӿ��� �̹��� Ȱ��ȭ 
        gameOverUI.NoticeGameOver("DEFEAT", "�����ϰ� ���Ҵ�.");
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


    IEnumerator EnemyDisableTem()  //�ڷ�ƾ ���_���� ����ȭ�ǰ� �ִϸ��̼� ����
    {
        agent.speed = 0;
        animator.SetTrigger("Spat_in_Face");  //���� �Ӹ��� �°� ��� ����ȭ�Ǵ� �ִϸ��̼�

        yield return new WaitForSeconds(5f);  //5�� ���

   
        agent.speed = 1;
    }


    IEnumerator BossDie()  //�ڷ�ƾ ���_���� ����ϰ� �ִϸ��̼� ������� ��� ��, �Ҹ� 
    {
        agent.speed = 0;
        animator.SetTrigger("Dead");  //�� �״� �ִϸ��̼� ���
        enemyCollider.enabled = false;  //������ �ݶ��̴� ��Ȱ��ȭ

        yield return new WaitForSeconds(2f);  //��� �� 2�� ���

        gameObject.SetActive(false);  //�ݶ��̴� ������Ʈ �ı�(��Ȱ��ȭ)

        InitEnemyHP();  //���� �װ� ü�� �ʱ�ȭ       
        agent.speed = 1;
        enemyCollider.enabled = true;  //�ݶ��̴��� �ٽ� Ȱ��ȭ 
    }


    private void InitEnemyHP() //���� ü�� �ʱ�ȭ
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
