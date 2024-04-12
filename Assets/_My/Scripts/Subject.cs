using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subject : MonoBehaviour
{
    [SerializeField]
    private int subjectMaxHP = 3;
    public int subjectCurrentHP = 0;
    private Animator animator;

    private UnityEngine.AI.NavMeshAgent navAgent;

    public SubjectHand subjectHand;  //공격 매체
    public int damage = 1;  //플레이어에게 주는 damage

    public bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        subjectCurrentHP = subjectMaxHP; // 최대 HP로 현재 HP 초기화

        subjectHand.damage = damage; //플레이어에게 손으로 공격
    }

    public void TakeDamage(int damageAmount)
    {
        subjectCurrentHP -= damageAmount;

        if (subjectCurrentHP <= 0)
        {
            int randomValue = Random.Range(0, 2);  //0 or 1 랜덤으로 사망 모션 재생

            if (randomValue == 0)
            {
                animator.SetTrigger("DIE1");
            }
            else
            {
                animator.SetTrigger("DIE2");
            }

            isDead = true;

            //Dead Sound
            SoundManager.instance.SubjectChannel.PlayOneShot(SoundManager.instance.SubjectDeath);

        }
        else  // Hit 애니메이션
        {
            animator.SetTrigger("DAMAGE");

            //Hurt Sound
            SoundManager.instance.SubjectChannel.PlayOneShot(SoundManager.instance.SubjectHurt);
        }
    }

    //기즈모 -> 범위 측정 하는 것
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2.5f); //Attacking // Stop Attacking

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 14f); //Detection(start Chasing)

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 21f); //Stop Chasing
    }
}
