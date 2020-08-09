using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Assertions.Must;
using UnityEngine.Experimental.XR;

public class PlayerController : MonoBehaviour
{
    public GameObject composer; // 합성창
    public GameObject note; // 다이어리
    public Quick_slot_new qick;
    public GameObject rotate_ob; // 회전하는 오브젝트
    public Dialogue dialogue;
    public Camera cameras;
    public Transform main_cam;

    private ThrowManager throwManager;
    private MapChecker mapChecker;
    private Transform child; // 모델 Transform.
    private Rigidbody player_rigidbody;
    private PlayerState player_state;
    private ParticleSystem followDust;

    public float player_speed = 6.0f;         // 캐릭터 걷는 속도
    public float player_run_speed = 9.0f;     // 캐릭터 달리는 속도
    public float player_jump_power = 10.0f;    // 캐릭터 점프력

    private float input_horizontal;         // 수직방향 입력 ws
    private float input_vertical;           // 수평방향 입력 ad
    private Vector3 lookAt;
    private Vector3 movement;

    public bool is_jumping;                // 점프키를 입력하면 true.
    private bool stop_jumping;                // 점프금지 시간
    private bool is_run;                    // 달리고 있는지.

    public bool climb_crash = false; // 갈색 충돌시 true
    public bool climb_mod = false; // 갈색 충돌시 키 누르면 true
    public GameObject climb_ob;
    public GameObject climb_point_ob;// 갈색에서 매달릴곳
    public bool climb_time = false; // 매달리면 0.5초간만트루
    public GameObject climb_head; // 상단 이동 위해서
    public GameObject climb_fornt; // 앞 이동 위해서
    public GameObject climb_finish;

    public bool hang_crash = false; // 파랑 충돌시 true
    public int hang_mod = 0; // 기본 0 매달리기 1 떨어지기 2
    public bool hang_end = false; // r연타 방지용
    public GameObject hang_ob; // 매달리는 파랑이
    public float hang_x;
    public float hang_y;
    public float hang_z;
    public int hang_vecter = 0; // 캐릭터가 어느방향 바라보게 해야하는지

    bool eat_bool = false; // 먹기면 true
    string eat_item;
    GameObject eat_object;//아이템
    GameObject eat_objects;//오브젝트

    public GameObject shadow; // 매달리기 충돌체크용
    public GameObject shadow2; // 매달리기 충돌체크용
    public bool climb_up_bool = false;

    public Material player_mate;

    public GameObject chest_ob;
    public GameObject radio_ob;

    int right_crash = 0; // 우클릭 중복 때매

    public bool tuto = false; // 튜토리얼 이면 트루

    IEnumerator StopJumping()             
    {
        stop_jumping = true;
        yield return new WaitForSeconds(0.7f);
        stop_jumping = false;
    }
    
    public IEnumerator climb_up()                  
    {
        player_state.climb_up();
        climb_up_bool = true;

        yield return new WaitForSeconds(4f);

        player_state.climb_finish();
  

        climb_up_bool = false;
        shadow_out();
    }

