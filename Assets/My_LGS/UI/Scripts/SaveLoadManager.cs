using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadManager : MonoBehaviour
{
    public Transform playerTransform;   // �÷��̾��� ��ġ�� ȸ��

    private int bulletCnt;              // �÷��̾��� ź�� ��
    public GameManager gameManager;

    private float playerHP;             // �÷��̾��� ü�� ����
    public GameObject playerObject;
    private PlayerBody playerBody;

    private PlayerData playerData;  // �÷��̾� ������ ������ ���� ����
    private string saveFilePath;    // ���̺� ���� ���
    private string lastSavedScene;  // ���������� ����� scene �̸�


    private void Awake()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "saveData.json");
    }

    private void Start()
    {
        LoadPlayerHPandBullet(); // ���� ���� �� ���� �ҷ�����

        // GameManager�� ã�Ƽ� ����
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager reference is not set in SaveLoadManager.");
            return;
        }

        // gameManager���� ź�� �� ����
        bulletCnt = gameManager.CurrentBullet;


        // �÷��̾� ��ġ ����
        if (playerTransform == null)
        {
            Debug.LogError("PlayerObject is not assigned.");
            return;
        }

        // platerBodt���� �÷��̾� ü�� ����
        playerBody = playerTransform.GetComponent<PlayerBody>();
        if (playerBody != null)
        {
            playerHP = playerBody.HP;
        }
        else
        {
            Debug.LogError("PlayerBody component not found on playerObject.");
        }

        // ���� ���� stage1, stage2, stage3�� ��쿡�� ���̺�
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "Stage1" || currentSceneName == "Stage2" || currentSceneName == "Stage3")
        {
            SaveData();
        }
        else
        {
            Debug.LogWarning("Cannot save in this scene.");
        }
    }


    // ��ü ������ �����ϱ� (�÷��̾� ��ġ, HP, ��ź��)
    public void SaveData()
    {
        // �÷��̾� ������ ����
        playerData = new PlayerData();
        playerData.SaveData(playerTransform, playerHP, bulletCnt);

        // ���� �� ����
        lastSavedScene = SceneManager.GetActiveScene().name;

        // ���� ������ ����
        GameData gameData = new GameData();
        gameData.playerData = playerData;
        gameData.lastSavedScene = lastSavedScene;

        // JSON���� ����ȭ�Ͽ� ����
        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(saveFilePath, json);

        Debug.Log("Game Saved");
    }


    public void SaveHPandBullet()
    {
        // �÷��̾� ������ ����
        playerData = new PlayerData();
        playerData.SaveHPandBullet(playerHP, bulletCnt);

        // ���� ������ ����
        GameData gameData = new GameData();
        gameData.playerData = playerData;

        // JSON���� ����ȭ�Ͽ� ����
        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(saveFilePath, json);

        Debug.Log("Game Saved");
    }


    // ������ �ҷ����� (�÷��̾�, ��ġ, HP, ź���)
    public void LoadPlayerData()
    {
        if (File.Exists(saveFilePath))
        {
            // ���̺� ������ ������ ��� ������ �ҷ�����
            string json = File.ReadAllText(saveFilePath);
            GameData gameData = JsonUtility.FromJson<GameData>(json);

            // �÷��̾� ������ �ҷ�����
            if (gameData.playerData != null)
            {
                gameData.playerData.LoadData(playerTransform, ref playerHP, ref bulletCnt);
            }
        }
        else
        {
            Debug.LogWarning("Save file not found");
        }
    }

    // ������ �ҷ����� (�÷��̾� HP, ź�� ��)
    public void LoadPlayerHPandBullet()
    {
        if (File.Exists(saveFilePath))
        {
            // ���̺� ������ ������ ��� ������ �ҷ�����
            string json = File.ReadAllText(saveFilePath);
            GameData gameData = JsonUtility.FromJson<GameData>(json);

            // �÷��̾� ������ �ҷ�����
            if (gameData.playerData != null)
            {
                gameData.playerData.LoadHPandBullet(ref playerHP, ref bulletCnt);
            }
        }
        else
        {
            Debug.LogWarning("Save file not found");
        }
    }


    //���������� ����� �� �ҷ�����
    public void LoadLastSavedScene()
    {
        if (File.Exists(saveFilePath))
        {
            // ���̺� ������ ������ ��� ������ �ҷ�����
            string json = File.ReadAllText(saveFilePath);
            GameData gameData = JsonUtility.FromJson<GameData>(json);


            // ���������� ����� ������ �̵�
            if (!string.IsNullOrEmpty(gameData.lastSavedScene))
            {
                SceneManager.LoadScene(gameData.lastSavedScene);
            }
        }
        else
        {
            Debug.LogWarning("Save file not found");
        }
    }


    // ���� ���̺� ���� ����
    public void DeleteSaveFile()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("Save file is deleted");
        }
    }



}
