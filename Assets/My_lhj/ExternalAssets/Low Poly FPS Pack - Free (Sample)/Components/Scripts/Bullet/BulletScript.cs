using UnityEngine;
using System.Collections;

// ----- Low Poly FPS Pack Free Version -----
public class BulletScript : MonoBehaviour {

	[Range(5, 100)]
	[Tooltip("After how long time should the bullet prefab be destroyed?")]
	public float destroyAfter;
	[Tooltip("If enabled the bullet destroys on impact")]
	public bool destroyOnImpact = false;
	[Tooltip("Minimum time after impact that the bullet is destroyed")]
	public float minDestroyTime;
	[Tooltip("Maximum time after impact that the bullet is destroyed")]
	public float maxDestroyTime;

	[Header("Impact Effect Prefabs")]
	public Transform [] metalImpactPrefabs;

	private void Start () 
	{
		//Start destroy timer
		StartCoroutine (DestroyAfter ());
	}

	//If the bullet collides with anything
	private void OnCollisionEnter (Collision collision) 
	{
		//If destroy on impact is false, start 
		//coroutine with random destroy timer
		if (!destroyOnImpact) 
		{
			StartCoroutine (DestroyTimer ());
		}
		//Otherwise, destroy bullet on impact
		else 
		{
			Destroy (gameObject);
		}

		////////////////////// here!!/////////////////////////////////////////////////////////
		//If bullet collides with "Metal" tag
		if (collision.transform.tag == "Metal") 
		{
			//Instantiate random impact prefab from array
			Instantiate (metalImpactPrefabs [Random.Range 
				(0, metalImpactPrefabs.Length)], transform.position, 
				Quaternion.LookRotation (collision.contacts [0].normal));
			//Destroy bullet object
			Destroy(gameObject);
		}

        //If bullet collides with "Target" tag
        /*if (collision.transform.tag == "Target") 
		{
			//Toggle "isHit" on target object
			collision.transform.gameObject.GetComponent
				<TargetScript>().isHit = true;
			//Destroy bullet object
			Destroy(gameObject);
		}*/

        //If bullet collides with "" tag
        if (collision.transform.tag == "Subject")
        {
            // 데미지를 주는 코드 추가
            int damage = 1; // 이 데미지 값을 조절하여 타겟에게 줄 데미지를 결정
            collision.transform.gameObject.GetComponent<Subject>().TakeDamage(damage);

            //Destroy bullet object
            Destroy(gameObject);
        }


        if (collision.transform.tag == "Boss") // 보스의 몸통
        {
            Debug.Log("피격");
            collision.transform.gameObject.GetComponent<BossEnemy>().TakeDamage(1); // 1 데미지

            Destroy(gameObject);
        }
        if (collision.transform.tag == "BossHead") // 보스의 머리
        {
            Debug.Log("피격");
            collision.transform.gameObject.GetComponent<BossEnemy>().TakeDamage(10); // 10 데미지
            collision.transform.gameObject.GetComponent<BossEnemy>().headShotCnt += 1; // 헤드샷 카운트
            Debug.Log("헤드샷 카운트 : " + collision.transform.gameObject.GetComponent<BossEnemy>().headShotCnt);

            Destroy(gameObject);
        }



        //If bullet collides with "ExplosiveBarrel" tag
        if (collision.transform.tag == "ExplosiveBarrel") 
		{
			//Toggle "explode" on explosive barrel object
			collision.transform.gameObject.GetComponent
				<ExplosiveBarrelScript>().explode = true;
			//Destroy bullet object
			Destroy(gameObject);
		}
	}

	private IEnumerator DestroyTimer () 
	{
		//Wait random time based on min and max values
		yield return new WaitForSeconds
			(Random.Range(minDestroyTime, maxDestroyTime));
		//Destroy bullet object
		Destroy(gameObject);
	}

	private IEnumerator DestroyAfter () 
	{
		//Wait for set amount of time
		yield return new WaitForSeconds (destroyAfter);
		//Destroy bullet object
		Destroy (gameObject);
	}
}
