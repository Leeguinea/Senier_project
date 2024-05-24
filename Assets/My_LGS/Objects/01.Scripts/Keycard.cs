using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keycard : MonoBehaviour
{
    [SerializeField]
    //public string KeyccardNum;
    public float rotationSpeed = 20f;   //회전 속도
    public float shakeIntensity = 0.1f; //흔들림 강도
    public float shakeFrequency = 2f;   //흔들림 주파수

    private Vector3 initialPosition;    //초기 위치


    void Start()
    {
        initialPosition = transform.position; //초기 위치 저장
    }


    void Update()
    {
        if (gameObject.activeSelf)
        {
            // 천천히 회전시킴
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            // 위아래로 흔들림을 추가
            float shakeAmount = Mathf.Sin(Time.time * shakeFrequency) * shakeIntensity;
            transform.position = initialPosition + Vector3.up * shakeAmount;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어와 접촉 시
        {
            if (this.gameObject.name == "Keycard_01") // 키카드 01
            {
                other.gameObject.GetComponent<InteractObjects>().haskey1 = true; // 획득
                gameObject.SetActive(false); // 오브젝트 비활성화
                Debug.Log("1번 카드키 획득");
            }
            if (this.gameObject.name == "Keycard_02") // 키카드 02
            {
                other.gameObject.GetComponent<InteractObjects>().haskey2 = true; // 획득
                gameObject.SetActive(false); // 오브젝트 비활성화
                Debug.Log("2번 카드키 획득");
            }
            if (this.gameObject.name == "Keycard_03") // 키카드 03
            {
                other.gameObject.GetComponent<InteractObjects>().haskey3 = true; // 획득
                gameObject.SetActive(false); // 오브젝트 비활성화
                Debug.Log("3번 카드키 획득");
            }
        }
    }
}
