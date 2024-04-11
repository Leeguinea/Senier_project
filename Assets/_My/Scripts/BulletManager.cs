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
        if (other.CompareTag("Enemy"))
        {
            // ���� ���� ����
            other.gameObject.GetComponent<Enemy>().enemyCurrentHP -= 1;   //������ 1�� -1
        }

        if (other.CompareTag("Subject"))
        {
            // ���� HP�� ���ҽ�Ű�� ���, TakeDamage �޼ҵ带 ȣ���Ͽ� ������ ó��
            other.gameObject.GetComponent<Subject>().TakeDamage(1);   //������ 1�� ������
        }

        DestroyBullet();
    }


}