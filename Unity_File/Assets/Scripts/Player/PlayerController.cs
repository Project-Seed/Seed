using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform player_transform;
    // 플레이어의 위치정보를 가져옴   
    public Transform Player_transform { get => player_transform; }
    public GameObject inventory; // 인벤토리
    public GameObject composer; // 합성창
    public GameObject note; // 다이어리
    private Transform child; // 모델 Transform.
    ThrowManager throwManager;
    Transform main_cam;
    public Camera cameras;
    public Inven_quick qick;

    public float player_speed = 2.0f;         // 캐릭터 걷는 속도
    public float player_run_speed = 6.0f;     // 캐릭터 달리는 속도
    public float player_jump_power = 10.0f;    // 캐릭터 점프력

    private Rigidbody player_rigidbody;
    private PlayerState player_state;
    private float input_horizontal;         // 수직방향 입력 ws
    private float input_vertical;           // 수평방향 입력 ad

    private bool is_jumping;                // 점프키를 입력하면 true.
    private bool in_ground;                 // 땅에 있으면 true.
    private bool is_run;                    // 달리고 있는지.

    public bool throw_mode = false;                 // 던지기 모드 (임시변수)
    private Vector3 lookAt;

    public bool climb_crash = false; // 갈색 충돌시 true
    public bool climb_mod = false; // 갈색 충돌시 키 누르면 true
    public Vector3 climb_po; // 충돌후 갈색 위치, 오르기 제한 범위때매
    public Quaternion climb_ro; // 충돌후 갈색 각도, r 누를때 정면 바라보기 위해

    public bool hang_crash = false; // 파랑 충돌시 true
    public bool hang_mod = false; // 파랑 충돌시 키 누르면 true

    bool eat_bool = false; // 먹기면 true
    string eat_item;
    GameObject eat_object;


    IEnumerator StopJumping()                  // 이단 점프를 막기 위해 점프시 1초간 점프금지
    {
        is_jumping = false;
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator hang_up()                  // 이단 점프를 막기 위해 점프시 1초간 점프금지
    {
        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < 45; i++)
        {
            gameObject.transform.Translate(0, 0.025f, 0);
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(1.6f);

        for (int i = 0; i < 30; i++)
        {
            gameObject.transform.Translate(0, 0.025f, 0);
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(0.2f);

        hang_mod = false;
    }

    void Start()
    {
        player_rigidbody = GetComponent<Rigidbody>();
        player_transform = GetComponent<Transform>(); //나중에 제거. 그냥 transform으로 쓰기
        player_state = GetComponent<PlayerState>();
        throwManager = GetComponent<ThrowManager>();
        main_cam = Camera.main.transform;
        child = transform.GetChild(0);

        is_jumping = false;
        is_run = false;
    }

    private void Update()                               // 키 입력은 Update에서 받고
    {
        if (InputManager.instance.click_mod == 0)
        {

            if (Input.GetButtonDown("Jump"))
            {
                if (!is_jumping && player_state.lending_time == false)
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

                    transform.rotation = climb_ro;
                    transform.rotation = Quaternion.Euler(new Vector3(-transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 180, transform.rotation.eulerAngles.z));
                    transform.Translate(0, 0, 0.8f);
                }

                if (hang_mod == true)
                {
                    hang_mod = false;
                    player_state.hang_off();
                }
                else if (hang_crash == true && hang_mod == false)
                {
                    hang_mod = true;
                    player_state.hang_on();
                }
            }

            if (Input.GetKeyDown(KeyCode.T))
            { 
                if(hang_mod == true)
                {
                    StartCoroutine(hang_up());
                    player_state.hang_up();
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


                if (Input.GetKey(KeyCode.W))
                    gameObject.transform.Translate(0, Time.deltaTime, 0);
                if (Input.GetKey(KeyCode.S))
                    gameObject.transform.Translate(0, -Time.deltaTime, 0);
                if (Input.GetKey(KeyCode.D))
                    gameObject.transform.Translate(Time.deltaTime, 0, 0);
                if (Input.GetKey(KeyCode.A))
                    gameObject.transform.Translate(-Time.deltaTime, 0, 0);

                if(climb_crash == false) // 떨어진다!
                {
                    climb_mod = false;
                    player_state.climb_off();
                }
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
                if (GameSystem.instance.item_search(qick.item_name, "category") == "seed") // 씨앗 타입이어야지만 던져짐
                {
                    if (GameSystem.instance.item_num[qick.item_name] >= 1 && InputManager.instance.click_mod == 0)
                    {
                        throw_mode = true;
                        //child.localRotation = Quaternion.Slerp(child.localRotation, transform.localRotation, 0.5f);

                        throwManager.mouse_down(qick.item_name);

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

                    player_state.shoot_stop();
                }
            }

            //좌클릭 Up 발사.
            if (Input.GetMouseButtonUp(0))
            {
                if (throw_mode) //취소를 안했을 경우에만 발사
                {
                    throw_mode = false;
                    GameSystem.instance.item_num[qick.item_name]--;
                    throwManager.mouse_up(true);

                    player_state.shoot();
                }
            }

            input_horizontal = Input.GetAxis("Horizontal");
            if (input_horizontal < 0)
            {
                player_state.left_check = true;
                player_state.left_check = false;
            }
            else if (input_horizontal > 0)
            {
                player_state.left_check = false;
                player_state.right_check = true;
            }
            else
            {
                player_state.left_check = false;
                player_state.left_check = false;
            }

            input_vertical = Input.GetAxis("Vertical");
            if (input_vertical != 0)
                player_state.updown_check = true;
            else
                player_state.updown_check = false;

            //카메라 움직임과 연동
            Quaternion dir = main_cam.localRotation;
            dir.x = 0f; dir.z = 0f;

            if (hang_mod == false)
                transform.localRotation = Quaternion.Slerp(transform.localRotation, dir, 0.5f);

            //발사모드에서는 캐릭터 회전 안함. 앞만봄
            if (throw_mode)
                child.localRotation = Quaternion.Slerp(child.localRotation, transform.localRotation, 0.2f);

            //이동할때만 모델회전
            else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
                     Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                transform.localRotation = dir;

                if (climb_mod == false && hang_mod == false)
                    child.localRotation = Quaternion.Slerp(child.localRotation, Quaternion.LookRotation(lookAt), 0.2f);
            }



            // 아이템 먹기
            if (Input.GetKeyDown(KeyCode.E) && eat_item != "")
            {
                Eat_system.instance.eat_item(eat_item);
                eat_item = "";

                eat_object.GetComponent<ExampleItem>().eat();

                eat_bool = false;
                Key_guide.instance.item_off();
                Key_guide.instance.item_name_off();
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventory.activeSelf == true)
            {
                inventory.SetActive(false);
            }
            else
            {
                composer.SetActive(false);
                note.SetActive(false);
                inventory.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            if (composer.activeSelf == true)
                composer.SetActive(false);
            else
            {
                inventory.SetActive(false);
                note.SetActive(false);
                composer.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (note.activeSelf == true)
                note.SetActive(false);
            else
            {
                inventory.SetActive(false);
                composer.SetActive(false);
                note.SetActive(true);
            }
        }


        if(climb_mod == true || hang_mod == true)
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

    private void FixedUpdate()
    {
        if (InputManager.instance.click_mod == 0)
            Moving();
        else
            player_state.state_move = 0;
    }

    private void Moving()
    {
        Vector3 movement = transform.forward * input_vertical + transform.right * input_horizontal;
        movement = movement.normalized;

        //문제점. 대각선이동시에는? 방향을 정해주는게 아니라(look at=) 곱해줘야함. . .
        if (climb_mod == false && hang_mod == false && player_state.lending_time == false)
        {
            //if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 1.0f))
            //{
            //    Debug.DrawRay(transform.position, transform.forward * 0.5f, Color.blue, 1.0f);

            //    if (!hit.transform.CompareTag("Player") && Vector3.Distance(hit.point,transform.position) < 0.1f)
            //        Debug.Log("STOP" + hit.distance);
            //}

           transform.Translate(movement * (is_run ? player_run_speed : player_speed) * Time.deltaTime, Space.Self);
        }
        if ((Mathf.Abs(movement.z) + Mathf.Abs(movement.x)) >= 1 && climb_mod == false && hang_mod == false)
            player_state.state_move = 1;
        else
            player_state.state_move = 0;
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
        if (collision.gameObject.CompareTag("Ground")||collision.gameObject.CompareTag("Plantable"))
        {
            in_ground = true;
            is_jumping = false;
            Debug.Log("in Ground");

            player_state.landing();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Plantable"))
        {
            in_ground = false;
            Debug.Log("not in Ground");

            player_state.flying();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "brown_trigger")
        {
            Debug.Log("갈색 충돌");
            climb_ro = collision.transform.rotation;
            climb_po = collision.transform.position;
            climb_crash = true;

            Key_guide.instance.climb_on();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        IItem item = collision.GetComponent<IItem>(); //IItem을 상속받는 모든 아이템들
        if (item != null)                         //아이템과 부딪혔다면 함수를 호출하고 지움.
        {
            Key_guide.instance.item_off();
            Key_guide.instance.item_name_off();
        }

        if (collision.gameObject.name == "brown_trigger")
        {
            Debug.Log("갈색 떨어짐");
            climb_crash = false;

            Key_guide.instance.climb_off();
        }
    }


    private void OnTriggerStay(Collider collision)
    {
        IItem item = collision.GetComponent<IItem>(); //IItem을 상속받는 모든 아이템들
        if (item != null)                         //아이템과 부딪혔다면 함수를 호출하고 지움.
        {
            eat_bool = true;
            eat_item = collision.name;
            eat_object = collision.gameObject;
            Key_guide.instance.item_on();
            Vector3 eat_pos = cameras.WorldToScreenPoint(eat_object.transform.position);
            string eat_item2 = GameSystem.instance.item_search(eat_item, "name_ko");
            Key_guide.instance.item_name_on(eat_item2, eat_pos);
        }
    }
}
