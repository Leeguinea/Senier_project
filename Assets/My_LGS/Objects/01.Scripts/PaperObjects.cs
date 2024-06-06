using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Xml;
using TMPro;


public class PaperObjects : MonoBehaviour
{
    [Header("Paper Setting")]
    public string filePath = "Assets/My_LGS/Objects/Clipboard/Paper.xml";
    public string obj_id;
    public bool isInteracting; // 상호작용 중 여부

    [Header("Paper sound")]
    public AudioSource papeSound;

    [Header("Aim UI")]
    public GameObject gunUI; // 총 UI
    public GameObject interactedAimUI; // 에이밍 UI

    private Camera mainCamera; // 플레이어 카메라
    private Vector3 originalPosition; // 원래 위치
    private Quaternion originalRotation; // 원래 회전


    // Start is called before the first frame update
    void Start()
    {
        isInteracting = false;

        // 메인 카메라 
        mainCamera = Camera.main;
        originalPosition = transform.position;
        originalRotation = transform.rotation;


        // 문서 파일 디스플레이
        if (System.IO.File.Exists(filePath))
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);

            // obj_id와 일치하는 paper 노드를 찾음
            XmlNode paper = xmlDoc.SelectSingleNode("/papers/paper[obj_id='" + obj_id + "']");
            if (paper != null)
            {
                // paper 노드의 자식 노드에서 정보 추출
                string title = paper.SelectSingleNode("title").InnerText;
                string date = paper.SelectSingleNode("date").InnerText;
                string content = paper.SelectSingleNode("content").InnerText;

                Transform titleTransform = transform.Find("Title");
                Transform dateTransform = transform.Find("Date");
                Transform contentTransform = transform.Find("Content");

                if (titleTransform != null)
                {
                    // Title 오브젝트가 있으면 TextMeshPro 컴포넌트를 가져와서 텍스트 설정
                    TextMeshPro titleText = titleTransform.GetComponent<TextMeshPro>();
                    //Debug.Log(GUItitleText.text);
                    if (titleText != null)
                    {
                        titleText.text = title;
                    }
                    else
                    {
                        Debug.Log("TextMeshProUGUI component not found on the Title object.");
                    }
                }

                if (dateTransform != null)
                {
                    // Date 오브젝트가 있으면 TextMeshPro 컴포넌트를 가져와서 텍스트 설정
                    TextMeshPro dateText = dateTransform.GetComponent<TextMeshPro>();
                    //Debug.Log(GUIdateText.text);
                    if (dateText != null)
                    {
                        dateText.text = date;
                    }
                    else
                    {
                        Debug.Log("TextMeshProUGUI component not found on the Date object.");
                    }
                }

                if (contentTransform != null)
                {
                    // Content 오브젝트가 있으면 TextMeshPro 컴포넌트를 가져와서 텍스트 설정
                    TextMeshPro contentText = contentTransform.GetComponent<TextMeshPro>();
                    //Debug.Log(GUIcontentText.text);
                    if (contentText != null)
                    {
                        contentText.text = content;
                    }
                    else
                    {
                        Debug.Log("TextMeshProUGUI component not found on the Content object.");
                    }
                }


            }
            else
            {
                Debug.LogError("Paper with obj_id " + obj_id + " not found in XML file.");
            }
        }
        else
        {
            Debug.LogError("Report XML file not found at path: " + filePath);
        }
    }

    private void Update()
    {     
        if (isInteracting == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                EndInteraction();
            }
        }
        

    }


    public void StartInteraction() // 플레이어와 상호작용 시작
    {
        // 게임을 일시중지
        Time.timeScale = 0f;
        isInteracting = true;

        // UI 비활성화
        gunUI.SetActive(false);
        interactedAimUI.SetActive(false);

        // 문서 오브젝트를 플레이어 카메라 근처로 이동 및 확대
        transform.position = mainCamera.transform.position + mainCamera.transform.forward * 0.205f + Vector3.up * 0.000f;

        // 문서 오브젝트를 플레이어의 시선과 일치하도록 회전
        transform.rotation = Quaternion.LookRotation(mainCamera.transform.forward);
        transform.Rotate(90f,180f, 0f);

        // 사운드 출력
        papeSound.Play();
    }


    public void EndInteraction() // 플레이어와 상호작용 종료
    {
        // 게임을 다시 재개
        Time.timeScale = 1f;
        isInteracting = false;

        // UI 활성화
        gunUI.SetActive(true);
        interactedAimUI.SetActive(true);

        // 문서 오브젝트를 원래 위치와 회전으로 되돌림
        transform.position = originalPosition;
        transform.rotation = originalRotation;

        // 사운드 출력
        papeSound.Play();
    }

}

