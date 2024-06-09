using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseDoor : MonoBehaviour
{
    [Header("Door Setting")]
    public bool open = false;
    public float doorOpenAngle = 90f;
    public float doorCloseAngle = 0f;
    public float smoot = 1f;
    private float colliderDisableTime = 1f; // 콜라이더 비활성화 시간
    private Collider doorCollider;


    [Header("Door Sound")]
    public AudioSource doorSound;
    private float soundPlaybackSpeed = 0.7f; // 효과음 재생 속도 조절



    private void Start()
    {
        doorCollider = GetComponent<Collider>();

        doorSound.pitch = soundPlaybackSpeed;  // 오디오 소스의 재생 속도 설정
    }

    void Update()
    {
        if (open)
        {
            Quaternion targetRotation = Quaternion.Euler(0, doorOpenAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smoot * Time.deltaTime);
        }
        else
        {
            Quaternion targetRotation2 = Quaternion.Euler(0, doorCloseAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation2, smoot * Time.deltaTime);
        }

    }


    public void ChangeDoorState()
    {
        open = !open;
        StartCoroutine(OpenCloseCoroutine());
    }


    private IEnumerator OpenCloseCoroutine()
    {
        doorSound.Play(); // 문 열기/닫기 사운드 재생

        doorCollider.enabled = false; // 콜라이더 비활성화

        yield return new WaitForSeconds(colliderDisableTime); // 문이 열린/닫힌 상태를 유지하는 시간

        doorCollider.enabled = true; // 콜라이더 다시 활성화
    }
}
