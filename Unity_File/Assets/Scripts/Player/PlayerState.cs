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

    public bool updown_check = false;
    public bool left_check = false;
    public bool right_check = false;
    public bool shoot_check = false; // 사격 모드시 트루

    public bool lending_time = false; // true면 0.5초간 착지후 발이 아파 못움직임


    public Transform spine; // 상체
    public Transform target; // 조준시 상체 바라볼곳
    public Vector3 ress;


    public static PlayerState Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;

        spine = animator.GetBoneTransform(HumanBodyBones.Chest); // 상체값 가져오기
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

        if (state_fly == 1 && dont_fly == false)
            fly_time += Time.deltaTime;
        else
            fly_time = 0;

        if (fly_time >= 1.2f)
            state_sky = 1;
        else
            state_sky = 0;

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

        if (updown_check && !left_check && !right_check)
            climb_blend = 0.5f;
        else if (updown_check && left_check && !right_check)
            climb_blend = 0.25f;
        else if (updown_check && !left_check && right_check)
            climb_blend = 0.75f;
        else if (!updown_check && left_check && !right_check)
            climb_blend = 0f;
        else if (!updown_check && !left_check && right_check)
            climb_blend = 1f;

        animator.SetFloat("climb_Blend", climb_blend);
        animator.SetInteger("move", state_move);
        animator.SetInteger("sky", state_sky);
    }

    /*
    private void LateUpdate()
    {
        if (shoot_check)
        {
            spine.LookAt(target.position); //플레이어의 상체부분이 타겟 위치 보기
            spine.rotation = Quaternion.Euler(ress);
        }
    }*/

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
            animator.SetInteger("sky", state_sky);
            fly_time = 0;
            animator.SetTrigger("lending");
            lending_time = true;
            StartCoroutine("lending_coroutine");
        }

        state_fly = 0;
    }

    IEnumerator lending_coroutine()
    {
        yield return new WaitForSeconds(1f);
        animator.ResetTrigger("lending");
        lending_time = false;
    }

    public void flying()
    {
        state_fly = 1;
    }

    public void shoot_ready()
    {
        shoot_check = true;
        animator.ResetTrigger("shoot_stop");
        animator.SetTrigger("shoot_ready");
    }

    public void shoot()
    {
        shoot_check = false;
        animator.SetTrigger("shoot");
    }
    public void shoot_stop()
    {
        shoot_check = false;
        animator.SetTrigger("shoot_stop");
    }

    public void climb_on()
    {
        animator.ResetTrigger("climb_off");
        animator.SetTrigger("climb_on");
        dont_fly = true;
        state_sky = 0;
    }
    
    public void climb_off()
    {
        animator.SetTrigger("climb_off");
        dont_fly = false;
    }

    public void hang_on()
    {
        animator.ResetTrigger("hang_ing");
        animator.SetTrigger("hang_on");
        animator.SetTrigger("hang_on2");
        dont_fly = true;
        state_sky = 0;
    }
    public void hang_ing()
    {
        animator.SetTrigger("hang_ing");
    }
    public void hang_land()
    {
        animator.SetTrigger("hang_land");
        animator.SetTrigger("hang_land2");
        dont_fly = false;
    }

    public void tutorial_start()
    {
        InputManager.instance.game_stop();
        animator.SetTrigger("lying_ing");
    }

    public void tutorial_up()
    {
        InputManager.instance.game_start();
        animator.SetTrigger("lying_end");
    }
}
