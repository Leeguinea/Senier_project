using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealKit : MonoBehaviour
{
    [SerializeField]
    public int healAmount = 50;    //힐량
    public float respawnTime = 2f;  //리스폰 타이머

    public float rotationSpeed = 20f;   //회전 속도
    public float shakeIntensity = 0.1f; //흔들림 강도
    public float shakeFrequency = 2f;   //흔들림 주파수

    private Vector3 initialPosition;    //초기 위치

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position; //초기 위치 저장
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            // 힐 팩을 천천히 회전시킴
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            // 위아래로 흔들림을 추가
            float shakeAmount = Mathf.Sin(Time.time * shakeFrequency) * shakeIntensity;
            transform.position = initialPosition + Vector3.up * shakeAmount;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) //플레이어와 접촉 시
        {
            other.gameObject.GetComponent<PlayerHealth>().RestoreHealth(healAmount); //힐링
            gameObject.SetActive(false);    //힐팩 비활성화
            Invoke("Respawn", respawnTime); //힐팩 리스폰
        }
    }

    private void Respawn() //힐팩 리스폰
    {
        gameObject.SetActive(true); //힐팩 활성화
    }
}
