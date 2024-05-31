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

    private Camera mainCamera; // �÷��̾� ī�޶�
    private Vector3 originalPosition; // ���� ��ġ
    private Quaternion originalRotation; // ���� ȸ��
    public bool isInteracting = false; // ��ȣ�ۿ� �� ����

    private Collider paperCollider; // ���� ������Ʈ�� �ݶ��̴�


    // Start is called before the first frame update
    void Start()
    {
        // ���� ī�޶� 
        mainCamera = Camera.main;
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        // ������ �� �ݶ��̴��� ������
        paperCollider = GetComponent<Collider>();

        // ���� ���� ���÷���
        if (System.IO.File.Exists(filePath))
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);

            // obj_id�� ��ġ�ϴ� paper ��带 ã��
            XmlNode paper = xmlDoc.SelectSingleNode("/papers/paper[obj_id='" + obj_id + "']");
            if (paper != null)
            {
                // paper ����� �ڽ� ��忡�� ���� ����
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
                    // Title ������Ʈ�� ������ TextMeshPro ������Ʈ�� �����ͼ� �ؽ�Ʈ ����
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
                    // Date ������Ʈ�� ������ TextMeshPro ������Ʈ�� �����ͼ� �ؽ�Ʈ ����
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
                    // Content ������Ʈ�� ������ TextMeshPro ������Ʈ�� �����ͼ� �ؽ�Ʈ ����
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
        if (isInteracting) //��ȣ�ۿ� �Ͻ�
        {
            StartInteraction();
            //PauseManager.PauseGame();

            // ESC Ű, FŰ�� ������ �� ��ȣ�ۿ� ����
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.F))
            {
                EndInteraction();
                //PauseManager.ResumeGame();
            }
        }
    }


    public void StartInteraction() // �÷��̾�� ��ȣ�ۿ� ����
    {
        //PauseManager.PauseGame();

        Time.timeScale = 0f;

        // �ݶ��̴��� ��Ȱ��ȭ
        if (paperCollider != null)
        {
            paperCollider.enabled = false;
        }

        // ���� ������Ʈ�� �÷��̾� ī�޶� ��ó�� �̵� �� Ȯ��
        transform.position = mainCamera.transform.position + mainCamera.transform.forward * 0.155f + Vector3.up * 0.10f;

        // ���� ������Ʈ�� �÷��̾��� �ü��� ��ġ�ϵ��� ȸ��
        transform.rotation = Quaternion.LookRotation(mainCamera.transform.forward);
        transform.Rotate(90f,180f, 0f);

    }


    public void EndInteraction() // �÷��̾�� ��ȣ�ۿ� ����
    {
        // ������ �ٽ� �簳
        //PauseManager.ResumeGame();
        isInteracting = false;
        Time.timeScale = 1f;

        // �ݶ��̴��� Ȱ��ȭ
        if (paperCollider != null)
        {
            paperCollider.enabled = true;
        }

        // ���� ������Ʈ�� ���� ��ġ�� ȸ������ �ǵ���
        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }

}

