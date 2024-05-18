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
                // ���� �� ���� ����
                saveLoadManager.SaveGame();

                // �� �̵� �� ��Ż ������ �����Ͽ� ��ǥ ������ ����� �� �ֵ��� ����
                PlayerPrefs.SetString("LastPortalScene", SceneManager.GetActiveScene().name);
                PlayerPrefs.SetFloat("LastPortalPosX", targetPosition.x);
                PlayerPrefs.SetFloat("LastPortalPosY", targetPosition.y);
                PlayerPrefs.SetFloat("LastPortalPosZ", targetPosition.z);
                PlayerPrefs.SetFloat("LastPortalRotX", targetRotation.x);
                PlayerPrefs.SetFloat("LastPortalRotY", targetRotation.y);
                PlayerPrefs.SetFloat("LastPortalRotZ", targetRotation.z);
                PlayerPrefs.Save();

                // ��ǥ ������ �̵�
                SceneManager.LoadScene(targetScene);
            }
            else
            {
                Debug.LogError("SaveLoadManager is null. Cannot save the game.");
            }
        }
    }



}
