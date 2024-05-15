using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        }
        else
        {
            print("Player Hit");
            StartCoroutine(BloodyScreenEffect());
        }
    }

    private void playerDead()
    {
        
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
