using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
using UnityEngine;
//using UnityEngine.Playables;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //źȯ, �Ѿ�
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

    //���� ȿ��
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
    private GameObject weaponClipFX;

    [Header("Enemy")]
    [SerializeField]
    private GameObject[] spawnPoint;



    // Start is called before the first frame update
    void Start()
    {
        Instance = this; //��𼭵� ���� ����, i���� �빮�� I�� ��� ���� �Ȼ���

        currentShootDelay = 0;

        InitBullet();

        //StartCoroutine(EnemySpawn());
    }

    // Update is called once per frame
    void Update()
    {
        bulletText.text = currentBullet + " / ��"; //+ maxBullet;
    }

    //��_���
    public void Shooting(Vector3 targetPosition, Enemy enemy, AudioSource weaponSound, AudioClip shootingSound)
    {
        currentShootDelay += Time.deltaTime;

        if (currentShootDelay < maxShootDelay || currentBullet <= 0)
            return;

        currentBullet -= 1;         //else 
        currentShootDelay = 0;     //else 

        weaponSound.clip = shootingSound;
        weaponSound.Play();

        Vector3 aim = (targetPosition - bulletPoint.position).normalized; //�Ѿ� ����

        //Instantiate(weaponFlashFX, bulletPoint);// Instantiate==����
        GameObject flashFX = PoolManager.Instance.ActivateObj(1);
        SetObjPosition(flashFX, bulletCasePoint);
        flashFX.transform.rotation = Quaternion.LookRotation(aim, Vector3.up);

        //Instantiate(bulletCaseFX, bulletCasePoint);
        GameObject caseFX = PoolManager.Instance.ActivateObj(2);
        SetObjPosition(caseFX, bulletCasePoint);

        //Instantiate(bulletObj, bulletPoint.position, Quaternion.LookRotation(aim, Vector3.up)); //ȸ���� y�� ����
    
        GameObject prefabToSpawn = PoolManager.Instance.ActivateObj(0);
        SetObjPosition(prefabToSpawn,bulletPoint);
        prefabToSpawn.transform.rotation = Quaternion.LookRotation(aim, Vector3.up);
        

        //Raycast(���� ������ �Դ� �ڵ�_raycast)
        /*
        if(enemy != null && enemy.enemyCurrentHP > 0)
        {
            enemy.enemyCurrentHP -= 1;
            Debug.Log("enemy HP :" + enemy.enemyCurrentHP);
        }
        */
    }



    public void ReroadClip()
    {
        Instantiate(weaponClipFX, weaponClipPoint);
        GameObject clipFX = PoolManager.Instance.ActivateObj(3);
        SetObjPosition(clipFX, weaponClipPoint);

        InitBullet();
    }

    //�Ѿ� ����_Canvas
    private void InitBullet()
    {
        currentBullet = maxBullet;
    }

    private void SetObjPosition(GameObject obj, Transform targetTransform)
    {
        obj.transform.position = targetTransform.position;
    }

    IEnumerator EnemySpawn() // �� ���� �Լ�
    {
        // ��1�� ��2�� prefabs �迭�� 4��°�� 5��° �ε����� �ִٰ� ����
        int randomIndex = Random.Range(4, 6); // 4 �Ǵ� 5 �� ������ ���� ����

        GameObject enemy = PoolManager.Instance.ActivateObj(randomIndex);
        SetObjPosition(enemy, spawnPoint[Random.Range(0, spawnPoint.Length)].transform);

        yield return new WaitForSeconds(7f);  // 7�ʸ��� �� ����

        StartCoroutine(EnemySpawn());  // ��������� �ڷ�ƾ ����
    }


    /* IEnumerator EnemySpawn() // �� ���� �Լ�
     {
         //Instantiate(enemy, spawnPoint[Random.Range(0, spawnPoint.Length)].transform.position, Quaternion.identity);
         GameObject enemy = PoolManager.Instance.ActivateObj(4);
         SetObjPosition(enemy, spawnPoint[Random.Range(0, spawnPoint.Length)].transform);

         yield return new WaitForSeconds(7f);  //7�ʸ��� �� ����

         StartCoroutine(EnemySpawn());  //����
     }*/
}
