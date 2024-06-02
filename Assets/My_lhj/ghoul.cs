using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ghoul : MonoBehaviour
{
    [SerializeField]
    private int enemyMaxHP = 1;
    public int enemyCurrentHP = 0;

    private UnityEngine.AI.NavMeshAgent agent;
    private Animator animator;

    private GameObject targetPlayer;

    private CapsuleCollider enemyCollider;

    private bool isAttacking = false;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
        enemyCollider = GetComponent<CapsuleCollider>();

        targetPlayer = GameObject.FindWithTag("Player");

        InitEnemyHP();
    }

    void Update()
    {
        if (enemyCurrentHP <= 0)
        {
            StartCoroutine(EnemyDie());
            return;
        }

        if (targetPlayer != null)
        {
            RotateTowardsPlayer();
            if (!isAttacking)
            {
                StartCoroutine(ChasePlayer());
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
        animator.SetTrigger("Die");
        enemyCollider.enabled = false; // 콜라이더 비활성화

        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false); // 게임 오브젝트 비활성화
        InitEnemyHP();
        agent.speed = 1;
        enemyCollider.enabled = true; // 콜라이더 다시 활성화
    }

    private IEnumerator ChasePlayer()
    {
        isAttacking = true;
        animator.SetTrigger("Run");
        agent.SetDestination(targetPlayer.transform.position);

        if (Vector3.Distance(transform.position, targetPlayer.transform.position) <= 2f)
        {
            // 공격
            Debug.Log("Enemy attacked the player!");
        }

        yield return new WaitForSeconds(2f);

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
            animator.SetTrigger("Die");
        }
    }
}
