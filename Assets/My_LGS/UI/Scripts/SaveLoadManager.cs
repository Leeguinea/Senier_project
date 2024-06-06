using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveLoadManager : MonoBehaviour
{
    [Header("Player Position")]
    public GameObject playerObject;     // 플레이어의 위치를 참조할 오브젝트
    private Transform playerTransform;   // 플레이어의 위치와 회전에 대한 변수

    [Header("Bullet Count : CurrentAmmoText")]
    public Text currentAmmoText;
    private int bulletCnt;              // 플레이어의 탄약 수에 대한 변수

    [Header("Player HP : arms_handgun_01")]
    public PlayerBody playerBody;
    private int playerHP;             // 플레이어의 체력에 대한 변수

    // 세이브 로드와 관련 변수
    private PlayerData playerData;  // 플레이어 데이터 저장을 위한 변수
    private string saveFilePath;    // 세이브 파일 경로
    private string lastSavedScene;  // 마지막으로 저장된 scene 이름
    

    void Awake()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "saveData.json");
        SceneManager.sceneLoaded += OnSceneLoaded; // 씬 로드 이벤트 구독
    }


    void Start()
    {
        LoadPlayerHPandBullet(); // 게임 시작 시 게임 불러오기


        // 플레이어 위치 초기화
        if (playerObject != null) // 플레이어 위치 미참조 시, 디버그 에러
        {
            playerTransform = playerObject.transform;
            Debug.Log("참조확인", playerTransform);
        }
        else
        {
            Debug.LogError("playerObject is not assigned.");
            return;
        }
        


        // 탄약 수 초기화
        if (currentAmmoText == null) // 플레이어 탄약 수 미참조 시, 디버그 에러
        {
            Debug.LogError("currentAmmoText reference is not set in SaveLoadManager.");
            return;
        }
        else
        {
            bulletCnt = int.Parse(currentAmmoText.text); // 형변환 
        }


        // 플레이어 체력 초기화
        if (playerBody != null) // 플레이어 체력 미참조 시, 디버그 에러
        {
            playerHP = playerBody.HP; //플레이어 체력 초기화
        }
        else
        {
            Debug.LogError("PlayerBody is not assigned.");
            return;
        }


        // 현재 씬이 stage1, stage2, stage3인 경우에만 세이브
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
        // 플레이어의 위치 실시간 반영
        if (playerObject != null)
        {
            Transform currentTransform = playerObject.transform;
            if (playerTransform != currentTransform)
            {
                playerTransform = currentTransform;
                Debug.Log("Updated playerTransform : " + playerTransform);
            }
        }

        // 플레이어의 체력 실시간 반영
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
            Debug.Log("playerBody가 Null 상태");
        }

        // 탄약 수 실시간 반영
        if (currentAmmoText != null)
        {
            int currentBulletCnt = int.Parse(currentAmmoText.text);
            if (bulletCnt != currentBulletCnt)
            {
                bulletCnt = currentBulletCnt;
                Debug.Log("Updated bulletCnt : " + bulletCnt);
            }
        }

        if (Input.GetKeyDown(KeyCode.F5)) // 'F'키를 눌렀을 때
        {
            SaveData();
        }
    }

    // 전체 데이터 저장하기 (플레이어 위치, HP, 잔탄수)
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

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadPlayerHPandBullet(); // 씬 로드 시 데이터 불러오기
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
