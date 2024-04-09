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

    public void DeactivateRagdoll()  //isKinematic 활성화
    {
        foreach (var rigidBody in rigidBodies)
        {
            rigidBody.isKinematic = true;
        }
        animator.enabled = true;

    }

    public void ActivateRagdoll() //애니메이션에 제어되는 대신 시뮬레이션되고 있음
    {
        foreach (var rigidBody in rigidBodies)
        {
            rigidBody.isKinematic = false;  
        }
        animator.enabled = false;
    }
}
