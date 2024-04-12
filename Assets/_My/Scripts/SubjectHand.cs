using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubjectHand : MonoBehaviour
{
    public int damage;
    public int HP = 100;

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;

        if (HP <= 0)
        {
            Debug.Log("player dead");
        }

        else
        {
            Debug.Log("player hit"); ;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SubjectHand"))
        {
            TakeDamage(other.gameObject.GetComponent<SubjectHand>().damage);
        }
    }
}

