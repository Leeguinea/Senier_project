using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string targetScene;
    public Vector3 targetPosition;
    public Vector3 targetRotation;

    private SaveLoadManager saveLoadManager;

    private void Start()
    {
        saveLoadManager = FindObjectOfType<SaveLoadManager>();
        if (saveLoadManager == null)
        {
            Debug.LogError("SaveLoadManager not found in the scene.");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (saveLoadManager != null)
            {
                // 현재 씬 상태 저장
                saveLoadManager.SaveGame();

                // 씬 이동 후 포탈 정보를 저장하여 목표 씬에서 사용할 수 있도록 설정
                PlayerPrefs.SetString("LastPortalScene", SceneManager.GetActiveScene().name);
                PlayerPrefs.SetFloat("LastPortalPosX", targetPosition.x);
                PlayerPrefs.SetFloat("LastPortalPosY", targetPosition.y);
                PlayerPrefs.SetFloat("LastPortalPosZ", targetPosition.z);
                PlayerPrefs.SetFloat("LastPortalRotX", targetRotation.x);
                PlayerPrefs.SetFloat("LastPortalRotY", targetRotation.y);
                PlayerPrefs.SetFloat("LastPortalRotZ", targetRotation.z);
                PlayerPrefs.Save();

                // 목표 씬으로 이동
                SceneManager.LoadScene(targetScene);
            }
            else
            {
                Debug.LogError("SaveLoadManager is null. Cannot save the game.");
            }
        }
    }



}
