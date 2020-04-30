﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform player_transform;
    // 플레이어의 위치정보를 가져옴   
    public Transform Player_transform { get => player_transform; }

    private Rigidbody player_rigidbody;
    private PlayerState player_state;
    private float input_horizontal;         // 수직방향 입력 ws
    private float input_vertical;           // 수평방향 입력 ad
    private bool is_jumping;                // 점프키를 입력하면 true.
    private bool in_ground;                 // 땅에 있으면 true.
    private bool is_run;                    // 달리고 있는지.

    public bool throw_mode = false;                 // 던지기 모드 (임시변수)
    public float throw_position;
    private Vector3 lookAt;

    [SerializeField]
    private float player_speed = 2.0f;         // 캐릭터 걷는 속도
    private float player_run_speed = 6.0f;     // 캐릭터 달리는 속도
    private float player_jump_power = 5.0f;    // 캐릭터 점프력

    public GameObject inventory; // 인벤토리
    public GameObject composer; // 합성창
    public GameObject note; // 다이어리
    public Transform camera_rig_transform;
    private Transform child;
    private Transform aim;
    ThrowManager throwManager;

    public Qick_slot_sum qick;

    public bool climb_crash = false; // 갈색 충돌시 true
    public bool climb_mod = false; // 갈색 충돌시 키 누르면 true
    public Vector3 climb_po; // 충돌후 갈색 위치, 오르기 제한 범위때매

    IEnumerator StopJumping()                  // 이단 점프를 막기 위해 점프시 1초간 점프금지
    {
        is_jumping = false;
        yield return new WaitForSeconds(0.5f);
    }

    void Start()
    {
        player_rigidbody = GetComponent<Rigidbody>();
        player_transform = GetComponent<Transform>();
        player_state = GetComponent<PlayerState>();
        throwManager = GetComponent<ThrowManager>();
        child = transform.GetChild(0);
        aim = transform.GetChild(1);
        is_jumping = false;
        is_run = false;
    }

    private void Update()                               // 키 입력은 Update에서 받고
    {
        if (InputManager.instance.click_mod == 0)
        {

            if (Input.GetButtonDown("Jump"))
            {
                if (!is_jumping)
                    StartCoroutine(Jumping());
            }

            if(Input.GetKeyDown(KeyCode.R))
            {
                if(climb_mod == true)
                {
                    climb_mod = false;
                    player_state.climb_off();
                }
                else if(climb_crash == true && climb_mod == false)
                {
                    climb_mod = true;
                    player_state.climb_on();
                }
            }

            if (Input.GetKey(KeyCode.A))
            {
                //현재 캐릭터의 왼쪽 방향을 받아놓고 그쪽을 보도록 함.
                lookAt = -transform.right;
            }

            if (Input.GetKey(KeyCode.D))
            {
                lookAt = transform.right;
            }

            if (Input.GetKey(KeyCode.W))
            {
                lookAt = transform.forward;
            }

            if (Input.GetKey(KeyCode.S))
            {
                lookAt = -transform.forward;
            }

            if (climb_mod == true)
            {
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D))
                    player_state.animator.SetBool("climb_move", true);
                else
                    player_state.animator.SetBool("climb_move", false);


                if (Input.GetKey(KeyCode.A) && climb_po.z + 1 > gameObject.transform.position.z)
                    gameObject.transform.Translate(0,0, Time.deltaTime);
                if (Input.GetKey(KeyCode.S) && climb_po.y - 0.5f < gameObject.transform.position.y)
                    gameObject.transform.Translate(0, -Time.deltaTime, 0);
                if (Input.GetKey(KeyCode.D) && climb_po.z - 1 < gameObject.transform.position.z)
                    gameObject.transform.Translate(0, 0, -Time.deltaTime);
                if (Input.GetKey(KeyCode.W) && climb_po.y + 3.2f > gameObject.transform.position.y)
                    gameObject.transform.Translate(0, Time.deltaTime, 0);
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                player_state.dash_on();
                is_run = true;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                player_state.dash_off();
                is_run = false;
            }

            //좌클릭 타겟팅 시작.
            if (Input.GetMouseButtonDown(0))
            {
                if (GameSystem.instance.item_search(qick.item_names, "category") == "seed") // 씨앗 타입이어야지만 던져짐
                {
                    if (GameSystem.instance.item_num[qick.item_names] >= 1 && InputManager.instance.click_mod == 0)
                    {
                        throw_mode = true;
                        lookAt = transform.forward;
                        child.rotation = Quaternion.Slerp(child.rotation, Quaternion.LookRotation(lookAt), 0.5f);

                        throwManager.mouse_down(qick.item_names);

                        player_state.shoot_ready();
                    }
                }
            }

            //좌클릭하는 동안 계속 조준하면서 Raycasting.
            if (Input.GetMouseButton(0))
            {
                if (throw_mode)
                    throwManager.Targeting();
            }

            //좌클릭->우클릭 발사 취소.
            if (Input.GetMouseButtonDown(1))
            {
                if (throw_mode)
                {
                    throw_mode = false;
                    throwManager.mouse_up(false);
                }
            }

            //좌클릭 Up 발사.
            if (Input.GetMouseButtonUp(0))
            {
                if (throw_mode) //취소를 안했을 경우에만 발사
                {
                    throw_mode = false;
                    GameSystem.instance.item_num[qick.item_names]--;
                    throwManager.mouse_up(true);

                    player_state.shoot();
                }
            }

            input_horizontal = Input.GetAxis("Horizontal");
            input_vertical = Input.GetAxis("Vertical");
            
            Vector3 movement = new Vector3(input_horizontal, 0, input_vertical);
            movement = movement.normalized;

            //카메라 움직임과 연동
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
                Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || throw_mode)
            {
                Quaternion dir = camera_rig_transform.localRotation;

                aim.localRotation = Quaternion.AngleAxis(dir.eulerAngles.x, aim.right);

                dir.x = 0f; dir.z = 0f;

                if (climb_mod == false)
                    transform.localRotation = Quaternion.Slerp(transform.localRotation, dir, 0.5f);
                //child.rotation = Quaternion.LookRotation(lookAt);

                if (!throw_mode && climb_mod == false)
                    child.rotation = Quaternion.Slerp(child.rotation, Quaternion.LookRotation(lookAt), 0.2f);
            }
           
            //문제점. 대각선이동시에는? 방향을 정해주는게 아니라(look at=) 곱해줘야함. . .
            if(climb_mod == false)
                player_transform.Translate(movement * (is_run? player_run_speed : player_speed) * Time.deltaTime, Space.Self);
            
            if ((Mathf.Abs(movement.z) + Mathf.Abs(movement.x)) >= 1 && climb_mod == false)
                player_state.state_move = 1;
            else
                player_state.state_move = 0;

        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventory.activeSelf == true)
                inventory.SetActive(false);
            else if (InputManager.instance.click_mod == 0)
                inventory.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            if (composer.activeSelf == true)
                composer.SetActive(false);
            else
                composer.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (note.activeSelf == true)
                note.SetActive(false);
            else
                note.SetActive(true);
        }


        if(climb_mod == true)
        {
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
        else
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }

    }

    IEnumerator Jumping()
    {
        if (!in_ground) yield return null;

        is_jumping = true;
        player_state.jump();

        yield return new WaitForSeconds(0.2f);

        player_rigidbody.AddForce(Vector3.up * player_jump_power, ForceMode.Impulse);   //점프
    
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            in_ground = true;
            is_jumping = false;
            Debug.Log("in Ground");

            player_state.landing();
        }
        else if(collision.gameObject.name == "brown_seed(Clone)")
        { 
            climb_po = collision.transform.position;
            climb_crash = true;
            transform.rotation = collision.transform.rotation;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            in_ground = false;
            Debug.Log("not in Ground");

            player_state.flying();
        }
        else if (collision.gameObject.name == "brown_seed(Clone)")
        {
            climb_crash = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IItem item = other.GetComponent<IItem>(); //IItem을 상속받는 모든 아이템들
        if (item != null)                         //아이템과 부딪혔다면 함수를 호출하고 지움.
        {
            item.Collided();

            if (GameSystem.instance.item_num[other.name] == 0) // 못먹었던 아이템이면
                GameSystem.instance.item_time.Add(other.name);
            GameSystem.instance.item_num[other.name] += 1;

            if (Dictionarys.instance.dictionary_num[other.name] == false) // '한번도' 못먹었던 아이템이면 (도감용)
                Dictionarys.instance.dictionary_num[other.name] = true;

            Destroy(other.gameObject);
        }
    }
}
