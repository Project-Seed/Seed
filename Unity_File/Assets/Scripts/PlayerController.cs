using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform player_transform;     // 플레이어의 위치정보를 가져옴
    private Rigidbody player_rigidbody;
    private float input_horizontal;         // 수직방향 입력 ws
    private float input_vertical;           // 수평방향 입력 ad
    public bool turning;                   // 회전 중
    private float amount = 180.0f;          // 회전 각도
    //private float time = 0.5f;              // 회전 시간
    private float DegreesLeft;              // 남은 각 계산
    private bool is_jumping;                // 점프키를 입력하면 true.
    private bool in_ground;                 // 땅에 있으면 true.
    private bool is_back;                   // 반대방향을 보고있는지.

    Vector3 movement;                       // 계산결과로 나올 이동 벡터.

    //public Transform main_camera;           // 카메라 트랜스폼 가져옴
    //public Transform aim;
    public bool throw_mode;                 // 던지기 모드
    public float throw_position;


    [SerializeField]
    private float player_speed = 4.0f;         // 캐릭터 속도
    private float player_jump_power = 4.0f;    // 캐릭터 점프력
    float mouse_input;
    public float mouse_sensitivity = 10.0f; // 마우스 감도
    public Vector3 mouse_move;
    public GameObject inventory; // 인벤토리
    public GameObject composer; // 합성창
    public GameObject diary; // 다이어리

    public ThirdCamera tc;
    public ThrowManager GetThrowManager;

    IEnumerator StopJumping()                  // 이단 점프를 막기 위해 점프시 1초간 점프금지
    {
        is_jumping = false;
        yield return new WaitForSeconds(0.5f);
    }
    void Start()
    {
        player_transform = GetComponent<Transform>();
        player_rigidbody = GetComponent<Rigidbody>();
        tc = GetComponent<ThirdCamera>();
        is_jumping = false;
        turning = false;
        is_back = false;
    }

    private void Update()                               // 키 입력은 Update에서 받고
    {

        // 카메라 회전하면 캐릭터도 회전
      
        input_horizontal = Input.GetAxis("Horizontal");
        input_vertical = Input.GetAxis("Vertical");

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

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventory.activeSelf == true)
                inventory.SetActive(false);
            else
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
            if (diary.activeSelf == true)
                diary.SetActive(false);
            else
                diary.SetActive(true);
        }
        if (Input.GetMouseButtonDown(0))
        {
            GetThrowManager.OnThrowMode();
            //낙하지점예상하기
            //이 곳에 원 그리기
        }
        if (Input.GetMouseButtonUp(0))
        {
            //throw_position = 0.0f; //초기화
            GetThrowManager.ExitThrowMode();
        }
        //움직임
        //Debug.Log("Horizontal : " + input_horizontal.ToString());
        //Debug.Log("Vertical : " + input_vertical.ToString());
        movement.Set(input_horizontal, 0, input_vertical);
        movement = movement * player_speed * Time.deltaTime;
        player_transform.Translate(movement.normalized * player_speed * Time.deltaTime, Space.Self);

       // mouse_input = Input.GetAxisRaw("Mouse X") * mouse_sensitivity;
        mouse_move += new Vector3(0, Input.GetAxis("Mouse X") * mouse_sensitivity, 0);
        player_transform.localEulerAngles = mouse_move;
        //Debug.Log("캐릭터 : " + mouse_move.y);

        //점프
        if (is_jumping && in_ground)
            Jumping();
     
    }

   
    private void OnMouseDrag()
    {
        
    }

    private void LateUpdate()
    {
        //player_transform.rotation.Set(0, main_camera.rotation.y, 0, 0);
        //player_transform.eulerAngles = new Vector3(0, main_camera.eulerAngles.y, 0);
    }

    private void Moving()
    {   
        //Vector3 cam_front = (main_camera.up + main_camera.forward);
        movement.Set(input_horizontal, 0, input_vertical);
        movement = movement * player_speed * Time.deltaTime;

        // Transform 쓰는 방법
        player_transform.Translate(movement.normalized * player_speed * Time.deltaTime, Space.Self);

        // 회전 프레임 당 각도 계산해서 딱 180도 돌게함
        //if (turning)
        //{
        //    float DegreesThisFrame = (amount * Time.deltaTime) / time;

        //    if (DegreesThisFrame > DegreesLeft)
        //    {
        //        DegreesThisFrame = DegreesLeft;
        //        turning = false;
        //    }

        //    transform.Rotate(0f, DegreesThisFrame, 0f);
        //    DegreesLeft -= DegreesThisFrame;
        //}
      
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

            if (GameSystem.instance.dictionary_num[other.name] == false) // '한번도' 못먹었던 아이템이면 (도감용)
                GameSystem.instance.dictionary_num[other.name] = true;

            Destroy(other.gameObject);
        }
    }
}
