using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerAnimator : MonoBehaviour
{
    //References
    Animator am;
    PlayerController pc;
    SpriteRenderer sr;

   void Start()
    {
        am = GetComponent<Animator>();
        pc = GetComponentInParent<PlayerController>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (pc.moveInput.x != 0 || pc.moveInput.y != 0)
        {
            Debug.Log("Move");
            am.SetBool("Move", true);
            SpriteDirectionChecker();
        }
        else
        {
            Debug.Log("Idle");
            am.SetBool("Move", false);
        }
    }

    void SpriteDirectionChecker()
    {         
        
        if (pc.moveInput.x > 0)
        {
            sr.flipX = false;
        }
        else if (pc.moveInput.x < 0)
        {
            sr.flipX = true;
        }
    }
}
