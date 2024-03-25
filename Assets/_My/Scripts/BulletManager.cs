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

        BullectMove();
    }

    private void BullectMove()
    {
        bulletRigibody.velocity = transform.forward * moveSpeed; //총알 속도 = 전면 * 속도
    }

    private void DestroyBullet()
    {
        //Destroy(gameObject);      
        gameObject.SetActive(false);
        destroyTime = 3;
    }

    //충돌시 파괴, 충돌 감지  ->Header 충돌시 -5~-10의 데미지를 입게 할 것.
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().enemyCurrentHP -= 1;   //맞으면 1씩 -1
        }

        DestroyBullet();
    }
}
