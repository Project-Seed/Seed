using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    /* 나중에 바꿔야겠음
    [Flags]
    public enum State
    {

    }*/
    public Animator animator;

    public int state_move = 0; // 0 정지 1 앞으로 -1 뒤로
    public int state_dash = 0; // 0 안함 1 대쉬
    // public int state_jump = 0; // 0 안함 1 점프

    void Start()
    {
        
    }

    void Update()
    {
        animator.SetInteger("move", state_move);
    }

    public void dash_on()
    {
        animator.SetBool("run", true);
        state_dash = 1;
    }

    public void dash_off()
    {
        animator.SetBool("run", false);
        state_dash = 0;
    }

    public void jump()
    {
        animator.SetTrigger("jump");
    }
}
