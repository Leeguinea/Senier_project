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
            // �̵� ����, �÷��̾��� ü�°� ��ź ���� �����Ѵ�.
            saveLoadManager.SaveHPandBullet();

            // ��ǥ ������ �̵�
            SceneManager.LoadScene(targetScene);
        }
        else
        {
            Debug.LogError("SaveLoadManager is null. Cannot save the game.");
        }
    }



}
