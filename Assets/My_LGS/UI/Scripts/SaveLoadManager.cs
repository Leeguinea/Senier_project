using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadManager : MonoBehaviour
{
    public Transform playerTransform;
    public int playerHP;
    public int playerAmmo;
    public GameObject[] enemies;
    public GameObject[] interactableObjects;

    private string saveFilePath;

    private void Awake()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "saveData.json");
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    // 포탈 이동시, 이전 구역 위치 보존
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadGame();

        // 포탈 정보를 통해 플레이어 위치와 회전 복원
        if (PlayerPrefs.HasKey("LastPortalScene"))
        {
            string lastPortalScene = PlayerPrefs.GetString("LastPortalScene");

            if (lastPortalScene == SceneManager.GetActiveScene().name)
            {
                float posX = PlayerPrefs.GetFloat("LastPortalPosX");
                float posY = PlayerPrefs.GetFloat("LastPortalPosY");
                float posZ = PlayerPrefs.GetFloat("LastPortalPosZ");
                float rotX = PlayerPrefs.GetFloat("LastPortalRotX");
                float rotY = PlayerPrefs.GetFloat("LastPortalRotY");
                float rotZ = PlayerPrefs.GetFloat("LastPortalRotZ");

                playerTransform.position = new Vector3(posX, posY, posZ);
                playerTransform.rotation = Quaternion.Euler(rotX, rotY, rotZ);

                // 포탈 정보를 삭제하여 반복 로드를 방지
                PlayerPrefs.DeleteKey("LastPortalScene");
                PlayerPrefs.DeleteKey("LastPortalPosX");
                PlayerPrefs.DeleteKey("LastPortalPosY");
                PlayerPrefs.DeleteKey("LastPortalPosZ");
                PlayerPrefs.DeleteKey("LastPortalRotX");
                PlayerPrefs.DeleteKey("LastPortalRotY");
                PlayerPrefs.DeleteKey("LastPortalRotZ");
            }
        }
    }


    // 게임 저장하기
    public void SaveGame()
    {
        GameData gameData = new GameData();

        // Save player data
        gameData.playerData = new PlayerData();
        gameData.playerData.SaveData(playerTransform, playerHP, playerAmmo);

        // Save enemies data
        gameData.enemiesData = EnemyData.SaveData(enemies);

        // Save interactable objects data
        gameData.objectsData = ObjectData.SaveData(interactableObjects);

        // Serialize to JSON and save to file
        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(saveFilePath, json);

        Debug.Log("Game Saved");
    }


    // 게임 불러오기
    public void LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            GameData gameData = JsonUtility.FromJson<GameData>(json);

            // Load player data
            if (gameData.playerData != null)
            {
                gameData.playerData.LoadData(playerTransform, ref playerHP, ref playerAmmo);
            }

            // Load enemies data
            if (gameData.enemiesData != null)
            {
                EnemyData.LoadData(gameData.enemiesData, enemies);
            }

            // Load interactable objects data
            if (gameData.objectsData != null)
            {
                ObjectData.LoadData(gameData.objectsData, interactableObjects);
            }

            Debug.Log("Game Loaded");
        }
        else
        {
            Debug.LogWarning("Save file not found");
        }
    }


    // 게임 세이브 파일 삭제
    public void DeleteSaveFile()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
        }
    }



}
