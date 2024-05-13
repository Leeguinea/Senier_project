using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    public int HP = 100;

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;

        if(HP <= 0)
        {
            print("Player Dead");
        }

        else
        {
            print("Player Hit");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("SubjectHand"))
        {
            TakeDamage(1);  //피해 데미지
        }
    }
}
