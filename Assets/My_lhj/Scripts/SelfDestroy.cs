using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//BloodSpray�� ���õ� ��ũ��Ʈ��, Ư�� �Ⱓ�� ������ ��ü�� �ı��ϴ� �����.

public class SelfDestroy : MonoBehaviour
{
    public float timeForDestruction;


    void Start()
    {
        StartCoroutine(DestroySelf(timeForDestruction));
    }

    private IEnumerator DestroySelf(float timeForDestruction)
    {
        yield return new WaitForSeconds(timeForDestruction);

        Destroy(gameObject);
    }
}
