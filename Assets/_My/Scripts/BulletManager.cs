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
        bulletRigibody.velocity = transform.forward * moveSpeed; //�Ѿ� �ӵ� = ���� * �ӵ�
    }

    private void DestroyBullet()
    {
        gameObject.SetActive(false);
        destroyTime = 3;
    }

    //�浹�� �ı�, �浹 ����  ->Header �浹�� -5~-10�� �������� �԰� �� ��.
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision with: " + other.gameObject.name);

        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().enemyCurrentHP -= 1;   //������ 1�� -1
            //collided = true; //�浹 ���� ������Ʈ
        }

        if (other.CompareTag("Boss"))
        {
            Debug.Log("�ǰ�");
            bossEnemy.bossCurrentHP -= 1;   //������ HP -1
            Debug.Log("������ hp : " + bossEnemy.bossCurrentHP);
            //collided = true;                //�浹 ���� ������Ʈ 
        }

        if (other.CompareTag("BossHead"))
        {
            Debug.Log("��弦");
            bossEnemy.bossCurrentHP -= 1;  //������ HP -10
            Debug.Log("������ hp : " + bossEnemy.bossCurrentHP);
            bossEnemy.headShotCnt += 1;     //�浹 ���� ������Ʈ
            //collided = true;
        }

        DestroyBullet();
    }
}