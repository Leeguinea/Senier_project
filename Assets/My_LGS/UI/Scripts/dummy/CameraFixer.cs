using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFixer : MonoBehaviour
{
    // 메인 카메라의 초기 위치와 회전
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private Transform mainCameraTransform;

    public static bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        // 초기 위치와 회전 저장
        //initialPosition = mainCameraTransform.position;
        //initialRotation = mainCameraTransform.rotation;

        mainCameraTransform = GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused)
        {
            mainCameraTransform.position = mainCameraTransform.position;
            //mainCameraTransform.rotation = initialRotation;
            //isPaused = true;
        }
    }


    // 카메라의 위치와 회전 값을 고정시킵니다.
    public static void FixCamera()
    {
        // 메인 카메라의 Transform 컴포넌트를 가져옵니다.
        //Transform mainCameraTransform = Camera.main.transform;

        // 현재 위치와 회전 값을 저장합니다.
        //originalPosition = mainCameraTransform.position;
        //originalRotation = mainCameraTransform.rotation;

        isPaused = true;

    }

    public static void UnfixCamera()
    {
        // 메인 카메라의 Transform 컴포넌트를 가져옵니다.
        //Transform mainCameraTransform = Camera.main.transform;

        // 원래 위치와 회전 값을 복원합니다.
        //mainCameraTransform.position = originalPosition;
        //mainCameraTransform.rotation = originalRotation;

        isPaused = false;

    }

}
