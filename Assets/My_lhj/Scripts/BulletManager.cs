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

    private void BulletMove() //�Ѿ� �ӵ�
    {
        bulletRigibody.velocity = transform.forward * moveSpeed; //�Ѿ� �ӵ� = ���� * �ӵ�
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

        if (other.CompareTag("Subject"))
        {
            // ���� HP�� ���ҽ�Ű�� ���, TakeDamage �޼ҵ带 ȣ���Ͽ� ������ ó��
            if (other.gameObject.GetComponent<Subject>().isDead == false)
            {
                other.gameObject.GetComponent<Subject>().TakeDamage(1);   //������ 1�� ������
            }
        }


        DestroyBullet();  //�ı�
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
        contact.point, // ��ġ�� �����մϴ�.
        Quaternion.LookRotation(contact.normal)
        );

        bloodSprayPrefab.transform.SetParent(objectWeHit.gameObject.transform);
    }*/

