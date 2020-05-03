using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState instance; // 현재 클레스를 인스턴트화


    public float radiation; // 현재 피폭 수치
    public float max_radiation; // 전체 피폭 수치
    public int radiation_level; // 현재 피폭 레벨

    public float hp; // 현재 HP
    public float max_hp; // 전체 HP

    bool die_check = false; // 죽으면 true 


    public Animator animator;

    public int state_move = 0; // 0 정지 1 이동
    public int state_dash = 0; // 0 안함 1 대쉬
    public int state_sky = 0; // 0 공중 1초이전 1 공중 1초이후

    public int state_fly = 0; // 0 안공중 1 공중
    public float fly_time = 0; // 공중시간
    public bool dont_fly = false; // 메달리는 동안 날지않는 판정

    public float idle_time = 0; // 암것도 안하는 시간

    bool climb_updown_check = false;

    public static PlayerState Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
    }

    /*
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

    IEnumerator Start()
    {
        while (true)
        {
            
            yield return new WaitForSeconds(10f);
        }
    }
    */

    void Update()
    {
        if (hp <= 0 && die_check == false)
        {
            die_check = true;
            animator.SetTrigger("die");
        }

        animator.SetInteger("move", state_move);
        animator.SetInteger("sky", state_sky);

        if (state_fly == 1 && dont_fly == false)
            fly_time += Time.deltaTime;
        else
            fly_time = 0;

        if (fly_time >= 0.8)
            state_sky = 1;

        if (idle_time >= 10)
        {
            animator.SetTrigger("idle");
            idle_time -= 10;
        }

        if (state_move != 0 || state_fly != 0)
            idle_time = 0;
        else
            idle_time += Time.deltaTime;

        /*
        int state_set = 0;
        if (state_move == 1)
            state_set += 1;
        if (state_dash == 1)
            state_set += 2;
            */

        float climb_blend = 0;

        if (climb_updown_check)
            climb_blend += 0.5f;

        animator.SetFloat("climb_Blend", climb_blend);
    }

    public void dash_on()
    {
        animator.SetBool("run", true);
        state_dash = 1;

        //state = State.move_state;
    }

    public void dash_off()
    {
        animator.SetBool("run", false);
        state_dash = 0;

        //state = State.dash_state;
    }

    public void jump()
    {
        animator.SetTrigger("jump");
        idle_time = 0;
    }

    public void landing()
    {
        if(state_sky == 1)
        {
            state_sky = 0;
            animator.SetTrigger("lending");
        }

        state_fly = 0;
    }

    public void flying()
    {
        state_fly = 1;
    }

    public void shoot_ready()
    {
        animator.SetTrigger("shoot_ready");
    }

    public void shoot()
    {
        animator.SetTrigger("shoot");
    }

    public void climb_on()
    {
        animator.SetTrigger("climb_on");
        dont_fly = true;
    }
    public void climb_off()
    {
        animator.SetTrigger("climb_off");
        dont_fly = false;
    }

    public void climb_updown_on()
    {
        climb_updown_check = true;
    }
    public void climb_updown_off()
    {
        climb_updown_check = false;
    }
}
