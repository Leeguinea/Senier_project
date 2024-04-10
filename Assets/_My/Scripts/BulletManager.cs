using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

        BullectMove();
    }

    private void BullectMove()
    {
        bulletRigibody.velocity = transform.forward * moveSpeed; //총알 속도 = 전면 * 속도
    }

    private void DestroyBullet()
    {
        gameObject.SetActive(false);
        destroyTime = 3;
    }

    //충돌시 파괴, 충돌 감지  ->Header 충돌시 -5~-10의 데미지를 입게 할 것.
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

        DestroyBullet();
    }
}