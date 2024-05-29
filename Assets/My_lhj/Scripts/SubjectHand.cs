using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubjectHand : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter(Collider other)
    {
        // "Player" �±׸� ���� ������Ʈ�� �浹�ߴ��� Ȯ��
        if (other.CompareTag("Player"))
        {
            // �浹�� ������Ʈ���� PlayerHealthBar ������Ʈ�� ã��
            PlayerHealthBar playerHealth = other.gameObject.GetComponent<PlayerHealthBar>();

            // PlayerHealthBar ������Ʈ�� �����ϴ��� Ȯ��
            if (playerHealth != null)
            {
                // TakeDamage �޼ҵ带 ȣ���Ͽ� �÷��̾�� �������� ��
                playerHealth.TakeDamage(damage);
            }
            else
            {
                Debug.LogError("Player ������Ʈ�� PlayerHealthBar ������Ʈ�� �����ϴ�.");
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