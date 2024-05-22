using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadManager : MonoBehaviour
{
    public Transform playerTransform;   // 플레이어의 위치와 회전

    private int bulletCnt;              // 플레이어의 탄약 수
    public GameManager gameManager;

    private float playerHP;             // 플레이어의 체력 상태
    public GameObject playerObject;
    private PlayerBody playerBody;

    private PlayerData playerData;  // 플레이어 데이터 저장을 위한 변수
    private string saveFilePath;    // 세이브 파일 경로
    private string lastSavedScene;  // 마지막으로 저장된 scene 이름


    private void Awake()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "saveData.json");
    }

    private void Start()
    {
        LoadPlayerHPandBullet(); // 게임 시작 시 게임 불러오기

        // GameManager를 찾아서 참조
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager reference is not set in SaveLoadManager.");
            return;
        }

        // gameManager에서 탄약 수 참조
        bulletCnt = gameManager.CurrentBullet;


        // 플레이어 위치 참조
        if (playerTransform == null)
        {
            Debug.LogError("PlayerObject is not assigned.");
            return;
        }

        // platerBodt에서 플레이어 체력 참조
        playerBody = playerTransform.GetComponent<PlayerBody>();
        if (playerBody != null)
        {
            playerHP = playerBody.HP;
        }
        else
        {
            Debug.LogError("PlayerBody component not found on playerObject.");
        }

        // 현재 씬이 stage1, stage2, stage3인 경우에만 세이브
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


    // 전체 데이터 저장하기 (플레이어 위치, HP, 잔탄수)
    public void SaveData()
    {
        // 플레이어 데이터 설정
        playerData = new PlayerData();
        playerData.SaveData(playerTransform, playerHP, bulletCnt);

        // 현재 씬 저장
        lastSavedScene = SceneManager.GetActiveScene().name;

        // 게임 데이터 생성
        GameData gameData = new GameData();
        gameData.playerData = playerData;
        gameData.lastSavedScene = lastSavedScene;

        // JSON으로 직렬화하여 저장
        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(saveFilePath, json);

        Debug.Log("Game Saved");
    }


    public void SaveHPandBullet()
    {
        // 플레이어 데이터 설정
        playerData = new PlayerData();
        playerData.SaveHPandBullet(playerHP, bulletCnt);

        // 게임 데이터 생성
        GameData gameData = new GameData();
        gameData.playerData = playerData;

        // JSON으로 직렬화하여 저장
        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(saveFilePath, json);

        Debug.Log("Game Saved");
    }


    // 데이터 불러오기 (플레이어, 위치, HP, 탄약수)
    public void LoadPlayerData()
    {
        if (File.Exists(saveFilePath))
        {
            // 세이브 파일이 존재할 경우 데이터 불러오기
            string json = File.ReadAllText(saveFilePath);
            GameData gameData = JsonUtility.FromJson<GameData>(json);

            // 플레이어 데이터 불러오기
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

    // 데이터 불러오기 (플레이어 HP, 탄약 수)
    public void LoadPlayerHPandBullet()
    {
        if (File.Exists(saveFilePath))
        {
            // 세이브 파일이 존재할 경우 데이터 불러오기
            string json = File.ReadAllText(saveFilePath);
            GameData gameData = JsonUtility.FromJson<GameData>(json);

            // 플레이어 데이터 불러오기
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


    //마지막으로 저장된 씬 불러오기
    public void LoadLastSavedScene()
    {
        if (File.Exists(saveFilePath))
        {
            // 세이브 파일이 존재할 경우 데이터 불러오기
            string json = File.ReadAllText(saveFilePath);
            GameData gameData = JsonUtility.FromJson<GameData>(json);


            // 마지막으로 저장된 씬으로 이동
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


    // 게임 세이브 파일 삭제
    public void DeleteSaveFile()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("Save file is deleted");
        }
    }



}
