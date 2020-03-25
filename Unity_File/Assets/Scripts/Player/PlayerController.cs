using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform player_transform;
    // 플레이어의 위치정보를 가져옴   
    public Transform Player_transform { get => player_transform; }

    private Rigidbody player_rigidbody;
    private float input_horizontal;         // 수직방향 입력 ws
    private float input_vertical;           // 수평방향 입력 ad
    public bool turning;                   // 회전 중
    private float amount = 180.0f;          // 회전 각도
    private float DegreesLeft;              // 남은 각 계산
    private bool is_jumping;                // 점프키를 입력하면 true.
    private bool in_ground;                 // 땅에 있으면 true.
    private bool is_back;                   // 반대방향을 보고있는지.
    private bool is_run;                    // 달리고 있는지.

    Vector3 movement;                       // 계산결과로 나올 이동 벡터.

    public Vector3 targetVec;
    public Vector3 upVec;
    public Vector3 rightVec;

    Vector3 offset;
    public bool throw_mode = false;                 // 던지기 모드 (임시변수)
    public float throw_position;

    [SerializeField]
    private float player_speed = 2.0f;         // 캐릭터 걷는 속도
    private float player_run_speed = 6.0f;     // 캐릭터 달리는 속도
    private float player_jump_power = 4.0f;    // 캐릭터 점프력

    public GameObject inventory; // 인벤토리
    public GameObject composer; // 합성창
    public GameObject note; // 다이어리

    public Animator animator;


    IEnumerator StopJumping()                  // 이단 점프를 막기 위해 점프시 1초간 점프금지
    {
        is_jumping = false;
        yield return new WaitForSeconds(0.5f);
    }
    void Start()
    {
        player_rigidbody = GetComponent<Rigidbody>();
        player_transform = GetComponent<Transform>();
        offset = new Vector3(0f, 1.5f, 0f);
        targetVec = player_transform.position + offset;
        is_jumping = false;
        turning = false;
        is_back = false;
        is_run = false;
    }

    private void Update()                               // 키 입력은 Update에서 받고
    {
        targetVec = player_transform.position + offset;
        upVec = player_transform.up;
        rightVec = player_transform.right;


        input_horizontal = Input.GetAxis("Horizontal");
        input_vertical = Input.GetAxis("Vertical");

        if (InputManager.instance.click_mod == 0)
        {
            if (Input.GetButtonDown("Jump"))
            {
                is_jumping = true;
                Debug.Log("Jump!");
            }

            if (!is_back && Input.GetKeyDown(KeyCode.S)) //앞인상태에서 S누르면
            {
                turning = true;
                is_back = true;
                DegreesLeft = amount;
            }

            if (is_back && Input.GetKeyDown(KeyCode.W)) //뒤인상태에서 w누르면
            {
                turning = true;
                is_back = false;
                DegreesLeft = amount;
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                animator.SetBool("front", true);
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                animator.SetBool("front", false);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                animator.SetBool("back", true);
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                animator.SetBool("back", false);
            }
            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                animator.SetBool("run", true);
                is_run = true;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                animator.SetBool("run", false);
                is_run = false;
            }

            if (Input.GetMouseButtonDown(0))
            {
                throw_mode = true;
                GameSystem.instance.SetMode(1);
            }

            if (Input.GetMouseButtonUp(0))
            {
                throw_mode = false;
                GameSystem.instance.SetMode(0);
            }

            //이동
            if (is_run == false)
            {
                movement.Set(input_horizontal, 0, input_vertical);
                movement = movement * player_speed * Time.deltaTime;
                player_transform.Translate(movement.normalized * player_speed * Time.deltaTime, Space.Self);
            }
            else
            {
                movement.Set(input_horizontal, 0, input_vertical);
                movement = movement * player_run_speed * Time.deltaTime;
                player_transform.Translate(movement.normalized * player_run_speed * Time.deltaTime, Space.Self);
            }


            //점프
            if (is_jumping && in_ground)
                Jumping();
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
    }

    private void Jumping()
    {
        if (!is_jumping || !in_ground)
            return;

        player_rigidbody.AddForce(Vector3.up * player_jump_power, ForceMode.Impulse);   //점프
        
        StartCoroutine("StopJumping");

        animator.SetTrigger("jump");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            in_ground = true;
            Debug.Log("in Ground");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            in_ground = false;
            Debug.Log("not in Ground");
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
