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

    // 코루틴 딜레이
    private WaitForSecondsRealtime _UIDelay1 = new WaitForSecondsRealtime(2f); // 활성화 시간
    private WaitForSecondsRealtime _UIDelay2 = new WaitForSecondsRealtime(0.3f);

    void Start()
    {
        if (Panel == null)
        {
            Debug.LogError("Pannel가 할당되지 않았습니다!");
        }

        if (MessageText == null)
        {
            Debug.LogError("MessageText가 할당되지 않았습니다!");
            return;
        }

        if (NoticeMessageAnimator == null)
        {
            Debug.LogError("NoticeMessageAnimator가 할당되지 않았습니다!");
            return;
        }
        NoticeMessageAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;


        Panel.SetActive(false); // 알림창 비활성화
    }

    public void DisplayNotice(string message)
    {
        MessageText.text = message;
        Panel.SetActive(false);
        StopAllCoroutines(); // 실행 중인 모든 코루틴을 중지합니다

        StartCoroutine(DisplayAni()); // 알림을 표시하는 코루틴 시작
    }

    IEnumerator DisplayAni()
    {
        Panel.SetActive(true);      // 알림창 활성화
        NoticeMessageAnimator.SetBool("isNotice", true);
        yield return _UIDelay1;     // 딜레이 후 실행 (메시지 표시 시간)

        NoticeMessageAnimator.SetBool("isNotice", false);
        yield return _UIDelay2;     // 딜레이 후 실행 (비활성화 애니메이션 후, 패널 비활성화)
        Panel.SetActive(false);     // 알림창 비활성화
    }

}
