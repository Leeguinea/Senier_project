using UnityEngine;

public class Knife : MonoBehaviour
{
    public int damage = 2; // Į�� ���� ������ ��

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter called with: " + other.name); // �̺�Ʈ �߻� Ȯ��

        if (other.CompareTag("Subject"))
        {
            Subject subject = other.GetComponent<Subject>();
            if (subject != null)
            {
                subject.TakeDamage(damage);
                Debug.Log("Damage dealt to: " + other.name); // �������� ����� ���������� Ȯ��
            }
        }
    }

}


