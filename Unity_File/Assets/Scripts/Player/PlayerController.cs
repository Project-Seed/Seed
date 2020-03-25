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

    public bool throw_mode = false;                 // 던지기 모드 (임시변수)
    public float throw_position;

    [SerializeField]
    private float player_speed = 4.0f;         // 캐릭터 속도
    private float player_jump_power = 4.0f;    // 캐릭터 점프력

    public GameObject inventory; // 인벤토리
    public GameObject composer; // 합성창
    public GameObject note; // 다이어리
    public Transform camera_rig_transform;

    IEnumerator StopJumping()                  // 이단 점프를 막기 위해 점프시 1초간 점프금지
    {
        is_jumping = false;
        yield return new WaitForSeconds(0.5f);
    }
    void Start()
    {
        player_rigidbody = GetComponent<Rigidbody>();
        player_transform = GetComponent<Transform>();
        is_jumping = false;
        turning = false;
        is_back = false;
    }

    private void Update()                               // 키 입력은 Update에서 받고
    {
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

            input_horizontal = Input.GetAxisRaw("Horizontal");
            input_vertical = Input.GetAxisRaw("Vertical");

            Vector3 movement = new Vector3(input_horizontal, 0, input_vertical);
            movement = movement.normalized;
            //키 입력이 들어온 순간 캐릭터의 vec을 카메라 vec에 맞춤. (rotate)
            //쿼터니언 값 수상함. 0~70도 정도까지만 잘 들어가고 나머지는 꼬임. <<해결
            if (!movement.Equals(Vector3.zero) || throw_mode)
            {
                Quaternion dir = camera_rig_transform.localRotation;
                dir.x = 0f; dir.z = 0f;
                transform.localRotation = Quaternion.Slerp(transform.localRotation, dir, 0.5f);
            }
            
            player_transform.Translate(movement * player_speed * Time.deltaTime, Space.Self);

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

        if (Input.GetKeyDown(KeyCode.O) && InputManager.instance.click_mod == 0)
        {
            if (composer.activeSelf == true)
                composer.SetActive(false);
            else if(InputManager.instance.click_mod == 0)
                composer.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.P) && InputManager.instance.click_mod == 0)
        {
            if (note.activeSelf == true)
                note.SetActive(false);
            else if (InputManager.instance.click_mod == 0)
                note.SetActive(true);
        }
    }

    private void Jumping()
    {
        if (!is_jumping || !in_ground)
            return;

        player_rigidbody.AddForce(Vector3.up * player_jump_power, ForceMode.Impulse);   //점프
        
        StartCoroutine("StopJumping");
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
