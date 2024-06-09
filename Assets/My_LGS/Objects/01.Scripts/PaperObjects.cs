using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Xml;
using Unity.VisualScripting;
using TMPro;


public class PaperObjects : MonoBehaviour
{
    public string filePath = "Assets/My_LGS/Objects/Clipboard/Paper.xml";
    public string obj_id;

    private Camera mainCamera; // 플레이어 카메라
    private Vector3 originalPosition; // 원래 위치
    private Quaternion originalRotation; // 원래 회전
    public bool isInteracting = false; // 상호작용 중 여부

    private Collider paperCollider; // 문서 오브젝트의 콜라이더


    // Start is called before the first frame update
    void Start()
    {
        // 메인 카메라 
        mainCamera = Camera.main;
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        // 시작할 때 콜라이더를 가져옴
        paperCollider = GetComponent<Collider>();

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
                //Debug.Log("title : " + title);
                //Debug.Log("date : " + date);
                //Debug.Log("content : " + content);

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
        if (isInteracting) //상호작용 일시
        {
            StartInteraction();
            //PauseManager.PauseGame();

            // ESC 키, F키를 눌렀을 때 상호작용 해제
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.F))
            {
                EndInteraction();
                //PauseManager.ResumeGame();
            }
        }
    }


    public void StartInteraction() // 플레이어와 상호작용 시작
    {
        //PauseManager.PauseGame();

        Time.timeScale = 0f;

        // 콜라이더를 비활성화
        if (paperCollider != null)
        {
            paperCollider.enabled = false;
        }

        // 문서 오브젝트를 플레이어 카메라 근처로 이동 및 확대
        transform.position = mainCamera.transform.position + mainCamera.transform.forward * 0.155f + Vector3.up * 0.10f;

        // 문서 오브젝트를 플레이어의 시선과 일치하도록 회전
        transform.rotation = Quaternion.LookRotation(mainCamera.transform.forward);
        transform.Rotate(90f,180f, 0f);

    }


    public void EndInteraction() // 플레이어와 상호작용 종료
    {
        // 게임을 다시 재개
        //PauseManager.ResumeGame();
        isInteracting = false;
        Time.timeScale = 1f;

        // 콜라이더를 활성화
        if (paperCollider != null)
        {
            paperCollider.enabled = true;
        }

        // 문서 오브젝트를 원래 위치와 회전으로 되돌림
        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }

}

