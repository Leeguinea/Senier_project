using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; set; }

    [Header("Subject Sound")]
    public AudioClip SubjectWalking;
    public AudioClip SubjectChase;
    public AudioClip SubjectAttack;
    public AudioClip SubjectHurt;
    public AudioClip SubjectDeath;

    public AudioSource SubjectChannel;

    private void Awake()
    {
        if (instance != null & instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
}