    IEnumerator hang_jump()
    {
        hang_mod = 1;

        if (hang_vecter == 0)
            rotate_ob.transform.eulerAngles = new Vector3(hang_ob.transform.eulerAngles.x, hang_ob.transform.eulerAngles.y + 90, hang_ob.transform.eulerAngles.z);
        else
            rotate_ob.transform.eulerAngles = new Vector3(hang_ob.transform.eulerAngles.x, hang_ob.transform.eulerAngles.y - 90, hang_ob.transform.eulerAngles.z);

        yield return new WaitForSeconds(0.3f);

        hang_x = (hang_ob.transform.position.x - gameObject.transform.position.x) / 10f;
        hang_y = (hang_ob.transform.position.y + 0.2f - gameObject.transform.position.y - 1f) / 10f;
        hang_z = (hang_ob.transform.position.z - gameObject.transform.position.z) / 10f;

        for (int i = 0; i < 10; i++)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + hang_x, gameObject.transform.position.y + hang_y, gameObject.transform.position.z + hang_z);
            yield return new WaitForSeconds(0.01f);
        }

        hang_mod = 2;
        player_state.hang_ing();
    }

    IEnumerator hang_land()
    {
        player_state.hang_land();
        yield return new WaitForSeconds(0.25f);
        hang_mod = 0;

        for (int i = 0; i < 10; i++)
        {
            gameObject.transform.Translate(rotate_ob.transform.forward * Time.deltaTime * 15, Space.World);
            
            yield return new WaitForSeconds(0.01f);
        }

        hang_end = false;
    }
    IEnumerator climb_05()
    {
        yield return new WaitForSeconds(0.5f);
        climb_time = false;
    }

    void Start()
    {
        player_rigidbody = GetComponent<Rigidbody>();
        player_state = GetComponent<PlayerState>();
        throwManager = GetComponent<ThrowManager>();
        mapChecker = GetComponentInChildren<MapChecker>();
        followDust = GetComponentInChildren<ParticleSystem>();
        qick = GameObject.Find("Quick_slot_new").GetComponent<Quick_slot_new>();
        child = transform.GetChild(0);
        dialogue = GameObject.Find("Dialogue").GetComponent<Dialogue>();
        lookAt = transform.forward;
        is_run = false;

        player_mate.color = new Color(1f, 1f, 1f, 1);
    }

    private void Update()                               // 키 입력은 Update에서 받고
    {
        if (InputManager.instance.click_mod == 0)
        {
            if (mapChecker.MapCheck(0.4f))
            {
                if (!tuto)
                {
                    if (movement != Vector3.zero)
                    {
                        if (!followDust.isPlaying)
                            followDust.Play();
                    }
                    else
                        followDust.Stop();
                }

                is_jumping = false;
                player_state.landing();
            }
            else if (!is_jumping)
            {
                is_jumping = true;
                player_state.flying(gameObject.transform.position.y);
            }
            else if (!tuto)
            {
                if (followDust.isPlaying)
                    followDust.Pause();
            }

            if (Input.GetButtonDown("Jump"))
            {
                if (!stop_jumping &&!is_jumping && player_state.lending_time == false && GameSystem.instance.GetModeNum() == 0)
                {
                    StartCoroutine(Jumping());
                }
            }

            if(Input.GetKeyDown(KeyCode.Z)) // 임시!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                PlayerState.instance.sleep_on();

            if (Input.GetKeyDown(KeyCode.R) && climb_up_bool == false)
            {
                if (Key_guide.instance.climb.activeSelf)
                    StartCoroutine(Key_guide.instance.climb_ing());

                if(climb_mod == true)
                {
                    climb_mod = false;
                    player_state.climb_off();

                    shadow.SetActive(false);
                    shadow2.SetActive(false);
                }
                else if(climb_crash == true && climb_mod == false)
                {
                    climb_mod = true;
                    player_state.climb_on();

                    Vector3 a = climb_fornt.transform.position;
                    gameObject.transform.position = climb_point_ob.transform.position;
                    rotate_ob.transform.LookAt(climb_ob.transform);
                    gameObject.transform.position = a;

                    climb_time = true;
                    StartCoroutine(climb_05());

                    shadow.SetActive(true);
                    shadow2.SetActive(true);
                }

                if (hang_mod == 2)
                {
                    StartCoroutine(hang_land());
                }
                else if (hang_crash == true && hang_mod == 0 && hang_end == false)
                {
                    StartCoroutine(hang_jump());

                    hang_end = true;
                    hang_mod = 1;
                    player_state.hang_on();
                }
            }

            if (climb_mod == true && climb_up_bool == false)
            {
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D))
                    player_state.animator.SetBool("climb_move", true);
                else
                    player_state.animator.SetBool("climb_move", false);


                if (Input.GetKey(KeyCode.W))
                    gameObject.transform.Translate((climb_head.transform.position - rotate_ob.transform.position).normalized * Time.deltaTime);
                if (Input.GetKey(KeyCode.S))
                    gameObject.transform.Translate((climb_head.transform.position - rotate_ob.transform.position).normalized * -Time.deltaTime);
                /*
                if (Input.GetKey(KeyCode.D))
                    gameObject.transform.Translate(Time.deltaTime, 0, 0);
                if (Input.GetKey(KeyCode.A))
                    gameObject.transform.Translate(-Time.deltaTime, 0, 0);
                */

                if (climb_crash == false) // 떨어진다!
                {
                    climb_mod = false;
                    player_state.climb_off();
                }
            }

            if (!tuto)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift) && GameSystem.instance.GetModeNum() == 0)
                {
                    player_state.dash_on();
                    is_run = true;
                }
                else if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    player_state.dash_off();
                    is_run = false;
                }
            }
            
            if (Input.GetMouseButtonDown(1) && !is_jumping)
            {
                //우클릭==조준.
                if (right_crash == 0)
                {
                    if (GameSystem.instance.item_search(qick.choose_item, "category") == "seed") // 씨앗 타입이어야지만 던져짐
                    {
                        if (GameSystem.instance.item_num[qick.choose_item] >= 1 && InputManager.instance.click_mod == 0)
                        {
                            GameSystem.instance.SetMode(1); //발사모드

                            throwManager.TargetOn(qick.choose_item);

                            player_state.shoot_ready();

                            right_crash = 1;

                            // 달리기 금지
                            player_state.dash_off();
                            is_run = false;
                        }
                    }
                }
                //우클릭 발사 취소.
                else if (right_crash == 1)
                {
                    if (GameSystem.instance.GetModeNum() == 1)
                    {
                        GameSystem.instance.SetMode(0); //기본모드

                        throwManager.ThrowSeed(false);//발사 옵션 false. 발사 취소

                        player_state.shoot_stop();

                        right_crash = 0;
                    }
                }
            }

            //조준모드면 계속 조준하면서 Raycasting.
            if (GameSystem.instance.GetModeNum() == 1)
                    throwManager.Targeting();

            //좌클릭 Down 발사.
            if (Input.GetMouseButtonDown(0) && !is_jumping)
            {
                if (GameSystem.instance.GetModeNum()==1) //취소를 안했을 경우에만 발사
                {
                    GameSystem.instance.SetMode(0); //기본모드

                    //발사 허가
                    if (throwManager.ThrowSeed(true))
                    {
                        GameSystem.instance.item_num[qick.choose_item]--;

                        if (GameSystem.instance.item_num[qick.choose_item] == 0)
                            GameSystem.instance.item_time.Remove(qick.choose_item);

                        player_state.shoot();
                    }
                    else
                        player_state.shoot_stop();

                    right_crash = 0;
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

            if (hang_mod == 0 && climb_mod == false)
                transform.localRotation = Quaternion.Slerp(transform.localRotation, dir, 0.5f);

            //발사모드에서는 캐릭터 회전 안함. 앞만봄
            if (GameSystem.instance.GetModeNum() == 1)
                child.localRotation = Quaternion.Slerp(child.localRotation, transform.localRotation, 0.2f);

            //이동할때만 모델회전
            else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
                     Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                if (climb_mod == false && hang_mod == 0)
                {
                    transform.localRotation = dir;
                    //캐릭터는 나아가는 쪽을 바라봄. lookAt=movement
                    if (movement != Vector3.zero)
                        child.localRotation = Quaternion.Slerp(child.localRotation, Quaternion.LookRotation(lookAt), 0.5f);
                }
            }

            // 아이템 먹기
            if (Input.GetKeyDown(KeyCode.E) && eat_item != "" && Key_guide.instance.item.activeSelf)
            {
                Eat_system.instance.eat_item(eat_item);
                eat_item = "";

                eat_object.GetComponent<ExampleItem>().eat();

                eat_bool = false;
                StartCoroutine(Key_guide.instance.item_ing());
                Key_guide.instance.item_name_off();
            }

            if (Input.GetKeyDown(KeyCode.E) && Key_guide.instance.objects.activeSelf)
            {
                switch (eat_objects.name)
                {
                    case "Radio":
                        dialogue.solo_talk(17);
                        Quest_clear_system.instance.clear_trigger[6]++;
                        break;

                    case "Frame":
                        dialogue.solo_talk(18);
                        Quest_clear_system.instance.clear_trigger[6]++;
                        break;

                    case "Letter":
                        dialogue.solo_talk(19);
                        Quest_clear_system.instance.clear_trigger[6]++;
                        break;

                    case "Book":
                        dialogue.solo_talk(20);
                        Quest_clear_system.instance.clear_trigger[6]++;
                        break;

                    case "Hari3_Book":
                        dialogue.solo_talk(21);
                        break;

                    case "Hari4_Book":
                        dialogue.solo_talk(23);
                        Quest_clear_system.instance.clear_trigger[7]++;
                        break;

                    case "Plant_Book":
                            Destroy(GameObject.Find("Plant_Book"));
                            Instantiate(Resources.Load<GameObject>("Tutorial/Plant_book"), GameObject.Find("Canvas").transform);
                            Eat_system.instance.eat_item("key");
                            Eat_system.instance.eat_item("mini_latter");
                            InputManager.instance.game_stop();
                        break;

                    case "Paper":
                        dialogue.solo_talk(26);
                        Instantiate(Resources.Load<GameObject>("Tutorial/Paper"), GameObject.Find("Canvas").transform);
                        Quest_clear_system.instance.clear_trigger[10]++;
                        break;
                }

                eat_objects.GetComponent<SphereCollider>().enabled = false;
                StartCoroutine(Key_guide.instance.object_ing());
            }

            if (Input.GetKeyDown(KeyCode.E) && Key_guide.instance.door.activeSelf)
            {
                StartCoroutine(Key_guide.instance.door_ing());
                chest_ob.GetComponent<Chest>().open();

                rotate_ob.transform.LookAt(chest_ob.transform);
                rotate_ob.transform.localRotation = Quaternion.Euler(new Vector3(0, rotate_ob.transform.localRotation.eulerAngles.y, 0));
                player_state.box_open();
            }

            if (Input.GetKeyDown(KeyCode.E) && Key_guide.instance.fix.activeSelf)
            {
                if(radio_ob.name == "radioTower1")
                {
                    GameSystem.instance.quest_state[19] = 3;
                    Quest_clear_system.instance.clear_reward(19);
                    GameObject.Find("Quest_quick").GetComponent<Quest_quick>().start_co(GameSystem.instance.quest_list[19 - 1]["title"], true);
                    GameObject.Find("Quest_quick").GetComponent<Quest_quick>().quest_re();
                }
                if (radio_ob.name == "radioTower2")
                {
                    GameSystem.instance.quest_state[5] = 2;
                }

                StartCoroutine(Key_guide.instance.fix_ing());
                radio_ob.GetComponent<Radio>().fixs();
            }
        }

        // 열려라 도감!
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (composer.activeSelf == true)
                composer.SetActive(false);
            else
            {
                note.SetActive(false);
                composer.SetActive(true);
            }
        }

        // 열려라 다이어리!
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (note.activeSelf == true)
                note.SetActive(false);
            else
            {
                composer.SetActive(false);
                note.SetActive(true);                           
            }
        }

        if(climb_mod == true || hang_mod != 0)
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
        movement = transform.forward * input_vertical + transform.right * input_horizontal;
        movement = movement.normalized;

        if (movement != Vector3.zero)
        {
            lookAt = movement;
        }
        else if(!tuto)
            followDust.Stop();

        if (climb_mod == false && hang_mod == 0 && player_state.lending_time == false)
        {
            Vector3 newPos = transform.localPosition +
                    transform.TransformDirection(movement * (is_run ? player_run_speed : player_speed) * Time.deltaTime);
            player_rigidbody.MovePosition(newPos);
        }
        if ((Mathf.Abs(movement.z) + Mathf.Abs(movement.x)) >= 1 && climb_mod == false && hang_mod == 0)
            player_state.state_move = 1;
        else
            player_state.state_move = 0;
    }

    IEnumerator Jumping()
    {
        player_state.jump();

        yield return new WaitForSeconds(0.05f);

        player_rigidbody.AddForce(Vector3.up * player_jump_power, ForceMode.Impulse);   //점프
        StartCoroutine(StopJumping());
    }

    private void OnTriggerEnter(Collider collision)
    {
        switch(collision.gameObject.name)
        {
            case "trigger1":
                if(Quest_clear_system.instance.clear_trigger[7] == 0 && GameSystem.instance.quest_state[7] == 1)
                {
                    dialogue.solo_talk(22);
                    Quest_clear_system.instance.clear_trigger[7]++;
                }
                break;

            case "trigger2":
                if (Quest_clear_system.instance.clear_trigger[9] == 0 && GameSystem.instance.quest_state[9] == 1)
                {
                    dialogue.solo_talk(25);
                    Quest_clear_system.instance.clear_trigger[9]++;
                }
                break;

            case "trigger_2floar":
                dialogue.solo_talk(28);
                break;


            case "radioTower1":
                if (collision.gameObject.GetComponent<Radio>().actives == false && GameSystem.instance.quest_state[19] == 2)
                {
                    radio_ob = collision.gameObject;
                    Key_guide.instance.fix_on();
                }
                break;

            case "radioTower2":
                if (collision.gameObject.GetComponent<Radio>().actives == false && GameSystem.instance.quest_state[5] == 1)
                {
                    radio_ob = collision.gameObject;
                    Key_guide.instance.fix_on();
                }
                break;
        }

        if (collision.gameObject.name == "white_seed_area")
        {
            State.instance.white_seed = true;
        }

        if (collision.gameObject.name == "brown_trigger" && climb_mod == false)
        {
            climb_ob = collision.gameObject.GetComponent<brown_trigger>().climb_model_ob;
            climb_point_ob = collision.gameObject.GetComponent<brown_trigger>().climb_point_ob;
            climb_crash = true;

            Key_guide.instance.climb_on();
        }

        if(collision.gameObject.name == "Chest_trigger")
        {
            if (collision.gameObject.GetComponent<Chest>().on == false)
            {
                chest_ob = collision.gameObject;
                Key_guide.instance.door_on();
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        ExampleItem item = collision.GetComponent<ExampleItem>(); //IItem을 상속받는 모든 아이템들
        if (item != null)                         //아이템과 부딪혔다면 함수를 호출하고 지움.
        {
            Key_guide.instance.item_off();
            Key_guide.instance.item_name_off();
        }

        if (collision.gameObject.CompareTag("Objects"))
        {
            Key_guide.instance.object_off();
        }

        if (collision.gameObject.name == "white_seed_area")
        {
            State.instance.white_seed = false;
        }

        if (collision.gameObject.name == "brown_trigger" && climb_time == false)
        {
            climb_crash = false;

            Key_guide.instance.climb_off();
        }

        if (collision.gameObject.name == "Chest_trigger")
        {
            Key_guide.instance.door_off();
        }

        if (collision.gameObject.name == "radioTower1" || collision.gameObject.name == "radioTower2")
        {
            Key_guide.instance.fix_off();
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        ExampleItem item = collision.GetComponent<ExampleItem>(); //IItem을 상속받는 모든 아이템들
        if (item != null)                         //아이템과 부딪혔다면 함수를 호출하고 지움.
        {
            eat_bool = true;
            eat_item = collision.name;
            eat_object = collision.gameObject;
            Key_guide.instance.item_on();
            Vector3 eat_pos = cameras.WorldToScreenPoint(eat_object.transform.position);
            Key_guide.instance.item_name_on(eat_item, eat_pos);
        }

        if (collision.gameObject.CompareTag("Objects"))
        {
            string name = null;
            bool key_on = false;

            switch (collision.gameObject.name)
            {
                case "Radio":
                    name = "라디오";
                    key_on = true;
                    eat_objects = collision.gameObject;
                    break;

                case "Frame":
                    name = "가족사진";
                    key_on = true;
                    eat_objects = collision.gameObject;
                    break;

                case "Letter":
                    name = "편지";
                    key_on = true;
                    eat_objects = collision.gameObject;
                    break;

                case "Book":
                    name = "다이어리";
                    key_on = true;
                    eat_objects = collision.gameObject;
                    break;

                case "Hari3_Book":
                    name = "해리포터 3권";
                    if (GameSystem.instance.quest_state[6] == 3)
                    {
                        key_on = true;
                        eat_objects = collision.gameObject;
                    }
                    else
                        key_on = false;
                    break;

                case "Hari4_Book":
                    name = "해리포터 4권";
                    if (Quest_clear_system.instance.clear_trigger[7] == 1)
                    {
                        key_on = true;
                        eat_objects = collision.gameObject;
                    }
                    else
                        key_on = false;
                    break;

                case "Plant_Book":
                    name = "식물책";
                    if (GameSystem.instance.quest_state[8] == 1)
                    {
                        key_on = true;
                        eat_objects = collision.gameObject;
                    }
                    else
                        key_on = false;
                    break;

                case "Paper":
                    name = "연구자료";
                    if (GameSystem.instance.quest_state[10] == 1)
                    {
                        key_on = true;
                        eat_objects = collision.gameObject;
                    }
                    else
                        key_on = false;
                    break;
            }

            if(key_on ==true)
                Key_guide.instance.object_on(name, cameras.WorldToScreenPoint(collision.gameObject.transform.position));
        }
    }

    public void shadow_out()
    {
        Debug.Log("갈색 떨어짐");
        climb_crash = false;

        shadow2.SetActive(false);
        shadow.SetActive(false);
    }
}
