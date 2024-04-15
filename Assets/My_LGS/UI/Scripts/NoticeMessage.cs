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

    // 코루틴 딜레이
    private WaitForSeconds _UIDelay1 = new WaitForSeconds(1.0f);
    private WaitForSeconds _UIDelay2 = new WaitForSeconds(0.3f);


    // Start is called before the first frame update
    void Start()
    {
        if (noticeMessageBox == null)
        {
            Debug.LogError("noticeMessageBox가 할당되지 않았습니다!");
        }
        
        noticeMessageBox.SetActive(false); //알림창 비활성화
    }

    public void Notice(string message)
    {
        noticeMessagetext.text = message;
        noticeMessageBox.SetActive(false);
        StopAllCoroutines();

        StartCoroutine(SubDelay()); //딜레이 실행
    }

    IEnumerator SubDelay()
    {
        noticeMessageBox.SetActive(true);     // 알림창 활성화
        noticeMessageAni.SetBool("isOn", true);
        yield return _UIDelay1;     // 2초후 실행

        noticeMessageAni.SetBool("isOn", false);
        yield return _UIDelay2;     // 0.3초후 실행
        noticeMessageBox.SetActive(false);    // 알림창 비활성화
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
