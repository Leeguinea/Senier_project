/*using System.Collections;
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
    }*/


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
            // 기존 Update 내용은 지우지 않습니다.
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
            while (true)
            {
                // x축 +30 방향으로 5초동안 이동
                float elapsedTime = 0f;
                while (elapsedTime < 5f)
                {
                    velocity = Vector3.left * (30f / 5f); // 5초 동안 +30 이동
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                // 3초 동안 정지
                velocity = Vector3.zero;
                yield return new WaitForSeconds(3f);

                // x축 -30 방향으로 5초동안 이동
                elapsedTime = 0f;
                while (elapsedTime < 5f)
                {
                    velocity = Vector3.right * (30f / 5f); // 5초 동안 -30 이동
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                // 3초 동안 정지
                velocity = Vector3.zero;
                yield return new WaitForSeconds(3f);
            }
        }
    }
}
