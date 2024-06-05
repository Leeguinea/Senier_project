using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MimicSpace
{
    public class mimic2 : MonoBehaviour
    {
        [Header("Controls")]
        [Tooltip("Body Height from ground")]
        [Range(0.5f, 5f)]
        public float height = 0.8f;
        public float speed = 5f;
        Vector3 velocity = Vector3.zero;
        public float velocityLerpCoef = 4f;
        Mimic myMimic;

        private Transform playerTransform;

        private void Start()
        {
            myMimic = GetComponent<Mimic>();
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            StartCoroutine(MoveRoutine());
        }

        void Update()
        {
            Vector3 direction = Vector3.zero;

            if (playerTransform != null)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

                if (distanceToPlayer < 1f)
                {

                    direction = (transform.position - playerTransform.position).normalized;
                }
                else
                {

                    direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
                }
            }

            velocity = Vector3.Lerp(velocity, direction * speed, velocityLerpCoef * Time.deltaTime);

            // Assigning velocity to the mimic to assure great leg placement
            myMimic.velocity = velocity;

            transform.position = transform.position + velocity * Time.deltaTime;

            RaycastHit hit;
            Vector3 destHeight = transform.position;
            if (Physics.Raycast(transform.position + Vector3.up * 5f, -Vector3.up, out hit))
                destHeight = new Vector3(transform.position.x, hit.point.y + height, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, destHeight, velocityLerpCoef * Time.deltaTime);
        }

        IEnumerator Move(Vector3 direction, float distance, float duration)
        {
            float elapsedTime = 0f;
            Vector3 start = transform.position;
            Vector3 end = start + direction.normalized * distance;
            while (elapsedTime < duration)
            {
                transform.position = Vector3.Lerp(start, end, (elapsedTime / duration));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }


        IEnumerator MoveRoutine()
        {
            while (true)
            {
                Vector3 randomDirection = GetRandomDirection();
                // ���� �������� 5�� ���� �̵�
                yield return Move(randomDirection, 5f, 5f); // �� �κ��� Move �Լ��� ���ǿ� ���� �޶��� �� ����
                                                            // 3�� ���� ����
                yield return new WaitForSeconds(3f); // 'Wait' ��� 'WaitForSeconds' ���
            }
        }


        Vector3 GetRandomDirection()
        {
            Vector3 direction = Vector3.zero;
            bool pathClear = false;
            int attempts = 0;

            while (!pathClear && attempts < 100)
            {
                direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
                pathClear = !Physics.Raycast(transform.position, direction, 5f); // 5 ���� �Ÿ� ���� ��ֹ��� ������ Ȯ��
                attempts++;
            }

            return direction;
        }
    }
}
