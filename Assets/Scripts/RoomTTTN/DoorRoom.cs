using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRoom : MonoBehaviour
{
    private Animator animator;
    private AudioManager audioManager;

    public void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        animator = GetComponent<Animator>();
    }

  

    [ContextMenu("Open")]
    public void Open()
    {
        audioManager.PlaySFX(audioManager.doorOpen);
        animator.SetTrigger("Open");
    }

    [ContextMenu("Close")]
    public void Close() 
    {
        audioManager.PlaySFX(audioManager.doorClose);
        animator.SetTrigger("Close");
    }
}
