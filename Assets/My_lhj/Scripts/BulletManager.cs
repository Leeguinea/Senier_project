using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private Rigidbody bulletRigibody;
    private BossEnemy bossEnemy;

    [SerializeField]
    private float moveSpeed = 10f;
    private float destroyTime = 3f;


    void Start()
    {
        bulletRigibody = GetComponent<Rigidbody>();
        bossEnemy = FindObjectOfType<BossEnemy>();
    }


    void Update()
    {
        destroyTime -= Time.deltaTime;

        if (destroyTime <= 0)
        {
            DestroyBullet();
        }

        BulletMove();
    }

    private void BulletMove() //총알 속도
    {
        bulletRigibody.velocity = transform.forward * moveSpeed; //총알 속도 = 전면 * 속도
    }

    private void DestroyBullet()
    {
        //Destroy(gameObject);      
        gameObject.SetActive(false);
        destroyTime = 3;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision with: " + other.gameObject.name);

        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().enemyCurrentHP -= 1;   //맞으면 1씩 -1
            //collided = true; //충돌 여부 업데이트
        }

        if (other.CompareTag("Boss"))
        {
            Debug.Log("피격");
            bossEnemy.bossCurrentHP -= 1;   //맞으면 HP -1
            Debug.Log("보스의 hp : " + bossEnemy.bossCurrentHP);
            //collided = true;                //충돌 여부 업데이트 
        }

        if (other.CompareTag("BossHead"))
        {
            Debug.Log("헤드샷");
            bossEnemy.bossCurrentHP -= 1;  //맞으면 HP -10
            Debug.Log("보스의 hp : " + bossEnemy.bossCurrentHP);
            bossEnemy.headShotCnt += 1;     //충돌 여부 업데이트
            //collided = true;
        }

        if (other.CompareTag("Subject"))
        {
            // 직접 HP를 감소시키는 대신, TakeDamage 메소드를 호출하여 데미지 처리
            if (other.gameObject.GetComponent<Subject>().isDead == false)
            {
                other.gameObject.GetComponent<Subject>().TakeDamage(1);   //맞으면 1씩 데미지
            }
        }


        DestroyBullet();  //파괴
    }
}

    /*
    //BloodSpray_Effect
    private void OnCollisionEnter(Collision objectWeHit)
    {
        if(objectWeHit.gameObject.CompareTag("Subject"))
        {
            CreateBloodSprayEffect(objectWeHit);

        }
    }

    private void CreateBloodSprayEffect(Collision objectWeHit)
    {
        ContactPoint contact = objectWeHit.contacts[0];

        GameObject bloodSprayPrefab = Instantiate(
        GameManager.Instance.bloodSprayEffect,
        contact.point, // 위치를 지정합니다.
        Quaternion.LookRotation(contact.normal)
        );

        bloodSprayPrefab.transform.SetParent(objectWeHit.gameObject.transform);
    }*/

