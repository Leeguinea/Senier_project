using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveLoadManager : MonoBehaviour
{
    [Header("Player Position")]
    public GameObject playerObject;     // �÷��̾��� ��ġ�� ������ ������Ʈ
    private Transform playerTransform;   // �÷��̾��� ��ġ�� ȸ���� ���� ����

    [Header("Bullet Count : CurrentAmmoText")]
    public Text currentAmmoText;
    private int bulletCnt;              // �÷��̾��� ź�� ���� ���� ����

    [Header("Player HP : arms_handgun_01")]
    public PlayerBody playerBody;
    private int playerHP;             // �÷��̾��� ü�¿� ���� ����

    // ���̺� �ε�� ���� ����
    private PlayerData playerData;  // �÷��̾� ������ ������ ���� ����
    private string saveFilePath;    // ���̺� ���� ���
    private string lastSavedScene;  // ���������� ����� scene �̸�
    

    void Awake()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "saveData.json");
        SceneManager.sceneLoaded += OnSceneLoaded; // �� �ε� �̺�Ʈ ����
    }


    void Start()
    {
        LoadPlayerHPandBullet(); // ���� ���� �� ���� �ҷ�����


        // �÷��̾� ��ġ �ʱ�ȭ
        if (playerObject != null) // �÷��̾� ��ġ ������ ��, ����� ����
        {
            playerTransform = playerObject.transform;
            Debug.Log("����Ȯ��", playerTransform);
        }
        else
        {
            Debug.LogError("playerObject is not assigned.");
            return;
        }
        


        // ź�� �� �ʱ�ȭ
        if (currentAmmoText == null) // �÷��̾� ź�� �� ������ ��, ����� ����
        {
            Debug.LogError("currentAmmoText reference is not set in SaveLoadManager.");
            return;
        }
        else
        {
            bulletCnt = int.Parse(currentAmmoText.text); // ����ȯ 
        }


        // �÷��̾� ü�� �ʱ�ȭ
        if (playerBody != null) // �÷��̾� ü�� ������ ��, ����� ����
        {
            playerHP = playerBody.HP; //�÷��̾� ü�� �ʱ�ȭ
        }
        else
        {
            Debug.LogError("PlayerBody is not assigned.");
            return;
        }


        // ���� ���� stage1, stage2, stage3�� ��쿡�� ���̺�
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "Map1" || currentSceneName == "Map2" || currentSceneName == "Map3")
        {
            SaveData();
        }
        else
        {
            Debug.LogWarning("Cannot save in this scene.");
        }
    }


    void Update()
    {
        // �÷��̾��� ��ġ �ǽð� �ݿ�
        if (playerObject != null)
        {
            Transform currentTransform = playerObject.transform;
            if (playerTransform != currentTransform)
            {
                playerTransform = currentTransform;
                Debug.Log("Updated playerTransform : " + playerTransform);
            }
        }

        // �÷��̾��� ü�� �ǽð� �ݿ�
        if (playerBody != null)
        {
            int currentPlayerHP = playerBody.HP;
            if (playerHP != currentPlayerHP)
            {
                playerHP = currentPlayerHP;
                Debug.Log("Updated playerHP : " + playerHP);
            }
        }
        else
        {
            Debug.Log("playerBody�� Null ����");
        }

        // ź�� �� �ǽð� �ݿ�
        if (currentAmmoText != null)
        {
            int currentBulletCnt = int.Parse(currentAmmoText.text);
            if (bulletCnt != currentBulletCnt)
            {
                bulletCnt = currentBulletCnt;
                Debug.Log("Updated bulletCnt : " + bulletCnt);
            }
        }

        if (Input.GetKeyDown(KeyCode.F5)) // 'F'Ű�� ������ ��
        {
            SaveData();
        }
    }

    // ��ü ������ �����ϱ� (�÷��̾� ��ġ, HP, ��ź��)
    public void SaveData()
    {

        if (playerTransform != null)
        {
            playerData = new PlayerData();
            playerData.SaveData(playerTransform, playerHP, bulletCnt);

            lastSavedScene = SceneManager.GetActiveScene().name;

            GameData gameData = new GameData();
            gameData.playerData = playerData;
            gameData.lastSavedScene = lastSavedScene;

            string json = JsonUtility.ToJson(gameData, true);
            File.WriteAllText(saveFilePath, json);

            Debug.Log("Game Saved");
        }
        else
        {
            Debug.LogError("Player Transform is null. Cannot save data.");
        }

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

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadPlayerHPandBullet(); // �� �ε� �� ������ �ҷ�����
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
