using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.Animations.Rigging;

public class PlayerManager : MonoBehaviour
{
    private StarterAssetsInputs input;
    private ThirdPersonController controller;
    private Animator anim;

    [Header("Aim")]  
    [SerializeField]
    private CinemachineVirtualCamera aimCam;
    [SerializeField]
    private GameObject aimImage;
    [SerializeField]
    private GameObject aimObj;
    [SerializeField]
    private float aimObjDis = 10f;
    [SerializeField]
    private LayerMask targetLayer;


    [Header("IK")]  //√— + º’
    [SerializeField]
    private Rig handRig;
    [SerializeField]
    private Rig aimRig;


    [Header("Weapon Sound Effect")] //√—º“∏Æ ª¿‘
    [SerializeField]
    private AudioClip shootingSound;
    [SerializeField]
    private AudioClip[] reroadSound;  
    private AudioSource weaponSound;


    private Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<StarterAssetsInputs>();
        controller = GetComponent<ThirdPersonController>();
        anim = GetComponent<Animator>();
        weaponSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        AimCheck();
    }

    private void AimCheck()
    {
       // Debug.Log("ø°¿”√º≈©Ω√¿€");
        if (input.reroad)
        {
            input.reroad = false;

            if (controller.isReroad)
            {
                //Debug.Log("ƒ¡∆Æ∑—¿Ã¡Ó∑ŒµÂ");
                return;
            }

            AimControll(false);
            SetRigWeight(0);
            anim.SetLayerWeight(1, 1);
            anim.SetTrigger("Reroad");
            controller.isReroad = true;  
            //Debug.Log("isReroad set to: " + controller.isReroad); //true∑Œ ¬Ô»˚
        }

        if (controller.isReroad)
        {
            //Debug.Log("¿Ã¡Ó∑ŒµÂ~~");
            return;
        }

        if (input.aim)
        {
            AimControll(true);

            anim.SetLayerWeight(1, 1);

            Vector3 targetPosition = Vector3.zero;
            Transform camTransform = Camera.main.transform;
            RaycastHit hit;

            if (Physics.Raycast(camTransform.position, camTransform.forward, out hit, Mathf.Infinity, targetLayer))
            {
                //Debug.Log("Name : " + hit.transform.gameObject.name);
                targetPosition = hit.point;
                aimObj.transform.position = hit.point;

                enemy = hit.collider.gameObject.GetComponent<Enemy>();
            }
            else
            {
                targetPosition = camTransform.position + camTransform.forward * aimObjDis;
                aimObj.transform.position = camTransform.position + camTransform.forward * aimObjDis;
            }

            Vector3 targetAim = targetPosition;
            targetAim.y = transform.position.y;
            Vector3 aimDir = (targetAim - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDir, Time.deltaTime * 50f);

            SetRigWeight(1);

            if (input.shoot) //√—æÀ πﬂªÁ
            {
                anim.SetBool("Shoot", true);
                GameManager.Instance.Shooting(targetPosition, enemy, weaponSound, shootingSound);
            }
            else
            {
                anim.SetBool("Shoot", false);
            }

        }
        else
        {
            AimControll(false);
            SetRigWeight(0);
            anim.SetLayerWeight(1, 0);
            anim.SetBool("Shoot", false);
        }
    }

    private void AimControll(bool isCheck)
    {
        aimCam.gameObject.SetActive(isCheck);
        aimImage.SetActive(isCheck);
        controller.isAimMove = isCheck;
    }

    public void Reroad()
    {
        //Debug.Log("Reroad");
        controller.isReroad = false;
        SetRigWeight(1);
        anim.SetLayerWeight(1, 0);
        PlayWeaponSound(reroadSound[2]);
    }

    private void SetRigWeight(float weight)
    {
        aimRig.weight = weight;
        handRig.weight = weight;
    }

    public void ReroadWeaponClip() 
    { 
        //Debug.Log("Reroad0_1");
        GameManager.Instance.ReroadClip();
        //Debug.Log("Reroad0_2");
        PlayWeaponSound(reroadSound[0]);
       // Debug.Log("Reroad0_3");
    }

    public void ReroadInsertClip()
    {
        //Debug.Log("Reroad1_1");
        PlayWeaponSound(reroadSound[1]);
        //Debug.Log("Reroad_1_2");
    }

    private void PlayWeaponSound(AudioClip sound)
    {
        weaponSound.clip = sound;
        weaponSound.Play();
    }
}
