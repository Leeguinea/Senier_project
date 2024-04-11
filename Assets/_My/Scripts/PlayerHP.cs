using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어가 데미지를 받는 부분
public class PlayerHP : MonoBehaviour
{
    public int HP = 100;
 
    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;

        if(HP <= 0)
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
        if(other.CompareTag("SubjectHand"))
        {
            TakeDamage(other.gameObject.GetComponent<SubjectHand>().damage);
        }
    }
}
