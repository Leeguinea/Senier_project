using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoticeMessage : MonoBehaviour
{
    [Header("NoticeMessage")]
    public GameObject noticeMessageBox;
    public Text noticeMessagetext;
    public Animator noticeMessageAni;

    // �ڷ�ƾ ������
    private WaitForSeconds _UIDelay1 = new WaitForSeconds(1.0f);
    private WaitForSeconds _UIDelay2 = new WaitForSeconds(0.3f);


    // Start is called before the first frame update
    void Start()
    {
        if (noticeMessageBox == null)
        {
            Debug.LogError("noticeMessageBox�� �Ҵ���� �ʾҽ��ϴ�!");
        }
        
        noticeMessageBox.SetActive(false); //�˸�â ��Ȱ��ȭ
    }

    public void Notice(string message)
    {
        noticeMessagetext.text = message;
        noticeMessageBox.SetActive(false);
        StopAllCoroutines();

        StartCoroutine(SubDelay()); //������ ����
    }

    IEnumerator SubDelay()
    {
        noticeMessageBox.SetActive(true);     // �˸�â Ȱ��ȭ
        noticeMessageAni.SetBool("isOn", true);
        yield return _UIDelay1;     // 2���� ����

        noticeMessageAni.SetBool("isOn", false);
        yield return _UIDelay2;     // 0.3���� ����
        noticeMessageBox.SetActive(false);    // �˸�â ��Ȱ��ȭ
    }

    private void Update()
    {
        if (noticeMessageAni.GetBool("isOn") == true)
        {
            //Debug.Log("isOn == true");
        }
        else
        {
            //Debug.Log("isOn == false");
        }
    }
}
