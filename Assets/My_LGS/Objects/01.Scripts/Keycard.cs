using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keycard : MonoBehaviour
{
    [SerializeField]
    //public string KeyccardNum;
    public float rotationSpeed = 20f;   //ȸ�� �ӵ�
    public float shakeIntensity = 0.1f; //��鸲 ����
    public float shakeFrequency = 2f;   //��鸲 ���ļ�

    private Vector3 initialPosition;    //�ʱ� ��ġ


    void Start()
    {
        initialPosition = transform.position; //�ʱ� ��ġ ����
    }


    void Update()
    {
        if (gameObject.activeSelf)
        {
            // õõ�� ȸ����Ŵ
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            // ���Ʒ��� ��鸲�� �߰�
            float shakeAmount = Mathf.Sin(Time.time * shakeFrequency) * shakeIntensity;
            transform.position = initialPosition + Vector3.up * shakeAmount;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // �÷��̾�� ���� ��
        {
            if (this.gameObject.name == "Keycard_01") // Űī�� 01
            {
                other.gameObject.GetComponent<InteractObjects>().haskey1 = true; // ȹ��
                gameObject.SetActive(false); // ������Ʈ ��Ȱ��ȭ
                Debug.Log("1�� ī��Ű ȹ��");
            }
            if (this.gameObject.name == "Keycard_02") // Űī�� 02
            {
                other.gameObject.GetComponent<InteractObjects>().haskey2 = true; // ȹ��
                gameObject.SetActive(false); // ������Ʈ ��Ȱ��ȭ
                Debug.Log("2�� ī��Ű ȹ��");
            }
            if (this.gameObject.name == "Keycard_03") // Űī�� 03
            {
                other.gameObject.GetComponent<InteractObjects>().haskey3 = true; // ȹ��
                gameObject.SetActive(false); // ������Ʈ ��Ȱ��ȭ
                Debug.Log("3�� ī��Ű ȹ��");
            }
        }
    }
}
