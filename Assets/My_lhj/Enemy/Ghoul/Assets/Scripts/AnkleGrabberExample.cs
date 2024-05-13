/*using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnkleGrabberExample : MonoBehaviour {
	private Animator anim;
	int IdleOne;
	int IdleAlert;
	int Sleeps;
	int AngryReaction;
	int Hit;
	int AnkleBite;
	int CrochBite;
	int Dies;
	int HushLittleBaby;
	int Run;

    [SerializeField]
    private int subjectMaxHP = 3;
    public int subjectCurrentHP = 0;
    private Animator animator;

    private UnityEngine.AI.NavMeshAgent navAgent;

    *//*public SubjectHand subjectHand;  //공격 매체*//*
    public int damage = 1;  //플레이어에게 주는 damage

    private CapsuleCollider SubjectCollider; // 콜라이더 선언
    //public bool isDead;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        AnkleGrabberCollider = GetComponent<CapsuleCollider>(); // 콜라이더 초기화
        AnkleGrabberCurrentHP = AnkleGrabberMaxHP; // 최대 HP로 현재 HP 초기화

        //subjectHand.damage = damage; //플레이어에게 손으로 공격
    }

    *//*public void TakeDamage(int damageAmount)
    {
        AnkleGrabberCurrentHP -= damageAmount;

        if (AnkleGrabberCurrentHP <= 0)
        {
            int randomValue = Random.Range(0, 2);  //0 or 1 랜덤으로 사망 모션 재생

            if (randomValue == 0)
            {
                animator.SetTrigger("DIE1");
            }
            else
            {
                animator.SetTrigger("DIE2");
            }
            SubjectCollider.enabled = false;  // 콜라이더 비활성화
            isDead = true;

            //Dead Sound
            SoundManager.instance.SubjectChannel.PlayOneShot(SoundManager.instance.SubjectDeath);
        }
        else  // Hit 애니메이션
        {
            animator.SetTrigger("DAMAGE");

            //Hurt Sound
            SoundManager.instance.SubjectChannel.PlayOneShot(SoundManager.instance.SubjectHurt);
        }
    }*//*


}

// Use this for initialization
*//* void Start () {
     anim = GetComponent<Animator> ();
     IdleOne = Animator.StringToHash("IdleOne");
     IdleAlert = Animator.StringToHash("IdleAlert");
     Sleeps = Animator.StringToHash("Sleeps");
     AngryReaction = Animator.StringToHash("AngryReaction");
     Hit = Animator.StringToHash("Hit");
     AnkleBite = Animator.StringToHash("AnkleBite");
     CrochBite = Animator.StringToHash("CrochBite");
     Dies = Animator.StringToHash("Dies");
     HushLittleBaby = Animator.StringToHash("HushLittleBaby");
     Hit = Animator.StringToHash("Hit");
     Run = Animator.StringToHash("Run");


 }

 // Update is called once per frame
 void Update () {
          if    (Input.GetKeyDown(KeyCode.Y)) {
         if(anim.GetCurrentAnimatorStateInfo(0).IsName("IdleOne")) {  
             anim.SetBool (IdleAlert, true); 
             anim.SetBool (IdleOne, false);                                      
             anim.SetBool (Sleeps, false);
             anim.SetBool (AngryReaction, false);
             anim.SetBool (Hit, false);  
             anim.SetBool (AnkleBite, false);  
             anim.SetBool (CrochBite, false); 
             anim.SetBool (Dies, false);
             anim.SetBool (HushLittleBaby, false);
             anim.SetBool (Run, false);  
         }
     } else if (Input.GetKeyUp(KeyCode.Y)) {
         if (anim.GetCurrentAnimatorStateInfo (0).IsName ("IdleAlert")) {            //y to alert
             anim.SetBool (IdleAlert, false); 
             anim.SetBool (IdleOne, true);                                      
             anim.SetBool (Sleeps, false);
             anim.SetBool (AngryReaction, false);
             anim.SetBool (Hit, false);  
             anim.SetBool (AnkleBite, false);  
             anim.SetBool (CrochBite, false); 
             anim.SetBool (Dies, false);
             anim.SetBool (HushLittleBaby, false);
             anim.SetBool (Run, false);  
         }
     } else if (Input.GetKeyDown(KeyCode.T)) {
         if (anim.GetCurrentAnimatorStateInfo (0).IsName ("IdleOne")) {
             anim.SetBool (IdleAlert, false); 
             anim.SetBool (IdleOne, false);                                      
             anim.SetBool (Sleeps, true);
             anim.SetBool (AngryReaction, false);
             anim.SetBool (Hit, false);  
             anim.SetBool (AnkleBite, false);  
             anim.SetBool (CrochBite, false); 
             anim.SetBool (Dies, false);
             anim.SetBool (HushLittleBaby, false);
             anim.SetBool (Run, false);  
         }
     } else if (Input.GetKeyUp(KeyCode.T)) {
         if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Sleeps")) {          //T to Sleeps
             anim.SetBool (IdleAlert, false); 
             anim.SetBool (IdleOne, true);                                      
             anim.SetBool (Sleeps, false);
             anim.SetBool (AngryReaction, false);
             anim.SetBool (Hit, false);  
             anim.SetBool (AnkleBite, false);  
             anim.SetBool (CrochBite, false); 
             anim.SetBool (Dies, false);
             anim.SetBool (HushLittleBaby, false);
             anim.SetBool (Run, false);  
         }
     } else if (Input.GetKeyDown(KeyCode.E)) {
         if (anim.GetCurrentAnimatorStateInfo (0).IsName ("IdleOne")) {
             anim.SetBool (IdleAlert, false); 
             anim.SetBool (IdleOne, false);                                      
             anim.SetBool (Sleeps, false);
             anim.SetBool (AngryReaction, true);
             anim.SetBool (Hit, false);  
             anim.SetBool (AnkleBite, false);  
             anim.SetBool (CrochBite, false); 
             anim.SetBool (Dies, false);;
             anim.SetBool (HushLittleBaby, false);
             anim.SetBool (Run, false);  
         }
     } else if (Input.GetKeyUp(KeyCode.E)) {
         if (anim.GetCurrentAnimatorStateInfo (0).IsName ("AngryReaction")) {            //E to angry react
             anim.SetBool (IdleAlert, false); 
             anim.SetBool (IdleOne, true);                                      
             anim.SetBool (Sleeps, false);
             anim.SetBool (AngryReaction, false);
             anim.SetBool (Hit, false);  
             anim.SetBool (AnkleBite, false);  
             anim.SetBool (CrochBite, false); 
             anim.SetBool (Dies, false);
             anim.SetBool (HushLittleBaby, false);
             anim.SetBool (Run, false);  
         }
     } else if (Input.GetKeyDown(KeyCode.U)) {
         if (anim.GetCurrentAnimatorStateInfo (0).IsName ("IdleOne")) {
             anim.SetBool (IdleAlert, false); 
             anim.SetBool (IdleOne, false);                                      
             anim.SetBool (Sleeps, false);
             anim.SetBool (AngryReaction, false);
             anim.SetBool (Hit, true);  
             anim.SetBool (AnkleBite, false);  
             anim.SetBool (CrochBite, false); 
             anim.SetBool (Dies, false);
             anim.SetBool (HushLittleBaby, false);
             anim.SetBool (Run, false);  
         }
     } else if (Input.GetKeyUp(KeyCode.U)) {
         if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Hit")) {                //u to hit
             anim.SetBool (IdleAlert, false); 
             anim.SetBool (IdleOne, true);                                      
             anim.SetBool (Sleeps, false);
             anim.SetBool (AngryReaction, false);
             anim.SetBool (Hit, false);  
             anim.SetBool (AnkleBite, false);  
             anim.SetBool (CrochBite, false); 
             anim.SetBool (Dies, false);
             anim.SetBool (HushLittleBaby, false);
             anim.SetBool (Run, false);  
         }
     } else if (Input.GetKeyDown(KeyCode.Q)) {
         if (anim.GetCurrentAnimatorStateInfo (0).IsName ("IdleOne")) {
             anim.SetBool (IdleAlert, false); 
             anim.SetBool (IdleOne, false);                                      
             anim.SetBool (Sleeps, false);
             anim.SetBool (AngryReaction, false);
             anim.SetBool (Hit, false);  
             anim.SetBool (AnkleBite, true);  
             anim.SetBool (CrochBite, false); 
             anim.SetBool (Dies, false);
             anim.SetBool (HushLittleBaby, false);
             anim.SetBool (Run, false);  
         }
     } else if (Input.GetKeyUp(KeyCode.Q)) {
         if (anim.GetCurrentAnimatorStateInfo (0).IsName ("AnkleBite")) {              //Q to ankle bite
             anim.SetBool (IdleAlert, false); 
             anim.SetBool (IdleOne, true);                                      
             anim.SetBool (Sleeps, false);
             anim.SetBool (AngryReaction, false);
             anim.SetBool (Hit, false);  
             anim.SetBool (AnkleBite, false);  
             anim.SetBool (CrochBite, false); 
             anim.SetBool (Dies, false);
             anim.SetBool (HushLittleBaby, false);
             anim.SetBool (Run, false);  
         }
     } else if (Input.GetKeyDown(KeyCode.W)) {
         if (anim.GetCurrentAnimatorStateInfo (0).IsName ("IdleOne")) {
             anim.SetBool (IdleAlert, false); 
             anim.SetBool (IdleOne, false);                                      
             anim.SetBool (Sleeps, false);
             anim.SetBool (AngryReaction, false);
             anim.SetBool (Hit, false);  
             anim.SetBool (AnkleBite, false);  
             anim.SetBool (CrochBite, true); 
             anim.SetBool (Dies, false);
             anim.SetBool (HushLittleBaby, false);
             anim.SetBool (Run, false);  
         }
     } else if (Input.GetKeyUp(KeyCode.W)) {
         if (anim.GetCurrentAnimatorStateInfo (0).IsName ("CrochBite")) {          //W to croch bite
             anim.SetBool (IdleAlert, false); 
             anim.SetBool (IdleOne, true);                                      
             anim.SetBool (Sleeps, false);
             anim.SetBool (AngryReaction, false);
             anim.SetBool (Hit, false);  
             anim.SetBool (AnkleBite, false);  
             anim.SetBool (CrochBite, false); 
             anim.SetBool (Dies, false);
             anim.SetBool (HushLittleBaby, false);
             anim.SetBool (Run, false);  
         }
     } else if (Input.GetKeyDown(KeyCode.O)) {
         if (anim.GetCurrentAnimatorStateInfo (0).IsName ("IdleOne")) {                        
             anim.SetBool (IdleAlert, false); 
             anim.SetBool (IdleOne, false);                                      
             anim.SetBool (Sleeps, false);
             anim.SetBool (AngryReaction, false);
             anim.SetBool (Hit, false);  
             anim.SetBool (AnkleBite, false);  
             anim.SetBool (CrochBite, false); 
             anim.SetBool (Dies, true);
             anim.SetBool (HushLittleBaby, false);
             anim.SetBool (Run, false);  
         }
     } else if (Input.GetKeyUp(KeyCode.O)) {
         if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Dies")) {                        //O to simple die
             anim.SetBool (IdleAlert, false); 
             anim.SetBool (IdleOne, true);                                      
             anim.SetBool (Sleeps, false);
             anim.SetBool (AngryReaction, false);
             anim.SetBool (Hit, false);  
             anim.SetBool (AnkleBite, false);  
             anim.SetBool (CrochBite, false); 
             anim.SetBool (Dies, false);
             anim.SetBool (HushLittleBaby, false);
             anim.SetBool (Run, false);  
     }
     } else if (Input.GetKeyDown(KeyCode.I)) {
         if (anim.GetCurrentAnimatorStateInfo (0).IsName ("IdleOne")) {                        
             anim.SetBool (IdleAlert, false); 
             anim.SetBool (IdleOne, false);                                      
             anim.SetBool (Sleeps, false);
             anim.SetBool (AngryReaction, false);
             anim.SetBool (Hit, false);  
             anim.SetBool (AnkleBite, false);  
             anim.SetBool (CrochBite, false); 
             anim.SetBool (Dies, false);
             anim.SetBool (HushLittleBaby, true);
             anim.SetBool (Run, false);  
         }
     } else if (Input.GetKeyUp(KeyCode.I)) {
         if (anim.GetCurrentAnimatorStateInfo (0).IsName ("HushLittleBaby")) {                        //I to hush
             anim.SetBool (IdleAlert, false); 
             anim.SetBool (IdleOne, true);                                      
             anim.SetBool (Sleeps, false);
             anim.SetBool (AngryReaction, false);
             anim.SetBool (Hit, false);  
             anim.SetBool (AnkleBite, false);  
             anim.SetBool (CrochBite, false); 
             anim.SetBool (Dies, false);
             anim.SetBool (HushLittleBaby, false);
             anim.SetBool (Run, false); 
         }
     } else if (Input.GetKeyDown(KeyCode.R)) {
         if (anim.GetCurrentAnimatorStateInfo (0).IsName ("IdleOne")) {                        
             anim.SetBool (IdleAlert, false); 
             anim.SetBool (IdleOne, false);                                      
             anim.SetBool (Sleeps, false);
             anim.SetBool (AngryReaction, false);
             anim.SetBool (Hit, false);  
             anim.SetBool (AnkleBite, false);  
             anim.SetBool (CrochBite, false); 
             anim.SetBool (Dies, false);
             anim.SetBool (HushLittleBaby, false);
             anim.SetBool (Run, true); 
         }
     } else if (Input.GetKeyUp(KeyCode.R)) {
         if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Run")) {                        //R to Run
             anim.SetBool (IdleAlert, false); 
             anim.SetBool (IdleOne, true);                                      
             anim.SetBool (Sleeps, false);
             anim.SetBool (AngryReaction, false);
             anim.SetBool (Hit, false);  
             anim.SetBool (AnkleBite, false);  
             anim.SetBool (CrochBite, false); 
             anim.SetBool (Dies, false);
             anim.SetBool (HushLittleBaby, false);
             anim.SetBool (Run, false);  
         }

      } 
 }
}*/