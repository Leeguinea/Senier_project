using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    Rigidbody[] rigidBodies;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidBodies = GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();

        DeactivateRagdoll();
    }

    public void DeactivateRagdoll()  //isKinematic Ȱ��ȭ
    {
        foreach (var rigidBody in rigidBodies)
        {
            rigidBody.isKinematic = true;
        }
        animator.enabled = true;

    }

    public void ActivateRagdoll() //�ִϸ��̼ǿ� ����Ǵ� ��� �ùķ��̼ǵǰ� ����
    {
        foreach (var rigidBody in rigidBodies)
        {
            rigidBody.isKinematic = false;  
        }
        animator.enabled = false;
    }
}
