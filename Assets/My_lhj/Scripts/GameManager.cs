using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    [Header("Enemy Prefabs")]
    public GameObject[] enemyPrefabs;

    [Header("Spawn Points")]
    public Transform[] spawnPoints;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            // Select a random enemy prefab
            int randomIndex = Random.Range(0, enemyPrefabs.Length);
            GameObject enemyPrefab = enemyPrefabs[randomIndex];

            // Select a random spawn point
            int randomSpawnPointIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomSpawnPointIndex];

            // Spawn the enemy
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

            // Wait for the next spawn
            yield return new WaitForSeconds(5f);
        }
    }
}



/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Enemy")]
    [SerializeField]
    private GameObject[] spawnPoint;

    
    void Start()
    {
        Instance = this; //어디서든 접근 가능, i말고 대문자 I로 써야 오류 안생김

        StartCoroutine(EnemySpawn());
    }

    IEnumerator EnemySpawn() // 적 스폰 함수 
    {
        // 적1과 적2가 prefabs 배열의 4번째와 5번째 인덱스에 있다고 가정
        int randomIndex = Random.Range(4, 6); // 4 또는 5 중 랜덤한 값을 생성

        GameObject enemy = PoolManager.Instance.ActivateObj(randomIndex);
        SetObjPosition(enemy, spawnPoint[Random.Range(0, spawnPoint.Length)].transform);

        yield return new WaitForSeconds(7f);  // 7초마다 적 스폰

        StartCoroutine(EnemySpawn());  // 재귀적으로 코루틴 실행
    }*/

    /*//탄환, 총알
    [Header("Bullet")]
    [SerializeField]
    private Transform bulletPoint;
    [SerializeField]
    private GameObject bulletObj;
    [SerializeField]
    private float maxShootDelay = 0.2f;
    [SerializeField]
    private float currentShootDelay = 0.2f;
    [SerializeField]
    private Text bulletText;
    private int maxBullet = 30;
    private int currentBullet = 0;

    //무기 효과
    [Header("Weapon FX")]
    [SerializeField]
    private GameObject weaponFlashFX;
    [SerializeField]
    private Transform bulletCasePoint;
    [SerializeField]
    private GameObject bulletCaseFX;
    [SerializeField]
    private Transform weaponClipPoint;
    [SerializeField]
    private GameObject weaponClipFX;*/

    /*[Header("Effect")]
    [SerializeField]
    public GameObject bloodSprayEffect;*/





    // Update is called once per frame
    /* void Update()
     {
         bulletText.text = currentBullet + " "; //+ maxBullet;
     }
 */
    /*    //총_기능
        public void Shooting(Vector3 targetPosition, Enemy enemy, AudioSource weaponSound, AudioClip shootingSound)
        {
            currentShootDelay += Time.deltaTime;

            if (currentShootDelay < maxShootDelay || currentBullet <= 0)
                return;

            currentBullet -= 1;         //else 
            currentShootDelay = 0;     //else 

            weaponSound.clip = shootingSound;
            weaponSound.Play();

            Vector3 aim = (targetPosition - bulletPoint.position).normalized; //총알 방향

            //Instantiate(weaponFlashFX, bulletPoint);// Instantiate==복제
            GameObject flashFX = PoolManager.Instance.ActivateObj(1);
            SetObjPosition(flashFX, bulletCasePoint);
            flashFX.transform.rotation = Quaternion.LookRotation(aim, Vector3.up);

            //Instantiate(bulletCaseFX, bulletCasePoint);
            GameObject caseFX = PoolManager.Instance.ActivateObj(2);
            SetObjPosition(caseFX, bulletCasePoint);

            //Instantiate(bulletObj, bulletPoint.position, Quaternion.LookRotation(aim, Vector3.up)); //회전은 y축 기준

            GameObject prefabToSpawn = PoolManager.Instance.ActivateObj(0);
            SetObjPosition(prefabToSpawn, bulletPoint);
            prefabToSpawn.transform.rotation = Quaternion.LookRotation(aim, Vector3.up);


            //Raycast(적이 데미지 입는 코드_raycast)
            *//*
            if(enemy != null && enemy.enemyCurrentHP > 0)
            {
                enemy.enemyCurrentHP -= 1;
                Debug.Log("enemy HP :" + enemy.enemyCurrentHP);
            }
            *//*
        }



        public void ReroadClip()
        {
            Instantiate(weaponClipFX, weaponClipPoint);
            GameObject clipFX = PoolManager.Instance.ActivateObj(3);
            SetObjPosition(clipFX, weaponClipPoint);

            InitBullet();
        }

        //총알 개수_Canvas
        private void InitBullet()
        {
            currentBullet = maxBullet;
        }

        private void SetObjPosition(GameObject obj, Transform targetTransform)
        {
            obj.transform.position = targetTransform.position;
        }*/




    /* public int CurrentBullet
     {
         get { return currentBullet; }
     }
    
}*/
