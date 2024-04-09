using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private Rigidbody bulletRigibody;

    [SerializeField]
    private float moveSpeed = 10f;
    private float destroyTime = 3f;


    void Start()
    {
        bulletRigibody = GetComponent<Rigidbody>();
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

    private void BulletMove()
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
        if (other.CompareTag("Enemy"))
        {
            // 기존 로직 유지
            other.gameObject.GetComponent<Enemy>().enemyCurrentHP -= 1;   //맞으면 1씩 -1
        }

        if (other.CompareTag("Subject"))
        {
            // 직접 HP를 감소시키는 대신, TakeDamage 메소드를 호출하여 데미지 처리
            other.gameObject.GetComponent<Subject>().TakeDamage(1);   //맞으면 1씩 데미지
        }

        DestroyBullet();
    }


}