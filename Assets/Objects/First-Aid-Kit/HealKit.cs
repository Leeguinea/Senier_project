using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealKit : MonoBehaviour
{
    [SerializeField]
    public int healAmount = 50;    //����
    public float respawnTime = 2f;  //������ Ÿ�̸�

    public float rotationSpeed = 20f;   //ȸ�� �ӵ�
    public float shakeIntensity = 0.1f; //��鸲 ����
    public float shakeFrequency = 2f;   //��鸲 ���ļ�

    private Vector3 initialPosition;    //�ʱ� ��ġ

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position; //�ʱ� ��ġ ����
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            // �� ���� õõ�� ȸ����Ŵ
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            // ���Ʒ��� ��鸲�� �߰�
            float shakeAmount = Mathf.Sin(Time.time * shakeFrequency) * shakeIntensity;
            transform.position = initialPosition + Vector3.up * shakeAmount;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) //�÷��̾�� ���� ��
        {
            other.gameObject.GetComponent<PlayerHealth>().RestoreHealth(healAmount); //����
            gameObject.SetActive(false);    //���� ��Ȱ��ȭ
            Invoke("Respawn", respawnTime); //���� ������
        }
    }

    private void Respawn() //���� ������
    {
        gameObject.SetActive(true); //���� Ȱ��ȭ
    }
}
