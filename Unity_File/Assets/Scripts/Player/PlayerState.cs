using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public Animator animator;

    public int state_move = 0; // 0 정지 1 이동
    public int state_dash = 0; // 0 안함 1 대쉬
    // public int state_jump = 0; // 0 안함 1 점프


    [Flags]
    public enum State
    {
        move_state = 1,
        dash_state = 2
    }
    State state;

    public int Get_State()
    {
        return (int)state;
    }


    void Start()
    {
        
    }

    void Update()
    {
        animator.SetInteger("move", state_move);

        int state_set = 0;
        if (state_move == 1)
            state_set += 1;
        if (state_dash == 1)
            state_set += 2;
    }

    public void dash_on()
    {
        animator.SetBool("run", true);
        state_dash = 1;

        state = State.move_state;
    }

    public void dash_off()
    {
        animator.SetBool("run", false);
        state_dash = 0;

        state = State.dash_state;
    }

    public void jump()
    {
        animator.SetTrigger("jump");
    }
}
