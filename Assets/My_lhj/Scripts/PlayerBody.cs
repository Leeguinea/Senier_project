using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Scene ������ ���� �߰�

public class PlayerBody : MonoBehaviour
{
    public PlayerHealthBar playerHealthBar;
    public int HP = 100;
    public GameObject bloodyScreen;

    private void Start()
    {
        playerHealthBar = GetComponent<PlayerHealthBar>();
    }

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;
        if (playerHealthBar != null)
        {
            playerHealthBar.TakeDamage(damageAmount);
        }

        if (HP <= 0)
        {
            print("Player Dead");
            // �÷��̾� dead �ڵ�
            playerDead(); // HP�� 0 ������ �� playerDead �޼ҵ� ȣ��
        }
        else
        {
            print("Player Hit");
        }
    }

    private void playerDead()
    {
        // ĳ���Ͱ� �׾��� �� ���� ���� ������ ��ȯ
        Invoke("LoadGameOverScene", 1f); // 1�� �Ŀ� ���� ���� ������ ��ȯ
    }

    void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOverScene"); // ���� ���� �� �ε�
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("SubjectHand"))
        {
            TakeDamage(1);  //���� ������
        }
    }
}
