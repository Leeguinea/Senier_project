using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubjectHand : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter(Collider other)
    {
        // "Player" 태그를 가진 오브젝트와 충돌했는지 확인
        if (other.CompareTag("Player"))
        {
            // 충돌한 오브젝트에서 PlayerHealthBar 컴포넌트를 찾음
            PlayerHealthBar playerHealth = other.gameObject.GetComponent<PlayerHealthBar>();

            // PlayerHealthBar 컴포넌트가 존재하는지 확인
            if (playerHealth != null)
            {
                // TakeDamage 메소드를 호출하여 플레이어에게 데미지를 줌
                playerHealth.TakeDamage(damage);
            }
            else
            {
                Debug.LogError("Player 오브젝트에 PlayerHealthBar 컴포넌트가 없습니다.");
            }
        }
    }
}




/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubjectHand : MonoBehaviour
{
    public int damage;
    public int HP = 10;

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

*/