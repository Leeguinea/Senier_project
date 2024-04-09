using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subject : MonoBehaviour
{
    [SerializeField]
    private int subjectMaxHP = 2;
    public int subjectCurrentHP = 0;
    private Animator animator;

    private UnityEngine.AI.NavMeshAgent navAgent;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        subjectCurrentHP = subjectMaxHP; // �ִ� HP�� ���� HP �ʱ�ȭ
    }


    public void TakeDamage(int damageAmount)
    {
        subjectCurrentHP -= damageAmount; 

        if (subjectCurrentHP <= 0) 
        {
            int randomValue = Random.Range(0, 2);  //0 or 1 �������� ��� ��� ���

            if(randomValue == 0)
            {
                animator.SetTrigger("DIE1");
            }
            else
            {
                animator.SetTrigger("DIE2");
            }
            
        }
        else  // Hit �ִϸ��̼�
        {
            animator.SetTrigger("DAMAGE");
        }
    }


}
