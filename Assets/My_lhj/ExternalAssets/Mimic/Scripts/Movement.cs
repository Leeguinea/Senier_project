using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MimicSpace
{
    /// <summary>
    /// This is a very basic movement script, if you want to replace it
    /// Just don't forget to update the Mimic's velocity vector with a Vector3(x, 0, z)
    /// </summary>
    public class Movement : MonoBehaviour
    {
        [Header("Controls")]
        [Tooltip("Body Height from ground")]
        [Range(0.5f, 5f)]
        public float height = 0.8f;
        public float speed = 5f;
        Vector3 velocity = Vector3.zero;
        public float velocityLerpCoef = 4f;
        Mimic myMimic;

        private void Start()
        {
            myMimic = GetComponent<Mimic>();
            StartCoroutine(MoveRoutine());
        }

        void Update()
        {
            velocity = Vector3.Lerp(velocity, new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * speed, velocityLerpCoef * Time.deltaTime);

            // Assigning velocity to the mimic to assure great leg placement
            myMimic.velocity = velocity;

            transform.position = transform.position + velocity * Time.deltaTime;
            RaycastHit hit;
            Vector3 destHeight = transform.position;
            if (Physics.Raycast(transform.position + Vector3.up * 5f, -Vector3.up, out hit))
                destHeight = new Vector3(transform.position.x, hit.point.y + height, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, destHeight, velocityLerpCoef * Time.deltaTime);
        }

        IEnumerator MoveRoutine()
        {
            Vector3 initialPosition = transform.position; // 처음 위치 저장

            // 이동 및 정지 로직을 함수로 분리
            yield return Move(Vector3.forward, 20f, 5f);  // z축으로 5초 동안 이동
            yield return Wait(3f);                        // 3초 동안 정지
            yield return Move(Vector3.right, -20f, 5f);    // x축으로 5초 동안 이동
            yield return Wait(3f);                        // 3초 동안 정지
            yield return MoveToInitial(initialPosition, 5f); // 처음 위치로 5초 동안 이동
            yield return Wait(3f);                        // 3초 동안 정지
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

        IEnumerator Wait(float duration)
        {
            yield return new WaitForSeconds(duration);
        }

        IEnumerator MoveToInitial(Vector3 initialPosition, float duration)
        {
            float elapsedTime = 0f;
            Vector3 start = transform.position;
            while (elapsedTime < duration)
            {
                transform.position = Vector3.Lerp(start, initialPosition, (elapsedTime / duration));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }

    }
}
