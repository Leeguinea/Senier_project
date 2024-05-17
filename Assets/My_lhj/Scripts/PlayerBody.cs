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
            StartCoroutine(BloodyScreenEffect());
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


    private IEnumerator BloodyScreenEffect()
    {
        if (bloodyScreen.activeInHierarchy == false)
        {
            bloodyScreen.SetActive(true);
        }

        var image = bloodyScreen.GetComponentInChildren<Image>();

        // Set the initial alpha value to 1 (fully visible).
        Color startColor = image.color;
        startColor.a = 2f;
        image.color = startColor;

        float duration = 3f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Calculate the new alpha value using Lerp.
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);

            // Update the color with the new alpha value.
            Color newColor = image.color;
            newColor.a = alpha;
            image.color = newColor;

            // Increment the elapsed time.
            elapsedTime += Time.deltaTime;

            yield return null; ; // Wait for the next frame.
        }

        if (bloodyScreen.activeInHierarchy)
        {
            bloodyScreen.SetActive(false);
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
