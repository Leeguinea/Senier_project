using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//BloodSpray와 관련된 스크립트로, 특정 기간이 지나면 개체를 파괴하는 기능임.

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
