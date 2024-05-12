using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFixer : MonoBehaviour
{
    private static Vector3 originalPosition;
    private static Quaternion originalRotation;

    private Transform mainCameraTransform;

    public static bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        // ���� ī�޶��� ��ġ�� ȸ�� ���� �����մϴ�.
        //originalPosition = transform.position;
        //originalRotation = transform.rotation;

        mainCameraTransform = GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused)
        {
            mainCameraTransform.position = mainCameraTransform.position;
            //isPaused = true;
        }
    }


    // ī�޶��� ��ġ�� ȸ�� ���� ������ŵ�ϴ�.
    public static void FixCamera()
    {
        // ���� ī�޶��� Transform ������Ʈ�� �����ɴϴ�.
        //Transform mainCameraTransform = Camera.main.transform;

        // ���� ��ġ�� ȸ�� ���� �����մϴ�.
        //originalPosition = mainCameraTransform.position;
        //originalRotation = mainCameraTransform.rotation;

        isPaused = true;

    }

    public static void UnfixCamera()
    {
        // ���� ī�޶��� Transform ������Ʈ�� �����ɴϴ�.
        //Transform mainCameraTransform = Camera.main.transform;

        // ���� ��ġ�� ȸ�� ���� �����մϴ�.
        //mainCameraTransform.position = originalPosition;
        //mainCameraTransform.rotation = originalRotation;

        isPaused = false;

    }

}
