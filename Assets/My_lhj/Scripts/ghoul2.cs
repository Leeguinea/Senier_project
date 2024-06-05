using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ghoul2 : MonoBehaviour
{
    [SerializeField]
    private int enemyMaxHP = 1;
    public int enemyCurrentHP = 0;

    private Animator animator;
    private NavMeshAgent agent;
    private GameObject targetPlayer;
    private CapsuleCollider enemyCollider;
    private bool isAttacking = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        enemyCollider = GetComponent<CapsuleCollider>();

        targetPlayer = GameObject.FindWithTag("Player");

        InitEnemyHP();

        agent.avoidancePriority = 50;
        agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
    }

    void Update()
    {
        if (enemyCurrentHP <= 0)
        {
            if (!animator.GetBool("Dies"))
            {
                StartCoroutine(EnemyDie());
            }
            return;
        }

        if (targetPlayer != null && !isAttacking)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, targetPlayer.transform.position);
            if (distanceToPlayer <= 1f) // ���� ���� ���� ���� ���
            {
                if (!animator.GetBool("Hit"))
                {
                    StartCoroutine(AttackPlayer());
                }
            }
            else if (distanceToPlayer > 2f) // �÷��̾���� �Ÿ��� 2���� �̻��� ��� �߰�
            {
                animator.SetBool("Run", true);
                agent.SetDestination(targetPlayer.transform.position);
            }
            else // �÷��̾���� �Ÿ��� 2���� ������ ��� �߰� ����
            {
                animator.SetBool("Run", false);
                agent.ResetPath(); // ���� ��θ� �ʱ�ȭ�Ͽ� �߰��� ����
            }
        }
    }

    private void InitEnemyHP()
    {
        enemyCurrentHP = enemyMaxHP;
    }

    IEnumerator EnemyDie()
    {
        agent.speed = 0;
        enemyCollider.enabled = false;
        animator.SetBool("Dies", true);
        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }

    private IEnumerator AttackPlayer()
    {
        isAttacking = true;
        agent.isStopped = true;
        animator.SetBool("Run", false);
        animator.SetBool("Hit", true);
        yield return new WaitForSeconds(1f); // ���� �ִϸ��̼��� ����Ǵ� �ð�
        animator.SetBool("Hit", false);
        agent.isStopped = false;
        isAttacking = false;
    }

    private void RotateTowardsPlayer()
    {
        Vector3 targetDirection = (targetPlayer.transform.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2f * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        enemyCurrentHP -= damage;
        if (enemyCurrentHP <= 0)
        {
            enemyCurrentHP = 0;
            if (!animator.GetBool("Dies"))
            {
                StartCoroutine(EnemyDie());
            }
        }
    }
}
