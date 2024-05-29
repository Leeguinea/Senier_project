using UnityEngine;

public class Knife : MonoBehaviour
{
    public int damage = 2; // 칼이 입힐 데미지 양

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter called with: " + other.name); // 이벤트 발생 확인

        if (other.CompareTag("Subject"))
        {
            Subject subject = other.GetComponent<Subject>();
            if (subject != null)
            {
                subject.TakeDamage(damage);
                Debug.Log("Damage dealt to: " + other.name); // 데미지가 제대로 입혀졌는지 확인
            }
        }
    }

}


