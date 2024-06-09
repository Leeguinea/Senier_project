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
    private float colliderDisableTime = 1f; // �ݶ��̴� ��Ȱ��ȭ �ð�
    private Collider doorCollider;


    [Header("Door Sound")]
    public AudioSource doorSound;
    private float soundPlaybackSpeed = 0.7f; // ȿ���� ��� �ӵ� ����



    private void Start()
    {
        doorCollider = GetComponent<Collider>();

        doorSound.pitch = soundPlaybackSpeed;  // ����� �ҽ��� ��� �ӵ� ����
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
        doorSound.Play(); // �� ����/�ݱ� ���� ���

        doorCollider.enabled = false; // �ݶ��̴� ��Ȱ��ȭ

        yield return new WaitForSeconds(colliderDisableTime); // ���� ����/���� ���¸� �����ϴ� �ð�

        doorCollider.enabled = true; // �ݶ��̴� �ٽ� Ȱ��ȭ
    }
}
