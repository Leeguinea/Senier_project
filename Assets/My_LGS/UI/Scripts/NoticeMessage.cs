using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoticeMessage : MonoBehaviour
{
    [Header("NoticeMessage")]
    public GameObject Panel;
    public TextMeshProUGUI MessageText;
    public Animator NoticeMessageAnimator;

    // �ڷ�ƾ ������
    private WaitForSecondsRealtime _UIDelay1 = new WaitForSecondsRealtime(2f); // Ȱ��ȭ �ð�
    private WaitForSecondsRealtime _UIDelay2 = new WaitForSecondsRealtime(0.3f);

    void Start()
    {
        if (Panel == null)
        {
            Debug.LogError("Pannel�� �Ҵ���� �ʾҽ��ϴ�!");
        }

        if (MessageText == null)
        {
            Debug.LogError("MessageText�� �Ҵ���� �ʾҽ��ϴ�!");
            return;
        }

        if (NoticeMessageAnimator == null)
        {
            Debug.LogError("NoticeMessageAnimator�� �Ҵ���� �ʾҽ��ϴ�!");
            return;
        }
        NoticeMessageAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;


        Panel.SetActive(false); // �˸�â ��Ȱ��ȭ
    }

    public void DisplayNotice(string message)
    {
        MessageText.text = message;
        Panel.SetActive(false);
        StopAllCoroutines(); // ���� ���� ��� �ڷ�ƾ�� �����մϴ�

        StartCoroutine(DisplayAni()); // �˸��� ǥ���ϴ� �ڷ�ƾ ����
    }

    IEnumerator DisplayAni()
    {
        Panel.SetActive(true);      // �˸�â Ȱ��ȭ
        NoticeMessageAnimator.SetBool("isNotice", true);
        yield return _UIDelay1;     // ������ �� ���� (�޽��� ǥ�� �ð�)

        NoticeMessageAnimator.SetBool("isNotice", false);
        yield return _UIDelay2;     // ������ �� ���� (��Ȱ��ȭ �ִϸ��̼� ��, �г� ��Ȱ��ȭ)
        Panel.SetActive(false);     // �˸�â ��Ȱ��ȭ
    }

}
