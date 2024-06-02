using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string targetScene;

    private SaveLoadManager saveLoadManager;

    private void Start()
    {
        saveLoadManager = FindObjectOfType<SaveLoadManager>();
        if (saveLoadManager == null)
        {
            Debug.LogError("SaveLoadManager not found in the scene.");
        }
    }



    public void UsePortal()
    {
        if (saveLoadManager != null)
        {
            // 이동 전에, 플레이어의 체력과 잔탄 수를 저장한다.
            saveLoadManager.SaveHPandBullet();

            // 목표 씬으로 이동
            SceneManager.LoadScene(targetScene);
        }
        else
        {
            Debug.LogError("SaveLoadManager is null. Cannot save the game.");
        }
    }



}
