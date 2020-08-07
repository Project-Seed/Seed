using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState instance; // 현재 클레스를 인스턴트화


    public int radiation; // 현재 피폭 수치
    public int max_radiation; // 전체 피폭 수치
    public int radiation_level; // 현재 피폭 레벨

    public int hp; // 현재 HP
    public int max_hp; // 전체 HP

    bool die_check = false; // 죽으면 true 


    public Animator animator;

    public Transform target;
    Transform chest;


    public int state_move = 0; // 0 정지 1 이동
    public int state_dash = 0; // 0 안함 1 대쉬

    public int state_sky = 0; // 0 공중 1 높이 차이
    public int state_fly = 0; // 0 안공중 1 공중
    public float fly_y; // 공중 y좌표
    public bool dont_fly = false; // 메달리는 동안 날지않는 판정

    public float idle_time = 0; // 암것도 안하는 시간

    public bool updown_check = false;
    public bool left_check = false;
    public bool right_check = false;
    public bool shoot_check = false; // 사격 모드시 트루
    public bool shoot_check2 = false; // 사격 모드시 트루

    public GameObject gun;

    public bool lending_time = false; // true면 0.5초간 착지후 발이 아파 못움직임

    public GameObject game_overs;


    public static PlayerState Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;

        chest = animator.GetBoneTransform(HumanBodyBones.RightUpperArm); // 상체값 가져오기
    }

    void Update()
    {
        if (hp <= 0 && die_check == false)
        {
            InputManager.instance.game_stop();
            die_check = true;
            animator.SetTrigger("die");

            StartCoroutine(gameover_image());
        }

        if (state_fly == 1 && dont_fly == false)
        {
            if (fly_y - 4f > gameObject.transform.position.y && state_sky == 0)
            {
                animator.SetTrigger("sky_ing");
                state_sky = 1;
            }
        }


        if (idle_time >= 10)
        {
            animator.SetTrigger("idle");
            idle_time -= 10;
        }

        if (state_move != 0 || state_fly != 0)
            idle_time = 0;
        else
            idle_time += Time.deltaTime;


        float climb_blend = 0.5f;

        /*
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
        */

        animator.SetFloat("climb_Blend", climb_blend);
        animator.SetInteger("move", state_move);
    }

    private void LateUpdate()
    {
        if (shoot_check)
        {
            chest.LookAt(target.position);
            chest.rotation = chest.rotation * Quaternion.Euler(180, 90 + 180+10, 10);
        }
    }

    IEnumerator gameover_image()
    {
        yield return new WaitForSeconds(4f);
        game_overs.SetActive(true);
        game_overs.GetComponent<Game_over>().image_on();
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
        if(state_sky == 1 && state_fly == 1)
        {
            state_sky = 0;
            animator.SetTrigger("lending");
            lending_time = true;
            StartCoroutine("lending_coroutine");


            int reduce_num = (int)(fly_y - gameObject.transform.position.y) / 4;
            State.instance.hp_down(reduce_num);
        }
        state_fly = 0; 
    }

    IEnumerator lending_coroutine()
    {
        yield return new WaitForSeconds(1f);
        animator.ResetTrigger("lending");
        animator.ResetTrigger("sky_ing");
        lending_time = false;
    }

    public void flying(float y)
    {
        fly_y = y;
        state_fly = 1;
    }

    public void shoot_ready()
    {
        animator.SetBool("shoot_start", true);
        shoot_check2 = true;
        StartCoroutine(shoot_readys());

        gun.SetActive(true);
    }
    IEnumerator shoot_readys()
    {
        yield return new WaitForSeconds(0.8f);
        
        if(shoot_check2 == true)
            shoot_check = true;
    }

    public void shoot()
    {
        animator.SetTrigger("shoot");
        animator.SetTrigger("shoot2");
        animator.SetBool("shoot_start", false);

        shoot_check = false;
        shoot_check2 = false;

        gun.SetActive(false);
    }
    public void shoot_stop()
    {
        animator.SetBool("shoot_start", false);
        animator.SetTrigger("shoot_stop");
        animator.SetTrigger("shoot_stop2");

        shoot_check = false;
        shoot_check2 = false;

        gun.SetActive(false);
    }

    public void climb_on()
    {
        animator.ResetTrigger("climb_off");
        animator.ResetTrigger("climb_up");
        animator.SetBool("climb_finish", false);
        animator.SetTrigger("climb_on");
        dont_fly = true;
        state_sky = 0;
    }
    public void climb_off()
    {
        animator.SetTrigger("climb_off");
        dont_fly = false;
    }
    public void climb_up()
    {
        animator.SetTrigger("climb_up");
    }

    public void climb_finish()
    {
        animator.SetBool("climb_finish", true);

        gameObject.transform.position = gameObject.GetComponent<PlayerController>().climb_finish.transform.position;
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

    public void box_open()
    {
        animator.SetTrigger("box_open");
    }

    public void reborn()
    {
        animator.SetTrigger("reborn");
        die_check = false;
    }
}
