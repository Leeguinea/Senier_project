using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subject : MonoBehaviour
{
    [SerializeField]
    private int subjectMaxHP = 10;
    public int subjectCurrentHP = 0;
    private Animator animator;

    private UnityEngine.AI.NavMeshAgent navAgent;

    public SubjectHand subjectHand;  //���� ��ü
    public int damage = 1;  //�÷��̾�� �ִ� damage

    private CapsuleCollider SubjectCollider; // �ݶ��̴� ����
    public bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        SubjectCollider = GetComponent<CapsuleCollider>(); // �ݶ��̴� �ʱ�ȭ
        subjectCurrentHP = subjectMaxHP; // �ִ� HP�� ���� HP �ʱ�ȭ

        subjectHand.damage = damage; //�÷��̾�� ������ ����
    }

    public void TakeDamage(int damageAmount)
    {
        subjectCurrentHP -= damageAmount;

        if (subjectCurrentHP <= 0)
        {
            int randomValue = Random.Range(0, 2);  //0 or 1 �������� ��� ��� ���

            if (randomValue == 0)
            {
                animator.SetTrigger("DIE1");
            }
            else
            {
                animator.SetTrigger("DIE2");
            }
            SubjectCollider.enabled = false;  // �ݶ��̴� ��Ȱ��ȭ
            isDead = true;

            //Dead Sound
            SoundManager.instance.SubjectChannel.PlayOneShot(SoundManager.instance.SubjectDeath);
        }
        else  // Hit �ִϸ��̼�
        {
            animator.SetTrigger("DAMAGE");

            //Hurt Sound
            SoundManager.instance.SubjectChannel.PlayOneShot(SoundManager.instance.SubjectHurt);
        }
    }

    //����� -> ���� ���� �ϴ� ��
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2.2f); //Attacking // Stop Attacking

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 5f); //Detection(start Chasing)

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 10f); //Stop Chasing
    }
}
