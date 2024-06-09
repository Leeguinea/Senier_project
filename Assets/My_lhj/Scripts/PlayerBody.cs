using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Scene 관리를 위해 추가

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
            // 플레이어 dead 코드
            playerDead(); // HP가 0 이하일 때 playerDead 메소드 호출
        }
        else
        {
            print("Player Hit");
        }
    }

    private void playerDead()
    {
        // 캐릭터가 죽었을 때 게임 오버 씬으로 전환
        Invoke("LoadGameOverScene", 1f); // 1초 후에 게임 오버 씬으로 전환
    }

    void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOverScene"); // 게임 오버 씬 로드
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("SubjectHand"))
        {
            TakeDamage(1);  //피해 데미지
        }
    }
}
