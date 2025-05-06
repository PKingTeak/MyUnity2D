using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;
    
 private static readonly int IsOpening = Animator.StringToHash("IsOpen");

  
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();    
    }

    public void Open()
    {
        animator.SetBool(IsOpening,true);
    }
    public void DoorClose()
    {

        animator.SetBool(IsOpening,false);
        //this is not useful
    }

}
