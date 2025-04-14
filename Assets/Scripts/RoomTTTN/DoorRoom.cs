using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRoom : MonoBehaviour
{
    private Animator animator;
    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

  

    [ContextMenu("Open")]
    public void Open()
    {
        animator.SetTrigger("Open");
    }

    [ContextMenu("Close")]
    public void Close() 
    {
        animator.SetTrigger("Close");
    }
}
