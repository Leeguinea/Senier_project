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
    public bool isInteracting; // ��ȣ�ۿ� �� ����

    [Header("Paper sound")]
    public AudioSource papeSound;

    [Header("Aim UI")]
    public GameObject gunUI; // �� UI
    public GameObject interactedAimUI; // ���̹� UI

    private Camera mainCamera; // �÷��̾� ī�޶�
    private Vector3 originalPosition; // ���� ��ġ
    private Quaternion originalRotation; // ���� ȸ��


    // Start is called before the first frame update
    void Start()
    {
        isInteracting = false;

        // ���� ī�޶� 
        mainCamera = Camera.main;
        originalPosition = transform.position;
        originalRotation = transform.rotation;


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
        if (isInteracting == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                EndInteraction();
            }
        }
        

    }


    public void StartInteraction() // �÷��̾�� ��ȣ�ۿ� ����
    {
        // ������ �Ͻ�����
        Time.timeScale = 0f;
        isInteracting = true;

        // UI ��Ȱ��ȭ
        gunUI.SetActive(false);
        interactedAimUI.SetActive(false);

        // ���� ������Ʈ�� �÷��̾� ī�޶� ��ó�� �̵� �� Ȯ��
        transform.position = mainCamera.transform.position + mainCamera.transform.forward * 0.205f + Vector3.up * 0.000f;

        // ���� ������Ʈ�� �÷��̾��� �ü��� ��ġ�ϵ��� ȸ��
        transform.rotation = Quaternion.LookRotation(mainCamera.transform.forward);
        transform.Rotate(90f,180f, 0f);

        // ���� ���
        papeSound.Play();
    }


    public void EndInteraction() // �÷��̾�� ��ȣ�ۿ� ����
    {
        // ������ �ٽ� �簳
        Time.timeScale = 1f;
        isInteracting = false;

        // UI Ȱ��ȭ
        gunUI.SetActive(true);
        interactedAimUI.SetActive(true);

        // ���� ������Ʈ�� ���� ��ġ�� ȸ������ �ǵ���
        transform.position = originalPosition;
        transform.rotation = originalRotation;

        // ���� ���
        papeSound.Play();
    }

}

